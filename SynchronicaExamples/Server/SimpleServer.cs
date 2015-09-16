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

using NLog;
using Synchronica.Examples.Scene;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Synchronica.Examples.Server
{
    class SimpleServer
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private SimpleScene scene = new SimpleScene();

        private TcpListener tcpListener;

        private List<SimpleClient> clients = new List<SimpleClient>();

        private object clientsLock = new object();

        private bool isClosed = false;

        public SimpleServer(int port)
        {
            this.tcpListener = new TcpListener(new IPAddress(0), port);
            this.tcpListener.Start();

            logger.Info("Started at {0}", this.tcpListener.Server.LocalEndPoint);

            ThreadPool.QueueUserWorkItem(AcceptThread);
            ThreadPool.QueueUserWorkItem(SceneThread);
        }

        public override string ToString()
        {
            return string.Format("{0}", GetType().Name);
        }

        public void Close()
        {
            if (!this.isClosed)
            {
                this.tcpListener.Stop();

                this.isClosed = true;
            }
        }

        private void AcceptThread(object state)
        {
            while (!this.isClosed)
            {
                try
                {
                    var tcpClient = this.tcpListener.AcceptTcpClient();
                    var client = new SimpleClient(tcpClient, this.scene);

                    lock (this.clientsLock)
                    {
                        this.clients.Add(client);
                    }
                }
                catch (SocketException ex)
                {
                    logger.Info("Disconnect {0}", ex.Message);

                    break;
                }
            }
        }

        private void SceneThread(object state)
        {
            while (!this.isClosed)
            {
                Thread.Sleep(100);

                var bytes = this.scene.Process();
                if (bytes != null)
                {
                    lock (this.clientsLock)
                    {
                        foreach (var client in this.clients)
                        {
                            if (client.LoginSucceeded)
                            {
                                client.WriteBytes(bytes);
                            }
                        }
                    }
                }
            }
        }
    }
}
