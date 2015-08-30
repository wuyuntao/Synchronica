﻿/*
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
    public abstract class Recorder<TData>
    {
        private int lastObjectId = 0;
        private Scene scene = new Scene();

        private int GetNextObjectId()
        {
            return ++this.lastObjectId;
        }

        #region GameObject

        public GameObject AddObject(int startTime)
        {
            if (startTime < this.scene.ElapsedTime)
                throw new ArgumentException("Cannot create object before lock time");

            var gameObject = new GameObject(this.scene, GetNextObjectId(), startTime);
            this.scene.AddObject(gameObject);
            return gameObject;
        }

        public void RemoveObject(GameObject gameObject, int endTime)
        {
            gameObject.Destroy(endTime);
        }

        public GameObject GetObject(int id)
        {
            return this.scene.GetObject(id);
        }

        #endregion

        #region Variable

        public Variable<bool> AddBoolean(GameObject gameObject, bool value)
        {
            return gameObject.AddBoolean(GetNextObjectId(), value);
        }

        public Variable<short> AddInt16(GameObject gameObject, short value)
        {
            return gameObject.AddInt16(GetNextObjectId(), value);
        }

        public Variable<int> AddInt32(GameObject gameObject, int value)
        {
            return gameObject.AddInt32(GetNextObjectId(), value);
        }

        public Variable<long> AddInt64(GameObject gameObject, long value)
        {
            return gameObject.AddInt64(GetNextObjectId(), value);
        }

        public Variable<float> AddFloat(GameObject gameObject, float value)
        {
            return gameObject.AddFloat(GetNextObjectId(), value);
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

        #endregion

        #region Record

        public TData Record(int time)
        {
            if (time < this.scene.ElapsedTime)
                throw new ArgumentException("Cannot create record before lock time");

            var data = (TData)SerializeRecord(time);

            if (data != null)
            {
                // TODO Remove obsolete objects?
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

        public IEnumerable<GameObject> Objects
        {
            get { return this.scene.Objects; }
        }
    }
}