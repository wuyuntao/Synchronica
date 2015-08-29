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
using Synchronica.Simulation.Variables;

namespace Synchronica.Record
{
    class RecorderGameObject : GameObject
    {
        private int lastVariableId;

        internal RecorderGameObject(int id, int startTime)
            : base(id, startTime)
        {
        }

        public VBoolean CreateBoolean(bool value)
        {
            var variable = new VBoolean(++this.lastVariableId, value);
            AddVariable(variable);
            return variable;
        }

        public VInt16 CreateInt16(short value)
        {
            var variable = new VInt16(++this.lastVariableId, value);
            AddVariable(variable);
            return variable;
        }

        public VInt32 CreateInt32(int value)
        {
            var variable = new VInt32(++this.lastVariableId, value);
            AddVariable(variable);
            return variable;
        }

        public VInt64 CreateInt64(long value)
        {
            var variable = new VInt64(++this.lastVariableId, value);
            AddVariable(variable);
            return variable;
        }

        public VFloat CreateFloat(float value)
        {
            var variable = new VFloat(++this.lastVariableId, value);
            AddVariable(variable);
            return variable;
        }
    }
}
