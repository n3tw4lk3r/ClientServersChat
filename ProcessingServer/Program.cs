using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

class ProcessingServer
{
    static void Main()
    {
        TcpListener listener = new TcpListener(IPAddress.Any, 5000);
        listener.Start();
        Console.WriteLine("Processing server launched on port 5000.");

        while (true)
        {
            TcpClient client = listener.AcceptTcpClient();
            Console.WriteLine("A client has connected.");

            Thread clientThread = new Thread(() => HandleClient(ref client));
            clientThread.Start();
        }
    }

    static void HandleClient(ref readonly TcpClient client)
    {
        try
        {
            var remoteEndPoint = client.Client.RemoteEndPoint as IPEndPoint;
            string clientIp = remoteEndPoint?.Address.ToString() ?? "unknown";
            int clientPort = remoteEndPoint?.Port ?? -1;

            string clientTag = $"[{clientIp}:{clientPort}]";

            using NetworkStream clientStream = client.GetStream();
            using TcpClient displayClient = new TcpClient("127.0.0.1", 6000);
            using NetworkStream displayStream = displayClient.GetStream();

            byte[] buffer = new byte[1024];
            int bytesRead;

            while ((bytesRead = clientStream.Read(buffer, 0, buffer.Length)) > 0)
            {
                string input = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                string processed = Process(ref input);
                string tagged = $"{clientTag} {processed}";
                byte[] outputData = Encoding.UTF8.GetBytes(tagged);
                displayStream.Write(outputData, 0, outputData.Length);
            }

            Console.WriteLine($"Client {clientTag} disconnected.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in client's stream: {ex.Message}");
        }
    }

    static string Process(ref readonly string text)
    {
        string[] words = text.Split(new[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
        HashSet<string> seen = new HashSet<string>();
        StringBuilder result = new StringBuilder();

        foreach (string word in words)
        {
            if (seen.Add(word))
                result.Append(word).Append(' ');
        }

        return result.ToString().Trim() + "\n";
    }
}
