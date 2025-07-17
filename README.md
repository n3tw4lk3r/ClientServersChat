# ClientServersChat

A simple message relay in C#.

## Structure

- `Client/` - connects to ProcessingServer and sends messages
- `ProcessingServer/` - receives messages, processes them (removes duplicates) and sends them to DisplayServer
- `DisplayServer/` - displays processed messages  
  
DisplayServer's port (6000) and ProcessingServer's port (5000) are hardcoded. You have to specify ProcessingServer's ip and Client's port using command line arguments when running each Client.
## Run

Run each command in a different terminal:

```Bash
dotnet run --project DisplayServer
dotnet run --project ProcessingServer
```

Then you may launch as many clients as you like (each one in a different terminal, with a different LocalPort):
```Bash
dotnet run --project Client -- <ProcessingServerIP> <LocalPort>
```