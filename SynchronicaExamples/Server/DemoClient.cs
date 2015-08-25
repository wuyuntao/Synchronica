using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace Synchronica.Examples.Server
{
    class DemoClient : LogObject
    {
        private TcpClient tcpClient;

        public DemoClient(TcpClient tcpClient)
        {
            this.tcpClient = tcpClient;

            Log("Connected from {0}", tcpClient.Client.RemoteEndPoint);
        }

        public override string ToString()
        {
            return string.Format("{0}", GetType().Name);
        }
    }
}
