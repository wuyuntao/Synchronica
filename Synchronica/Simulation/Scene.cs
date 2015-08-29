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

using System;
using System.Collections.Generic;

namespace Synchronica.Simulation
{
    sealed class Scene
    {
        private int lockTime;
        private Dictionary<int, GameObject> objects = new Dictionary<int, GameObject>();

        internal void Lock(int time)
        {
            if (time <= this.lockTime)
                throw new ArgumentException("Already locked");

            this.lockTime = time;
        }

        public GameObject GetObject(int id)
        {
            GameObject obj;
            this.objects.TryGetValue(id, out obj);
            return obj;
        }

        internal void AddObject(GameObject gameObject)
        {
            this.objects.Add(gameObject.Id, gameObject);
        }

        internal void RemoveObject(GameObject gameObject)
        {
            this.objects.Remove(gameObject.Id);
        }

        public int LockTime
        {
            get { return this.lockTime; }
        }

        public IEnumerable<GameObject> Objects
        {
            get { return this.objects.Values; }
        }

        public int Count
        {
            get { return this.objects.Count; }
        }
    }
}
