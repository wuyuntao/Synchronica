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
    public sealed class Recorder
    {
        private Scene scene = new Scene();
        private List<SceneData> frames = new List<SceneData>();
        private int nextStartMilliseconds = 0;

        public void CreateDataFrame(int duration)
        {
            if (duration <= 0)
                throw new ArgumentException("duration must > 0");

            var startMilliseconds = this.nextStartMilliseconds;
            var endMilliseconds = this.nextStartMilliseconds + duration;

            var data = scene.GetData(startMilliseconds, endMilliseconds);
            if (data != null)
                this.frames.Add(data);

            this.nextStartMilliseconds = endMilliseconds + 1;
        }

        public SynchronicaData GetDataFrames(int startMilliseconds)
        {
            SynchronicaData data = null;
            foreach (var frame in this.frames)
            {
                if (frame.StartMilliseconds < startMilliseconds)
                    continue;

                if (data == null)
                    data = new SynchronicaData();

                data.AddScene(frame);
            }

            return data;
        }

        public Scene Scene
        {
            get { return this.scene; }
        }
    }
}
