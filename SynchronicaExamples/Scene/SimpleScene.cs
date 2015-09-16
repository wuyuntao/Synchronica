using FlatBuffers.Schema;
using NLog;
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
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private FlatBufferRecorder recorder = new FlatBufferRecorder();

        private List<Cube> cubes = new List<Cube>();

        private object cubesLock = new object();

        private List<Input> inputs = new List<Input>();

        private object inputsLock = new object();

        private Stopwatch stopwatch = Stopwatch.StartNew();

        private List<byte[]> sceneData = new List<byte[]>();

        private object sceneDataLock = new object();

        #region Cube

        class Cube
        {
            private static Logger logger = LogManager.GetCurrentClassLogger();

            public SimpleScene scene;
            public Actor actor;
            public string clientName;

            private Variable<float> posX;
            private Variable<float> posY;
            private Variable<float> posZ;

            public Cube(SimpleScene scene, float posX, float posY, float posZ)
            {
                this.scene = scene;

                this.actor = this.scene.recorder.AddActor(0);
                this.posX = this.scene.recorder.AddFloat(actor, 1, posX);
                this.posY = this.scene.recorder.AddFloat(actor, 2, posY);
                this.posZ = this.scene.recorder.AddFloat(actor, 3, posZ);

                logger.Debug("Cube #{0} created: Pos: ({1}, {2}, {3}), Time: {4}",
                        this.actor.Id,
                        this.posX.GetValue(this.actor.StartTime),
                        this.posY.GetValue(this.actor.StartTime),
                        this.posZ.GetValue(this.actor.StartTime),
                        this.actor.StartTime);
            }

            internal void Forward(int time)
            {
                var recorder = this.scene.recorder;
                recorder.InterpolateKeyFrame(this.posZ, time);
                recorder.RemoveKeyFramesAfter(this.posZ, time + 1);

                var value = this.posZ.GetValue(time);
                recorder.AddLinearFrame(this.posZ, time + 1000, value + 1);

                logger.Debug("Cube #{0} forward: Z: {1} -> {2}, Time: {3} -> {4}",
                        this.actor.Id, value, value + 1, time, time + 1000);
            }

            internal void Back(int time)
            {
                var recorder = this.scene.recorder;
                recorder.InterpolateKeyFrame(this.posZ, time);
                recorder.RemoveKeyFramesAfter(this.posZ, time + 1);

                var value = this.posZ.GetValue(time) - 1;
                recorder.AddLinearFrame(this.posZ, time + 1000, value);

                logger.Debug("Cube #{0} back: Z: {1}, Time: {2}",
                        this.actor.Id, value, time + 1000);
            }

            internal void TurnLeft(int time)
            {
                var recorder = this.scene.recorder;
                recorder.InterpolateKeyFrame(this.posX, time);
                recorder.RemoveKeyFramesAfter(this.posX, time + 1);

                var value = this.posX.GetValue(time) - 1;
                recorder.AddLinearFrame(this.posX, time + 1000, value);

                logger.Debug("Cube #{0} left: X: {1}, Time: {2}",
                        this.actor.Id, value, time + 1000);
            }

            internal void TurnRight(int time)
            {
                var recorder = this.scene.recorder;
                recorder.InterpolateKeyFrame(this.posX, time);
                recorder.RemoveKeyFramesAfter(this.posX, time + 1);

                var value = this.posX.GetValue(time) + 1;
                recorder.AddLinearFrame(this.posX, time + 1000, value);

                logger.Debug("Cube #{0} right: X: {1}, Time: {2}",
                        this.actor.Id, value, time + 1000);
            }
        }

        #endregion

        #region Input

        class Input
        {
            public int actorId;
            public int time;
            public Command command;

            public Input(int actorId, int time, Command command)
            {
                this.actorId = actorId;
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
            var cube = new Cube(this, posX, posY, posZ);
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
                        cube = this.cubes.Find(c => c.actor.Id == input.actorId);
                    }

                    if (cube == null)
                        throw new InvalidOperationException("Invalid actor id");

                    ProcessCommand(cube, input.command, startTime);
                }

                var fbb = this.recorder.Record(endTime);
                if (fbb != null)
                {
                    var data = fbb.ToProtocolMessage(ServerMessageIds.SynchronizeSceneData);

                    lock (this.sceneDataLock)
                    {
                        this.sceneData.Add(data);
                    }

                    return data;
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
                    return cube.actor.Id;
                }
                else
                {
                    return 0;
                }
            }
        }

        public void AddInput(int actorId, int time, Command command)
        {
            lock (this.inputsLock)
            {
                this.inputs.Add(new Input(actorId, time, command));
            }
        }

        public IEnumerable<byte[]> GetSceneData()
        {
            lock (this.sceneDataLock)
            {
                return this.sceneData.ToArray();
            }
        }
    }
}
