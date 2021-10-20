using System;

namespace ChatRoom
{
    class Program
    {
        static void Main(string[] args)
        {
            int selection = MainMenu();

            switch (selection)
            {
                case 1:
                    Client.ClientCreator();
                    break;
                case 2:
                    Server.ServerCreator();
                    break;
                case 3:
                    Reader.ReaderCreator();
                    break;
                case 4:
                    Environment.Exit(0);
                    return;
            }
        }

        static int MainMenu()
        {
            int selection = 0;
            bool firstRun = true;

            while (selection < 1 || selection > 4)
            {
                Console.Clear();
                Console.WriteLine("Chat Room");
                Console.WriteLine("1) Create Client");
                Console.WriteLine("2) Create Server");
                Console.WriteLine("3) Start Reader (Read chat anonymously)");
                Console.WriteLine();
                Console.WriteLine("4) Exit");
                
                if (!firstRun)
                    Console.WriteLine("\nPlease enter a valid option");

                try
                {
                    selection = int.Parse(Console.ReadKey().KeyChar.ToString());
                }
                catch
                {
                    selection = 0;
                }

                firstRun = false;
            }

            return selection;
        }
    }
}
