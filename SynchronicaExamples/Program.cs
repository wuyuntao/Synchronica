using System;
using System.Threading;

namespace Synchronica.Examples
{
    class Program
    {
        static void Main(string[] args)
        {
            RunUnitTest();

            //StartServer();
        }

        static void RunUnitTest()
        {
            //var t = new Synchronica.Tests.Record.RecorderTest();
            //t.TestRecorder();

            //var t = new Synchronica.Tests.Replay.ReplayerTest();
            //t.TestReplayer();

            var t = new Synchronica.Tests.Simulation.VFloatTest();
            t.TestRemoveFramesBefore();
            t.TestRemoveFramesAfter();
        }

        static void StartServer()
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
