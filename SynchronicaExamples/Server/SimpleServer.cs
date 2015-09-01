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
            while(!this.isClosed)
            {
                Thread.Sleep(100);

                var bytes = this.scene.Process();
                if (bytes != null)
                {
                    lock(this.clientsLock)
                    {
                        foreach(var client in this.clients)
                        {
                            client.WriteBytes(bytes);
                        }
                    }
                }
            }
        }
    }
}
