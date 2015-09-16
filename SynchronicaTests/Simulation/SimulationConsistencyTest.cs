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

using NUnit.Framework;
using Synchronica.Simulation;
using Synchronica.Tests.Mock;
using System;
using System.Linq;

namespace Synchronica.Tests.Simulation
{
    public class SimulationConsistencyTest
    {
        const int TotalRunTime = 100000;
        const int RecordInterval = 100;
        const int ReplayInterval = 10;
        const double CreateActorOdds = 0.5;
        const double ChangeActorOdds = 0.8;
        const double RemoveActorOdds = 0.3;

        [Test]
        public void TestConsistency()
        {
            var random = new Random(1);
            var recorder = new Recorder();
            var replayer = new Replayer();

            for (var time = 0; time < TotalRunTime; time += RecordInterval)
            {
                var data = RandomRecord(random, recorder, time);

                replayer.Replay(time, time + RecordInterval, data);

                for (var frameTime = time; frameTime < time + RecordInterval; frameTime += ReplayInterval)
                {
                    var recorderFrame = GetFrameData(recorder.Scene, frameTime);
                    var replayerFrame = GetFrameData(replayer.Scene, frameTime);

                    CompareFrameData(recorderFrame, replayerFrame);
                }
            }
        }

        private static RecorderData RandomRecord(Random random, Recorder recorder, int time)
        {
            // Destroy an old actor randomly
            if (random.NextDouble() < RemoveActorOdds)
            {
                var endTime = random.Next(time, time + RecordInterval);
                var actor = recorder.Scene.Actors.FirstOrDefault();

                if (actor != null && actor.EndTime < 0)
                {
                    recorder.RemoveActor(actor, endTime);
                }
            }

            // Move exising actor randomly
            foreach (var actor in recorder.Scene.Actors)
            {
                if (random.NextDouble() >= ChangeActorOdds)
                    continue;

                var x = actor.GetVariable<float>(1);
                var y = actor.GetVariable<float>(2);
                var z = actor.GetVariable<float>(3);
                var keyFrameStartTime = random.Next(time, time + RecordInterval);
                var keyFrameEndTime = keyFrameStartTime + random.Next(1, 1000);

                switch (random.Next(0, 4))
                {
                    case 1:     // Linear movement
                        recorder.AddLine(x, keyFrameStartTime, keyFrameEndTime, (float)(random.NextDouble() * 100));
                        recorder.AddLine(y, keyFrameStartTime, keyFrameEndTime, (float)(random.NextDouble() * 100));
                        recorder.AddLine(z, keyFrameStartTime, keyFrameEndTime, (float)(random.NextDouble() * 100));
                        break;

                    case 2:     // Step movement
                        recorder.AddStep(x, keyFrameStartTime, keyFrameEndTime, (float)(random.NextDouble() * 100));
                        recorder.AddStep(y, keyFrameStartTime, keyFrameEndTime, (float)(random.NextDouble() * 100));
                        recorder.AddStep(z, keyFrameStartTime, keyFrameEndTime, (float)(random.NextDouble() * 100));
                        break;

                    default:     // No movement;
                        break;
                }
            }

            // Create a new actor randomly
            if (random.NextDouble() < CreateActorOdds)
            {
                var startTime = random.Next(time, time + RecordInterval);
                var actor = recorder.AddActor(startTime, f =>
                {
                    f.AddFloat(1, (float)(random.NextDouble() * 10));
                    f.AddFloat(2, (float)(random.NextDouble() * 10));
                    f.AddFloat(3, (float)(random.NextDouble() * 10));
                });
            }

            return recorder.Record(time + RecordInterval);
        }

        private static FrameData GetFrameData(Scene scene, int time)
        {
            return new FrameData()
            {
                Actors = (from actor in scene.Actors
                          orderby actor.Id
                          select GetActorData(actor, time) into data
                          where data != null
                          select data).ToArray(),
                Time = time,
            };
        }

        private static ActorData GetActorData(Actor actor, int time)
        {
            if (time < actor.StartTime)
                return null;

            if (time > actor.EndTime)
                return null;

            return new ActorData()
            {
                ActorId = actor.Id,
                StartTime = actor.StartTime,
                EndTime = actor.EndTime,
                X = actor.GetVariable<float>(1).GetValue(time),
                Y = actor.GetVariable<float>(2).GetValue(time),
                Z = actor.GetVariable<float>(3).GetValue(time),
            };
        }

        private static void CompareFrameData(FrameData recorderFrame, FrameData replayerFrame)
        {
            Assert.AreEqual(recorderFrame.Time, replayerFrame.Time);
            Assert.AreEqual(recorderFrame.Actors.Length, replayerFrame.Actors.Length);

            for (int i = 0; i < recorderFrame.Actors.Length; i++)
            {
                var recorderActor = recorderFrame.Actors[i];
                var replayerActor = replayerFrame.Actors[i];

                Assert.AreEqual(recorderActor.ActorId, replayerActor.ActorId);
                Assert.AreEqual(recorderActor.StartTime, replayerActor.StartTime);
                Assert.AreEqual(recorderActor.EndTime, replayerActor.EndTime);
                Assert.AreEqual(recorderActor.X, replayerActor.X);
                Assert.AreEqual(recorderActor.Y, replayerActor.Y);
                Assert.AreEqual(recorderActor.Z, replayerActor.Z);
            }
        }

        #region Data

        class FrameData
        {
            public int Time;
            public ActorData[] Actors;
        }

        class ActorData
        {
            public int ActorId;

            public int StartTime;
            public int EndTime;

            public float X;
            public float Y;
            public float Z;
        }

        #endregion
    }
}
