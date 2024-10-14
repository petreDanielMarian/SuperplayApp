using GameLogic.Helpers;
using GameLogic.Types;
using Serilog;
using SuperServer.Factories;
using SuperServer.Interfaces;
using SuperServer.Messages.Responses;
using System.Net;
using System.Net.WebSockets;
using System.Security.Cryptography;

namespace SuperServer
{
    public class Server
    { 
        private long _serverId { get; }
        public HttpListener HttpListener { get; init; }

        public Server()
        {
            _serverId = RandomNumbersPool.GetUniqueServerId();

            HttpListener = new HttpListener();
            HttpListener.Prefixes.Add("http://localhost:8080/");

            Log.Information($"Server ({_serverId}) is running on {HttpListener.Prefixes.First()}");
            Log.Warning($"Please be patient with the client response time (delays are added to read the texts)");
        }

        public async Task AwaitConnectionsAsync()
        {
            while (true)
            {
                try
                {
                    HttpListenerContext context = await HttpListener.GetContextAsync(); // This awaits a client to connect
                    if (context.Request.IsWebSocketRequest)
                    {
                        HttpListenerWebSocketContext webSocketContext = await context.AcceptWebSocketAsync(null);
                        WebSocket webSocket = webSocketContext.WebSocket;

                        _ = Task.Run(() => HandleClientAsync(webSocket));
                    }
                }
                catch (WebSocketException wsEx)
                {
                    Log.Error("Connection issue" + wsEx.ToString());
                    throw;
                }
                catch (Exception ex)
                {
                    Log.Error(ex.ToString());
                    throw;
                }
            }
        }

        public void Start()
        {
            try
            {
                HttpListener.Start();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex.ToString());
                throw;
            }
        }

        public void Shutdown()
        {
            try
            {
                HttpListener.Close();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex.ToString());
                throw;
            }
}

        /// <summary>
        /// Start communication with client and wait for calls from it
        /// </summary>
        /// <param name="webSocket"></param>
        /// <returns></returns>
        private async Task HandleClientAsync(WebSocket webSocket)
        {
            await EstablishClientConnectionAsync(webSocket);

            // start exchanging messages
            while (true)
            {
                string response = await TransferDataHelper.RecieveTextOverChannelAsync(webSocket);

                string[] tokens = response.Split(' ');
                var commandType = (CommandType)int.Parse(tokens[0]);
                var clientId = tokens[1];
                var payload = string.Join(" ", tokens.Skip(2));

                ICommandHandler handler = new CommandHandlerFactory(webSocket, payload).GetCommandHandler(commandType);

                Log.Information($"Recived and handling {commandType} from client {clientId}");

                _ = Task.Run(handler.Handle);
            }
        }

        private async Task EstablishClientConnectionAsync(WebSocket webSocket)
        {
            string clientInfo = await TransferDataHelper.RecieveTextOverChannelAsync(webSocket);

            Log.Information($"Connection request recieved from client {clientInfo}");

            string[] tokens = clientInfo.Split(" ");
            string clientId = tokens[0];
            string encryptedClientId = tokens[1];

            bool isClientAccepted = IsClientAccepted(clientId, encryptedClientId);
            await Task.Delay(1000);

            if (isClientAccepted)
            {
                await TransferDataHelper.SendTextOverChannelAsync(webSocket, new ConnectionResponse(_serverId.ToString()).ToString());

                Log.Information($"Connection established with client {clientId}");
            }
            else
            {
                await TransferDataHelper.SendTextOverChannelAsync(webSocket, new ConnectionResponse("REJECTED").ToString());

                Log.Information($"Connection refused with suspicious client {clientId}");
            }
        }

        private bool IsClientAccepted(string clientId, string encryptedClientId)
        {
            string decryptedId = string.Empty;
            string? key;

            using (var reader = new StreamReader("SuperKey.snk"))
            {
                key = reader.ReadLine();
            }

            if(key != null)
            {
                Guid encryptionKey = Guid.Parse(key);

                byte[] fullCipher = Convert.FromBase64String(encryptedClientId);

                using Aes aes = Aes.Create();
                aes.Key = encryptionKey.ToByteArray();

                byte[] iv = new byte[aes.BlockSize / 8];

                Array.Copy(fullCipher, 0, iv, 0, iv.Length);
                aes.IV = iv;

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using var memoryStream = new MemoryStream(fullCipher, iv.Length, fullCipher.Length - iv.Length);
                using var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
                using var streamReader = new StreamReader(cryptoStream);

                decryptedId = streamReader.ReadToEnd();
            }

            return decryptedId.Equals(clientId);
        }
    }
}
