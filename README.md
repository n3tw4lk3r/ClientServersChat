# ClientServersChat
A simple message relay in C#

## Structure
- `Client/` - connects to the processing server and sends messages
- `ProcessingServer/` - receives messages, processes them (removes duplicates) and sends them to the display server
- `DisplayServer/` - displays processed messages