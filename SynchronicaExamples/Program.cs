using Synchronica.Examples.Schema;
using System;
using System.Threading;

namespace Synchronica.Examples
{
    class Program
    {
        static void Main(string[] args)
        {
            //RunUnitTest();

            StartSimpleServer();
        }

        static void RunUnitTest()
        {
            //var t = new Synchronica.Tests.Record.RecorderTest();
            //t.TestRecorder();

            var t = new Synchronica.Tests.Simulation.ReplayerTest();
            t.TestReplayer();

            //var t = new Synchronica.Tests.Simulation.VFloatTest();
            //t.TestRemoveFramesBefore();
            //t.TestRemoveFramesAfter();
        }

        static void StartSimpleServer()
        {
            var server = new Server.SimpleServer(4000);

            Thread.Sleep(1000);

            Client.SimpleClient client = null;
            var exit = false;

            while (!exit)
            {
                var command = Console.ReadLine().Trim();
                switch (command)
                {
                    case "login":
                    case "l":
                        if (client == null)
                        {
                            client = new Client.SimpleClient("Client1", "127.0.0.1", 4000);
                            client.Login();
                        }
                        break;

                    case "disconnect":
                    case "c":
                        if (client != null)
                            client.Disconnect();
                        break;

                    case "exit":
                    case "x":
                        exit = true;
                        break;

                    case "forward":
                    case "w":
                        if (client != null)
                            client.Input(Command.Up);
                        break;

                    case "back":
                    case "s":
                        if (client != null)
                            client.Input(Command.Down);
                        break;

                    case "left":
                    case "a":
                        if (client != null)
                            client.Input(Command.Left);
                        break;

                    case "right":
                    case "d":
                        if (client != null)
                            client.Input(Command.Right);
                        break;
                }
            }

            server.Close();
        }
    }
}
