using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Synchronica.Examples.Server
{
    class DemoServer : LogObject
    {
        private TcpListener tcpListener;

        private List<DemoClient> clients = new List<DemoClient>();

        private object clientsLock = new object();

        private bool isClosed = false;

        public DemoServer(int port)
        {
            this.tcpListener = new TcpListener(new IPAddress(0), port);
            this.tcpListener.Start();

            Log("Started at {0}", this.tcpListener.Server.LocalEndPoint);

            ThreadPool.QueueUserWorkItem(WorkThread);
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

        private void WorkThread(object state)
        {
            while (!this.isClosed)
            {
                try
                {
                    var tcpClient = this.tcpListener.AcceptTcpClient();
                    var client = new DemoClient(tcpClient);

                    lock (this.clientsLock)
                    {
                        this.clients.Add(client);
                    }
                }
                catch (SocketException ex)
                {
                    Log("Disconnect {0}", ex.Message);

                    break;
                }
            }
        }
    }
}
