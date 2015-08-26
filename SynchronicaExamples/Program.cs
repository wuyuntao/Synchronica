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

            var client = new Client.DemoClient("127.0.0.1", 4000);

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();

            server.Close();
        }
    }
}
