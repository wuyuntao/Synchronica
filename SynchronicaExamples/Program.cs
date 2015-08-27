using System;
using System.Threading;

namespace Synchronica.Examples
{
    class Program
    {
        static void Main(string[] args)
        {
            var server = new Server.DemoServer(4000);

            Thread.Sleep(1000);

            var client = new Client.DemoClient("Client1", "127.0.0.1", 4000);

            var exit = false;
            while (!exit)
            {
                var command = Console.ReadLine().Trim();
                switch (command)
                {
                    case "login":
                    case "l":
                        client.Login();
                        break;

                    case "exit":
                    case "x":
                        exit = true;
                        break;
                }
            }

            server.Close();
        }
    }
}
