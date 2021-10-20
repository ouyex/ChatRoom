using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.IO;

namespace ChatRoom
{
    class Server
    {
        // Dictonary of clients
        private static Dictionary<int, NetworkStream> clientStreams = new Dictionary<int, NetworkStream>();

        public static void ServerCreator()
        {
            Console.Clear();
            Console.WriteLine("Server Creator");
            Console.WriteLine();
            Console.WriteLine("Please choose a local IPv4 Address to host the server on: ");

            string selectedIP = Console.ReadLine();

            Console.WriteLine("Please choose an open port to host the server on: ");

            int selectedPort = int.Parse(Console.ReadLine());

            StartServer(selectedIP, selectedPort);
        }

        public static void StartServer(string ipAddress, int port)
        {
            Console.Clear();

            // Parse ip address string
            Console.WriteLine("Parsing IP...");
            IPAddress ipAddr = IPAddress.Parse(ipAddress);

            // Create tcp listener
            Console.WriteLine("Creating and starting listener...");
            TcpListener listener = new TcpListener(ipAddr, port);
            listener.Start();

            Console.WriteLine("INIT FINISHED");
            Console.WriteLine();
            Console.WriteLine("Listening for clients...");

            

            int clientCount = 1;

            while (true)
            {
                try
                {
                    Console.WriteLine();
                    var client = listener.AcceptTcpClient();
                    clientStreams.Add(clientCount, client.GetStream());
                    Task.Run(() => HandleClient(client, clientCount));

                    SendGlobalMessage("Client " + clientCount + " has connected. (" + clientStreams.Count + " total clients)");

                    clientCount++;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        private static void HandleClient(TcpClient client, int clientNum)
        {
            Console.WriteLine("Connected to client #" + clientNum);
            ASCIIEncoding asen = new ASCIIEncoding();
            client.GetStream().Write(asen.GetBytes(clientNum.ToString()));

            while (true)
            {
                byte[] buffer = new byte[500];
                int readSize = clientStreams[clientNum].Read(buffer);

                string message = "";

                for (int i = 0; i < readSize; i++)
                {
                    message += Convert.ToChar(buffer[i]);
                }

                SendGlobalMessage(message);
            }
        }

        public static void SendGlobalMessage(string message)
        {
            Console.WriteLine("global : " + message);
            ASCIIEncoding asen = new ASCIIEncoding();

            foreach (var item in clientStreams)
            {
                item.Value.Write(asen.GetBytes(message));
            }
        }
    }
}
