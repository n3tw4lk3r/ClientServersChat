using System;
using System.Net.Sockets;
using System.Text;

class Client
{
    static void Main()
    {
        using TcpClient client = new TcpClient("127.0.0.1", 5000);
        using NetworkStream stream = client.GetStream();

        Console.WriteLine("Enter your message. Press Ctrl-C to quit.");

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
