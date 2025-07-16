using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class DisplayServer
{
    static void Main()
    {
        TcpListener listener = new TcpListener(IPAddress.Any, 6000);
        listener.Start();
        Console.WriteLine("Display server is waiting for a connection...");

        using TcpClient client = listener.AcceptTcpClient();
        Console.WriteLine("Connection established.");

        using NetworkStream stream = client.GetStream();
        byte[] buffer = new byte[1024];
        int bytesRead;

        while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
        {
            string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            Console.WriteLine(message.Trim());
        }

        Console.WriteLine("Connection closed.");
    }
}
