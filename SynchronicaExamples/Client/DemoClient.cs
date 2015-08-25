using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Synchronica.Examples.Client
{
    class DemoClient : LogObject
    {
        private TcpClient tcpClient;

        private NetworkStream networkStream;

        public DemoClient(string hostname, int port)
        {
            this.tcpClient = new TcpClient();
            this.tcpClient.Connect(hostname, port);
            this.networkStream = this.tcpClient.GetStream();

            Log("Connected to {0}:{1}", hostname, port);

            ThreadPool.QueueUserWorkItem(ReadThread);
        }

        public override string ToString()
        {
            return string.Format("{0}", GetType().Name);
        }

        private void ReadThread(object state)
        {
            var buffer = new byte[this.tcpClient.ReceiveBufferSize];

            while (this.networkStream.CanRead)
            {
                var readSize = this.networkStream.Read(buffer, 0, buffer.Length);

                var bytes = new byte[readSize];
                Array.Copy(buffer, bytes, readSize);

                Log("Received {0} bytes", readSize);
            }
        }
    }
}
