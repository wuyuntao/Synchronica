using System;
using System.Threading;

namespace Synchronica.Examples
{
    class Program
    {
        static void Main(string[] args)
        {
            // RunUnitTest();

            StartSimpleServer();
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

        static void StartSimpleServer()
        {
            var server = new Server.SimpleServer(4000);

            Thread.Sleep(1000);

            var client = new Client.SimpleClient("Client1", "127.0.0.1", 4000);

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
