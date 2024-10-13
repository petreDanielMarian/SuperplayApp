﻿using GameLogic.Helpers;
using GameLogic.Messages.Responses;
using GameLogic.Model;
using SuperServer.Database;
using SuperServer.Interfaces;
using System.Net.WebSockets;

namespace SuperServer.CommandHandlers
{
    public class LoginCommandHandler : ICommandHandler
    {
        private readonly string _recievedPayload;
        private WebSocket _webSocket;

        public LoginCommandHandler(WebSocket webSocket, string payload)
        {
            _webSocket = webSocket;
            _recievedPayload = payload;
        }

        public async Task Handle()
        {
            var udid = long.Parse(_recievedPayload);
            long playerId;

            if (PlayerRepository.RegisteredPlayers.TryGetValue(udid, out Player? value))
            {
                playerId = -value.Id;
            }
            else
            {
                playerId = RandomNumbersPool.GetUniquePlayerId();
                PlayerRepository.RegisterPlayer(udid, new PlayerConnection(new Player(playerId), _webSocket));
            }

            await TransferDataHelper.SendTextOverChannel(_webSocket, new LoginResponse(playerId).ToString());
        }
    }
}
