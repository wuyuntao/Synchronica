using FlatBuffers.Schema;
using Synchronica.Examples.Schema;
using Synchronica.Recorders;
using Synchronica.Simulation;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Synchronica.Examples.Scene
{
    class SimpleScene
    {
        private FlatBufferRecorder recorder = new FlatBufferRecorder();

        private List<Cube> cubes = new List<Cube>();

        private object cubesLock = new object();

        private List<Input> inputs = new List<Input>();

        private object inputsLock = new object();

        private Stopwatch stopwatch = Stopwatch.StartNew();

        #region Cube

        class Cube
        {
            public FlatBufferRecorder recorder;
            public GameObject gameObject;
            public string clientName;

            public Cube(FlatBufferRecorder recorder, GameObject gameObject)
            {
                this.recorder = recorder;
                this.gameObject = gameObject;
            }

            internal void Forward(int time)
            {
                throw new NotImplementedException();
            }

            internal void Back(int time)
            {
                throw new NotImplementedException();
            }

            internal void TurnLeft(int time)
            {
                throw new NotImplementedException();
            }

            internal void TurnRight(int time)
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region Input

        class Input
        {
            public int objectId;
            public int time;
            public Command command;

            public Input(int objectId, int time, Command command)
            {
                this.objectId = objectId;
                this.time = time;
                this.command = command;
            }
        }

        #endregion

        public SimpleScene()
        {
            CreateCube(10, 5, 10);
            CreateCube(-10, 5, 10);
            CreateCube(-10, 5, -10);
            CreateCube(10, 5, -10);
        }

        private void CreateCube(float posX, float posY, float posZ)
        {
            var gameObject = recorder.AddObject(0);
            recorder.AddFloat(gameObject, posX);
            recorder.AddFloat(gameObject, posY);
            recorder.AddFloat(gameObject, posZ);

            var cube = new Cube(this.recorder, gameObject);
            this.cubes.Add(cube);
        }

        public byte[] Process()
        {
            var startTime = (int)stopwatch.ElapsedMilliseconds;
            var endTime = startTime + 100;
            var pendingInputs = GetPendingInputs(endTime);

            if (pendingInputs != null)
            {
                foreach (var input in pendingInputs)
                {
                    Cube cube;
                    lock (this.cubesLock)
                    {
                        cube = this.cubes.Find(c => c.gameObject.Id == input.objectId);
                    }

                    if (cube == null)
                        throw new InvalidOperationException("Invalid object id");

                    ProcessCommand(cube, input.command, Math.Max(input.time, startTime));
                }

                var fbb = this.recorder.Record(endTime);
                if (fbb != null)
                {
                    return fbb.ToProtocolMessage(ServerMessageIds.SynchronizeSceneData);
                }
            }

            return null;
        }

        private IEnumerable<SimpleScene.Input> GetPendingInputs(int endTime)
        {
            var pendingInputs = new List<Input>();
            lock (this.inputsLock)
            {
                this.inputs.RemoveAll(input =>
                {
                    if (input.time <= endTime)
                    {
                        pendingInputs.Add(input);

                        return true;
                    }
                    else
                        return false;
                });
            }
            return pendingInputs;
        }

        private static void ProcessCommand(Cube cube, Command command, int time)
        {
            switch (command)
            {
                case Command.Up:
                    cube.Forward(time);
                    break;

                case Command.Down:
                    cube.Back(time);
                    break;

                case Command.Left:
                    cube.TurnLeft(time);
                    break;

                case Command.Right:
                    cube.TurnRight(time);
                    break;

                default:
                    throw new NotSupportedException();
            }
        }

        public int AllocateCube(string clientName)
        {
            lock (this.cubesLock)
            {
                var cube = this.cubes.Find(c => c.clientName == null);
                if (cube != null)
                {
                    cube.clientName = clientName;
                    return cube.gameObject.Id;
                }
                else
                {
                    return 0;
                }
            }
        }

        public void AddInput(int objectId, int time, Command command)
        {
            lock (this.inputsLock)
            {
                this.inputs.Add(new Input(objectId, time, command));
            }
        }
    }
}
