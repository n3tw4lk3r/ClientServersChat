# ClientServersChat

A simple message relay in C#

## Structure

- `Client/` - connects to the processing server and sends messages
- `ProcessingServer/` - receives messages, processes them (removes duplicates) and sends them to the display server
- `DisplayServer/` - displays processed messages

## Run

Run each command in a different terminal:

```Bash
dotnet run --project DisplayServer
dotnet run --project ProcessingServer
```

Then you may launch as many clients as you like (each one in a different terminal):
```Bash
dotnet run --project Client
```