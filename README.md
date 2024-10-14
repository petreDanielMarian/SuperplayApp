# Superplay Server-Client application

## Idea:

- Create Server-Client application based on WebSockets that supports some commands:
  - Login(udid) -> PlayerId
  - UpdateResources(ResourceType<Coins/Rolls>, ResourceValue) -> newResourceValue
  - SendGift(PlayerId, ResourceType, ResourceValue)
 
## Server:

---
### How to run:
- Build the project:
  ```
  dotnet build YourSolution.sln
  ```

## Client:

---

### How to run: 
