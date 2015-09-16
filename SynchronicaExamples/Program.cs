/*
 * The MIT License (MIT)
 * 
 * Copyright (c) 2015 Wu Yuntao
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
*/

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

            //var t = new Synchronica.Tests.Simulation.ReplayerTest();
            //t.TestReplayer();

            //var t = new Synchronica.Tests.Simulation.VFloatTest();
            //t.TestRemoveFramesBefore();
            //t.TestRemoveFramesAfter();

            var t = new Synchronica.Tests.Simulation.SimulationConsistencyTest();
            t.TestConsistency();
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
                        {
                            client.Disconnect();
                            client = null;
                        }
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
