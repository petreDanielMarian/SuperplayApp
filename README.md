# Server-Client application

## Idea:

- Create Server-Client application based on WebSockets that supports some commands:
  - Login(udid) -> PlayerId
  - UpdateResources(ResourceType<Coins/Rolls>, ResourceValue) -> newResourceValue
  - SendGift(PlayerId, ResourceType, ResourceValue)
---
## Server:
- starts awaiting for client commands
- sends notification to clients, if triggered
- In-Memory state saving
- C# 8.0 Console app, Serilog (file and console)
---
## Client:
- sends command to server
- server sends the request and it's handled
- C# 8.0 Console app, Serilog (file)
---
### How to run:
- Build the project:
  ```
  dotnet build SuperplayApp.sln
  ```
- Start the server:
  - go to <repo>\SuperServer\bin\Debug\net8.0 and run SuperServer.exe
- Start clients:
  - go to <repo>\SuperPlayer\bin\Debug\net8.0 and run SuperPlayer.exe
  - you can open multiple clients
