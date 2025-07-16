using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

class DisplayServer
{
    static void Main()
    {
        TcpListener listener = new TcpListener(IPAddress.Any, 6000);
        listener.Start();
        Console.WriteLine("Display server launched on port 6000");

        while (true)
        {
            TcpClient client = listener.AcceptTcpClient();
            Console.WriteLine("Processing server has connected.");

            Thread clientThread = new Thread(() => HandleClient(client));
            clientThread.Start();
        }
    }

    static void HandleClient(TcpClient client)
    {
        try
        {
            using NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];
            int bytesRead;

            while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
            {
                string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Console.WriteLine(message.Trim());
            }

            Console.WriteLine("Processing server disconnected.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in display server: {ex.Message}");
        }
    }
}
