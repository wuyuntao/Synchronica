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
using System;
using System.Collections.Generic;
using System.Linq;

namespace Synchronica.Record
{
    public abstract class Recorder<TData>
    {
        private int lastRecordTime = 0;
        private int lastObjectId = 0;
        private GameObjectManager<RecorderGameObject> objects = new GameObjectManager<RecorderGameObject>();

        public GameObject AddObject(int startTime)
        {
            if (startTime < this.lastRecordTime)
                throw new ArgumentException("Cannot create object before last record time");

            var gameObject = new RecorderGameObject(++this.lastObjectId, startTime);
            this.objects.AddObject(gameObject);
            return gameObject;
        }

        public TData Record(int time)
        {
            if (time <= this.lastRecordTime)
                throw new ArgumentException("Cannot create record before last record time");

            var data = (TData)Serialize(this.lastRecordTime + 1, time);

            if (data != null)
            {
                // TODO Remove obsolete objects?

                this.lastRecordTime = time;
            }

            return data;
        }

        protected abstract TData Serialize(int startTime, int endTime);

        public int LastRecordTime
        {
            get { return this.lastObjectId; }
        }

        public IEnumerable<GameObject> Objects
        {
            get
            {
                return from obj in this.objects.Objects
                       select (GameObject)obj;
            }
        }
    }
}
