using FlatBuffers;
using FlatBuffers.Schema;
using Synchronica.Examples.Schema;
using Synchronica.Schema;
using System;
using System.Net.Sockets;
using System.Threading;

namespace Synchronica.Examples.Client
{
    class SimpleClient : LogObject
    {
        private string name;

        private TcpClient tcpClient;

        private NetworkStream networkStream;

        private int objectId;

        public SimpleClient(string name, string hostname, int port)
        {
            this.name = name;
            this.tcpClient = new TcpClient();
            this.tcpClient.Connect(hostname, port);
            this.networkStream = this.tcpClient.GetStream();

            Log("Connected to {0}:{1}", hostname, port);

            ThreadPool.QueueUserWorkItem(ReadThread);
        }

        public override string ToString()
        {
            return string.Format("{0}-{1}", GetType().Name, this.objectId);
        }

        private void ReadThread(object state)
        {
            var schema = new MessageSchema();
            schema.Register(ServerMessageIds.LoginResponse, LoginResponse.GetRootAsLoginResponse);
            schema.Register(ServerMessageIds.SynchronizeSceneData, SynchronizeSceneData.GetRootAsSynchronizeSceneData);

            var processor = new MessageProcessor(schema);
            processor.Attach((int)ServerMessageIds.LoginResponse, OnLoginResponse);
            processor.Attach((int)ServerMessageIds.SynchronizeSceneData, OnSynchronizeSceneData);

            var buffer = new byte[this.tcpClient.ReceiveBufferSize];

            while (this.networkStream.CanRead)
            {
                var readSize = this.networkStream.Read(buffer, 0, buffer.Length);

                var bytes = new byte[readSize];
                Array.Copy(buffer, bytes, readSize);

                Log("Received {0} bytes", readSize);

                processor.Enqueue(bytes);
                processor.Process();
            }
        }

        private void OnLoginResponse(Message msg)
        {
            var res = (LoginResponse)msg.Body;

            this.objectId = res.ObjectId;

            Log("Login succeeded: {0}", this.objectId);
        }

        private void OnSynchronizeSceneData(Message msg)
        {
            throw new NotImplementedException();
        }

        public void Login()
        {
            var fbb = new FlatBufferBuilder(1024);

            var oName = fbb.CreateString(this.name);
            var oLogin = LoginRequest.CreateLoginRequest(fbb, oName);
            LoginRequest.FinishLoginRequestBuffer(fbb, oLogin);

            WriteBytes(FlatBufferExtensions.ToProtocolMessage(fbb, ClientMessageIds.LoginRequest));

            Log("Login");
        }

        public void Input(int milliseconds, Command command)
        {
            var fbb = new FlatBufferBuilder(1024);

            var oInput = InputRequest.CreateInputRequest(fbb, milliseconds, command);

            WriteBytes(FlatBufferExtensions.ToProtocolMessage(fbb, ClientMessageIds.InputRequest));

            Log("Input {0} {1}ms", command, milliseconds);
        }

        private void WriteBytes(byte[] bytes)
        {
            Log("Send {0} bytes", bytes.Length);

            ThreadPool.QueueUserWorkItem(s => this.networkStream.Write(bytes, 0, bytes.Length));
        }
    }
}
