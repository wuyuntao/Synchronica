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

using Synchronica.Simulation.Data;
using System;
using System.Collections.Generic;

namespace Synchronica.Simulation
{
    public sealed class Scene
    {
        private int nextObjectId = 1;
        private List<GameObject> objects = new List<GameObject>();
        private int milliseconds;

        public SceneData GetData(int startMilliseconds, int endMilliseconds)
        {
            SceneData data = null;

            foreach (var obj in this.objects)
            {
                var objectData = obj.GetData(startMilliseconds, endMilliseconds);
                if (objectData != null)
                {
                    if (data == null)
                        data = new SceneData(startMilliseconds, endMilliseconds);

                    data.AddObject(objectData);
                }
            }

            return data;
        }

        public GameObject CreateObject()
        {
            var gameObject = new GameObject(this, this.nextObjectId++);

            this.objects.Add(gameObject);

            return gameObject;
        }

        public void IncreaseMilliseconds(int milliseconds)
        {
            if (milliseconds <= 0)
                throw new ArgumentException("milliseconds must > 0");

            this.milliseconds += milliseconds;
        }

        public int Milliseconds
        {
            get { return this.milliseconds; }
        }
    }
}
