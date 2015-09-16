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

using Synchronica.Simulation;
using Synchronica.Simulation.KeyFrames;
using System;
using System.Collections.Generic;

namespace Synchronica.Recorders
{
    public abstract class Recorder<TData>
    {
        private int lastActorId = 0;
        private Scene scene = new Scene();

        private int GetNextActorId()
        {
            return ++this.lastActorId;
        }

        #region Actor

        public Actor AddActor(int startTime, Action<ActorFactory> initializer)
        {
            if (startTime < this.scene.ElapsedTime)
                throw new ArgumentException("Cannot create actor before lock time");

            var actor = new Actor(this.scene, GetNextActorId(), startTime);

            this.scene.AddActor(actor);
            ActorFactory.Initiailize(actor, initializer);

            return actor;
        }

        public void RemoveActor(Actor actor, int endTime)
        {
            actor.Destroy(endTime);
        }

        public Actor GetActor(int id)
        {
            return this.scene.GetActor(id);
        }

        #endregion

        #region KeyFrame

        public void AddLine<TValue>(Variable<TValue> variable, int startTime, int endTime, TValue endValue)
        {
            var v = variable as ILinearKeyFrameVariable<TValue>;
            if (v == null)
                throw new ArgumentException("Cannot add linear key frame");

            variable.Interpolate(startTime);
            variable.RemoveFramesAfter(startTime + 1);

            v.AddLinearFrame(endTime, endValue);

            ((RecordState)variable.State).OnChange(startTime);
        }

        public void AddPulse<TValue>(Variable<TValue> variable, int startTime, int endTime, TValue endValue)
        {
            var v = variable as IPulseKeyFrameVariable<TValue>;
            if (v == null)
                throw new ArgumentException("Cannot add pulse key frame");

            variable.Interpolate(startTime);
            variable.RemoveFramesAfter(startTime + 1);

            v.AddPulseFrame(endTime, endValue);

            ((RecordState)variable.State).OnChange(startTime);
        }

        public void AddStep<TValue>(Variable<TValue> variable, int startTime, int endTime, TValue endValue)
        {
            var v = variable as IStepKeyFrameVariable<TValue>;
            if (v == null)
                throw new ArgumentException("Cannot add step key frame");

            variable.Interpolate(startTime);
            variable.RemoveFramesAfter(startTime + 1);

            v.AddStepFrame(endTime, endValue);

            ((RecordState)variable.State).OnChange(startTime);
        }

        protected IEnumerable<KeyFrame> GetKeyFrameChanges(Variable variable)
        {
            var state = (RecordState)variable.State;

            if (state.HasChanges)
            {
                var frames = variable.FindFramesAfter(state.FirstChangeTime);

                foreach (var frame in frames)
                    yield return frame;
            }

            state.OnResetChange(variable.LastFrame.Time);
        }

        #endregion

        #region Record

        public TData Record(int time)
        {
            if (time < this.scene.ElapsedTime)
                throw new ArgumentException("Cannot create record before lock time");

            var data = (TData)SerializeRecord(time);

            if (data != null)
            {
                // TODO Remove obsolete actors?
                this.scene.ElapsedTime = time;
            }

            return data;
        }

        protected abstract TData SerializeRecord(int endTime);

        #endregion

        public Scene Scene
        {
            get { return this.scene; }
        }

        public IEnumerable<Actor> Actors
        {
            get { return this.scene.Actors; }
        }
    }
}
