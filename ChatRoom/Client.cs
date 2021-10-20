using System;
using System.Net;
using System.Text;
using System.Net.Sockets;
using System.IO;
using System.Threading.Tasks;

namespace ChatRoom
{
    class Client
    {
        static TcpClient tcpClient;
        static NetworkStream stream;
        static ASCIIEncoding asen = new ASCIIEncoding();
        static int clientNum = -1;
        static string username = "";

        public static void ClientCreator()
        {
            Console.Clear();
            Console.WriteLine("Client Creator");
            Console.WriteLine();
            Console.WriteLine("Please enter the IP of the chat room (do not include port): ");

            string selectedIP = Console.ReadLine();

            Console.WriteLine("Please enter the port of the chat room: ");

            int selectedPort = int.Parse(Console.ReadLine());

            StartClient(selectedIP, selectedPort);
        }

        public static void StartClient(string ipAddress, int port)
        {
            Console.Clear();

            tcpClient = new TcpClient(ipAddress, port);
            stream = tcpClient.GetStream();

            while (clientNum == -1)
            {
                try
                {
                    byte[] buffer = new byte[200];
                    int k = stream.Read(buffer);

                    for (int i = 0; i < k; i++)
                    {
                        clientNum = int.Parse(Convert.ToChar(buffer[i]).ToString());
                    }
                }
                catch {}
            }
            Console.Title = "Client " + clientNum;

            Console.WriteLine("Please enter an username: ");
            username = Console.ReadLine();
            Console.Clear();

            Console.WriteLine("Welcome, " + username + ". Your client number is " + clientNum + ".");

            Task.Run(() => StreamWriter());
            Task.Run(() => StreamReader());

            while (true) { }
        }

        private static void StreamWriter()
        {
            while (true)
            {
                string textInput = Console.ReadLine();
                
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                ClearCurrentConsoleLine();

                stream.Write(asen.GetBytes("[ " + username + " ] (" + clientNum + ") " + textInput));
            }
        }

        private static void StreamReader()
        {
            while (true)
            {
                byte[] buffer = new byte[500];
                int k = stream.Read(buffer);

                for (int i = 0; i < k; i++)
                {
                    Console.Write(Convert.ToChar(buffer[i]));
                }
                Console.WriteLine();
            }
        }

        private static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }
    }
}
