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

using Synchronica.Simulation.Variables;
using System;
using System.Collections.Generic;

namespace Synchronica.Simulation
{
    public abstract class GameObject
    {
        private Scene scene;
        private int id;
        private int startTime;
        private int endTime;
        private List<Variable> variables = new List<Variable>();

        internal GameObject(Scene scene, int id, int startTime)
        {
            if (startTime <= scene.LockTime)
                throw new ArgumentException("Cannot create object before lock time of scene");

            this.scene = scene;
            this.id = id;
            this.startTime = startTime;
        }

        internal void Destroy(int endTime)
        {
            if (this.endTime > 0)
                throw new ArgumentException("Cannot set end time multiple time");

            if (endTime <= this.startTime)
                throw new ArgumentException("Cannot destroy before start time");

            if (endTime <= scene.LockTime)
                throw new ArgumentException("Cannot destroy object before lock time of scene");

            this.endTime = endTime;
        }

        internal VBoolean CreateBoolean(int id, bool value)
        {
            var variable = new VBoolean(id, value);
            this.variables.Add(variable);
            return variable;
        }

        internal VInt16 CreateInt16(int id, short value)
        {
            var variable = new VInt16(id, value);
            this.variables.Add(variable);
            return variable;
        }

        internal VInt32 CreateInt32(int id, int value)
        {
            var variable = new VInt32(id, value);
            this.variables.Add(variable);
            return variable;
        }

        internal VInt64 CreateInt64(int id, long value)
        {
            var variable = new VInt64(id, value);
            this.variables.Add(variable);
            return variable;
        }

        internal VFloat CreateFloat(int id, float value)
        {
            var variable = new VFloat(id, value);
            this.variables.Add(variable);
            return variable;
        }

        public Variable GetVariable(int id)
        {
            return this.variables.Find(v => v.Id == id);
        }

        internal Scene Scene
        {
            get { return this.scene; }
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
