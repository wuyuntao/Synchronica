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
using System;
using System.Collections.Generic;

namespace Synchronica.Simulation
{
    public abstract class Replayer<TData>
    {
        private Scene scene = new Scene();

        #region GameObject

        protected GameObject AddObject(int id, int startTime)
        {
            if (startTime < this.scene.ElapsedTime)
                throw new ArgumentException("Cannot create object before last replay time");

            var gameObject = new GameObject(this.scene, id, startTime);
            this.scene.AddObject(gameObject);
            return gameObject;
        }

        protected void RemoveObject(GameObject gameObject, int endTime)
        {
            gameObject.Destroy(endTime);
        }

        public GameObject GetObject(int id)
        {
            return this.scene.GetObject(id);
        }

        #endregion

        #region Variable

        protected Variable<bool> AddBoolean(GameObject gameObject, int id)
        {
            return gameObject.AddBoolean(id);
        }

        protected Variable<short> AddInt16(GameObject gameObject, int id)
        {
            return gameObject.AddInt16(id);
        }

        protected Variable<int> AddInt32(GameObject gameObject, int id)
        {
            return gameObject.AddInt32(id);
        }

        protected Variable<long> AddInt64(GameObject gameObject, int id)
        {
            return gameObject.AddInt64(id);
        }

        protected Variable<float> AddFloat(GameObject gameObject, int id)
        {
            return gameObject.AddFloat(id);
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

        public IEnumerable<GameObject> Objects
        {
            get { return this.scene.Objects; }
        }
    }
}
