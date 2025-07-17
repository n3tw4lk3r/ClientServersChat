using System.Net;
using System.Net.Sockets;
using System.Text;

class Client
{
    static void Main(string[] args)
    {
        if (args.Length != 2)
        {
            Console.WriteLine("Usage: Client <ProcessingServerIP> <LocalPort>");
            return;
        }

        string processingServerIp = args[0];
        int serverPort = 5000;
        int localPort = Convert.ToInt32(args[1]);

        IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, localPort);
        TcpClient client = new TcpClient(localEndPoint);
        client.Connect(processingServerIp, serverPort);

        Console.WriteLine($"Connected to {processingServerIp}:{serverPort}.");

        using NetworkStream stream = client.GetStream();

        Console.WriteLine("Enter your messages. Press Ctrl-C to quit:");

        while (true)
        {
            Console.Write("> ");
            string? message = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(message))
                continue;

            byte[] data = Encoding.UTF8.GetBytes(message + "\n");
            stream.Write(data, 0, data.Length);
        }
    }
}
