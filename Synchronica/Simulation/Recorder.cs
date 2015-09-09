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

using Synchronica.Simulation.KeyFrames;
using Synchronica.Simulation.Variables;
using System;
using System.Collections.Generic;

namespace Synchronica.Simulation
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

        public Actor AddActor(int startTime)
        {
            if (startTime < this.scene.ElapsedTime)
                throw new ArgumentException("Cannot create actor before lock time");

            var actor = new Actor(this.scene, GetNextActorId(), startTime);
            this.scene.AddActor(actor);
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

        #region Variable

        public Variable<bool> AddBoolean(Actor actor, int id, bool value)
        {
            if (actor.StartTime < this.scene.ElapsedTime)
                throw new InvalidOperationException("Cannot add variable after actor starts");

            var variable = new VBoolean(actor, id, value);
            actor.AddVariable(variable);
            return variable;
        }

        public Variable<short> AddInt16(Actor actor, int id, short value)
        {
            if (actor.StartTime < this.scene.ElapsedTime)
                throw new InvalidOperationException("Cannot add variable after actor starts");

            var variable = new VInt16(actor, id, value);
            actor.AddVariable(variable);
            return variable;
        }

        public Variable<int> AddInt32(Actor actor, int id, int value)
        {
            if (actor.StartTime < this.scene.ElapsedTime)
                throw new InvalidOperationException("Cannot add variable after actor starts");

            var variable = new VInt32(actor, id, value);
            actor.AddVariable(variable);
            return variable;
        }

        public Variable<long> AddInt64(Actor actor, int id, long value)
        {
            if (actor.StartTime < this.scene.ElapsedTime)
                throw new InvalidOperationException("Cannot add variable after actor starts");

            var variable = new VInt64(actor, id, value);
            actor.AddVariable(variable);
            return variable;
        }

        public Variable<float> AddFloat(Actor actor, int id, float value)
        {
            if (actor.StartTime < this.scene.ElapsedTime)
                throw new InvalidOperationException("Cannot add variable after actor starts");

            var variable = new VFloat(actor, id, value);
            actor.AddVariable(variable);
            return variable;
        }

        #endregion

        #region KeyFrame

        public void AddLinearFrame<TValue>(Variable<TValue> variable, int time, TValue value)
        {
            var v = variable as ILinearKeyFrameVariable<TValue>;
            if (v == null)
                throw new ArgumentException("Cannot add linear key frame");

            v.AddLinearFrame(time, value);
        }

        public void AddPulseFrame<TValue>(Variable<TValue> variable, int time, TValue value)
        {
            var v = variable as IPulseKeyFrameVariable<TValue>;
            if (v == null)
                throw new ArgumentException("Cannot add pulse key frame");

            v.AddPulseFrame(time, value);
        }

        public void AddStepFrame<TValue>(Variable<TValue> variable, int time, TValue value)
        {
            var v = variable as IStepKeyFrameVariable<TValue>;
            if (v == null)
                throw new ArgumentException("Cannot add step key frame");

            v.AddStepFrame(time, value);
        }

        public void InterpolateKeyFrame<TValue>(Variable<TValue> variable, int time)
        {
            variable.Interpolate(time);
        }

        public void RemoveKeyFramesAfter<TValue>(Variable<TValue> variable, int time)
        {
            variable.RemoveFramesAfter(time);
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
