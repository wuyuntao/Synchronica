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
    public abstract class GameObject
    {
        private int id;
        private int startTime;
        private int endTime;
        private List<Variable> variables = new List<Variable>();

        internal GameObject(int id, int startTime)
        {
            if (startTime < 0)
                throw new ArgumentException("Start time must >= 0");

            this.id = id;
            this.startTime = startTime;
        }

        protected void Destroy(int endTime)
        {
            if (this.endTime > 0)
                throw new ArgumentException("Already destroyed");

            if (this.endTime <= this.startTime)
                throw new ArgumentException("Cannot destroy before start");

            this.endTime = endTime;
        }

        protected void AddVariable(Variable variable)
        {
            this.variables.Add(variable);
        }

        public Variable GetVariable(int id)
        {
            return this.variables.Find(v => v.Id == id);
        }

        public int Id
        {
            get { return this.id; }
        }

        public int StartTime
        {
            get { return this.startTime; }
        }

        public int EndTime
        {
            get { return this.endTime; }
        }

        public IEnumerable<Variable> Variables
        {
            get { return this.variables; }
        }
    }
}
