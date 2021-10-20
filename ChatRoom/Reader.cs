using System;
using System.Net.Sockets;
using System.IO;

namespace ChatRoom
{
    class Reader
    {
        public static void ReaderCreator()
        {
            Console.Clear();
            Console.WriteLine("Reader Creator");
            Console.WriteLine();
            Console.WriteLine("Please enter the IP of the chat room (do not include port): ");

            string selectedIP = Console.ReadLine();

            Console.WriteLine("Please enter the port of the chat room: ");

            int selectedPort = int.Parse(Console.ReadLine());

            StartReader(selectedIP, selectedPort);
        }

        public static void StartReader(string ipAddress, int port)
        {
            Console.Clear();

            TcpClient tcpClient = new TcpClient(ipAddress, port);
            NetworkStream stream = tcpClient.GetStream();

            while (true)
            {
                byte[] bb = new byte[200];
                int k = stream.Read(bb);

                for (int i = 0; i < k; i++)
                {
                    Console.Write(Convert.ToChar(bb[i]));
                }
                Console.WriteLine();
            }
        }
    }
}
