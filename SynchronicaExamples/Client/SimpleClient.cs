using FlatBuffers;
using FlatBuffers.Schema;
using NLog;
using Synchronica.Examples.Schema;
using Synchronica.Replayers;
using Synchronica.Schema;
using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace Synchronica.Examples.Client
{
    class SimpleClient
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private string name;

        private TcpClient tcpClient;

        private NetworkStream networkStream;

        private int objectId;

        private FlatBufferReplayer replayer = new FlatBufferReplayer();

        private object replayerLock = new object();

        private int elapsedTime;

        public SimpleClient(string name, string hostname, int port)
        {
            this.name = name;
            this.tcpClient = new TcpClient();
            this.tcpClient.Connect(hostname, port);
            this.networkStream = this.tcpClient.GetStream();

            logger.Info("Connected to {0}:{1}", hostname, port);

            ThreadPool.QueueUserWorkItem(ReadThread);
            //ThreadPool.QueueUserWorkItem(OutputThread);
        }

        public override string ToString()
        {
            return string.Format("{0}-{1}", GetType().Name, this.objectId);
        }

        public void Disconnect()
        {
            this.tcpClient.Close();
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
                int readSize;
                try
                {
                    readSize = this.networkStream.Read(buffer, 0, buffer.Length);
                }
                catch (IOException)
                {
                    readSize = 0;
                }

                if (readSize == 0)
                {
                    logger.Info("Disconnected");
                    break;
                }

                var bytes = new byte[readSize];
                Array.Copy(buffer, bytes, readSize);

                logger.Info("Received {0} bytes", readSize);

                processor.Enqueue(bytes);
                processor.Process();
            }
        }

        private void OutputThread(object state)
        {
            while (this.networkStream.CanRead)
            {
                lock (this.replayerLock)
                {
                    for (; this.elapsedTime < this.replayer.Scene.ElapsedTime; this.elapsedTime += 10)
                    {
                        foreach (var actor in this.replayer.Scene.Actors)
                        {
                            if (actor.Id != 1)
                                continue;

                            var posX = actor.GetVariable<float>(1);
                            var posY = actor.GetVariable<float>(2);
                            var posZ = actor.GetVariable<float>(3);

                            logger.Debug("Actor {0} moves to ({1}, {2}, {3}) at {4}ms",
                                    actor.Id,
                                    posX.GetValue(this.elapsedTime),
                                    posY.GetValue(this.elapsedTime),
                                    posZ.GetValue(this.elapsedTime),
                                    this.elapsedTime);
                        }
                    }
                }

                Thread.Sleep(10);
            }
        }

        private void OnLoginResponse(Message msg)
        {
            var res = (LoginResponse)msg.Body;

            this.objectId = res.ObjectId;

            logger.Info("Login succeeded: {0}", this.objectId);
        }

        private void OnSynchronizeSceneData(Message msg)
        {
            var data = (SynchronizeSceneData)msg.Body;
            logger.Info("Received SynchronizeSceneData: {0} -> {1}", data.StartTime, data.EndTime);

            lock (this.replayerLock)
            {
                this.replayer.Replay(data.StartTime, data.EndTime, data);
            }
        }

        public void Login()
        {
            var fbb = new FlatBufferBuilder(1024);

            var oName = fbb.CreateString(this.name);
            var oLogin = LoginRequest.CreateLoginRequest(fbb, oName);
            LoginRequest.FinishLoginRequestBuffer(fbb, oLogin);

            WriteBytes(FlatBufferExtensions.ToProtocolMessage(fbb, ClientMessageIds.LoginRequest));

            logger.Info("Login");
        }

        public void Input(Command command)
        {
            var time = this.replayer.Scene.ElapsedTime;
            var fbb = new FlatBufferBuilder(1024);

            var oInput = InputRequest.CreateInputRequest(fbb, time, command);
            InputRequest.FinishInputRequestBuffer(fbb, oInput);

            WriteBytes(FlatBufferExtensions.ToProtocolMessage(fbb, ClientMessageIds.InputRequest));

            logger.Info("Input {0} {1}ms", command, time);
        }

        private void WriteBytes(byte[] bytes)
        {
            logger.Info("Send {0} bytes", bytes.Length);

            ThreadPool.QueueUserWorkItem(s => this.networkStream.Write(bytes, 0, bytes.Length));
        }
    }
}
