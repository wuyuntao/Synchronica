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
    public delegate void NewActorEventHandler(Actor actor);

    public abstract class Replayer<TData>
    {
        public event NewActorEventHandler OnNewActor;

        private Scene scene = new Scene();

        #region Actor

        protected Actor AddActor(int id, int startTime)
        {
            if (startTime < this.scene.ElapsedTime)
                throw new ArgumentException("Cannot create actor before last replay time");

            var actor = new Actor(this.scene, id, startTime);
            this.scene.AddActor(actor);

            if (OnNewActor != null)
                OnNewActor(actor);

            return actor;
        }

        protected void RemoveActor(Actor actor, int endTime)
        {
            actor.Destroy(endTime);
        }

        public Actor GetActor(int id)
        {
            return this.scene.GetActor(id);
        }

        #endregion

        #region Variable

        protected Variable<bool> AddBoolean(Actor actor, int id)
        {
            var variable = new VBoolean(actor, id);
            actor.AddVariable(variable);
            return variable;
        }

        protected Variable<short> AddInt16(Actor actor, int id)
        {
            var variable = new VInt16(actor, id);
            actor.AddVariable(variable);
            return variable;
        }

        protected Variable<int> AddInt32(Actor actor, int id)
        {
            var variable = new VInt32(actor, id);
            actor.AddVariable(variable);
            return variable;
        }

        protected Variable<long> AddInt64(Actor actor, int id)
        {
            var variable = new VInt64(actor, id);
            actor.AddVariable(variable);
            return variable;
        }

        protected Variable<float> AddFloat(Actor actor, int id)
        {
            var variable = new VFloat(actor, id);
            actor.AddVariable(variable);
            return variable;
        }

        #endregion

        #region KeyFrame

        protected void AddLinearFrame<TValue>(Variable<TValue> variable, int time, TValue value)
        {
            var v = variable as ILinearKeyFrameVariable<TValue>;
            if (v == null)
                throw new ArgumentException("Cannot add linear key frame");

            v.AddLinearFrame(time, value);
        }

        protected void AddPulseFrame<TValue>(Variable<TValue> variable, int time, TValue value)
        {
            var v = variable as IPulseKeyFrameVariable<TValue>;
            if (v == null)
                throw new ArgumentException("Cannot add pulse key frame");

            v.AddPulseFrame(time, value);
        }

        protected void AddStepFrame<TValue>(Variable<TValue> variable, int time, TValue value)
        {
            var v = variable as IStepKeyFrameVariable<TValue>;
            if (v == null)
                throw new ArgumentException("Cannot add step key frame");

            v.AddStepFrame(time, value);
        }


        protected void RemoveFramesAfter(Variable variable, int startTime)
        {
            if (!variable.IsNew)
                variable.RemoveFramesAfter(startTime);
        }

        #endregion

        #region Replay

        public void Replay(int startTime, int endTime, TData data)
        {
            if (startTime != this.scene.ElapsedTime)
                throw new ArgumentException("Cannot replay data before last replay time");

            if (data != null)
            {
                DeserializeRecord(data);
            }

            this.scene.ElapsedTime = endTime;
        }

        protected abstract void DeserializeRecord(TData data);

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
