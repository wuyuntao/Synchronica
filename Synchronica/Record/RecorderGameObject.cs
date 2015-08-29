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

        internal RecorderGameObject(Scene scene, int id, int startTime)
            : base(scene, id, startTime)
        {
        }

        public VBoolean CreateBoolean(bool value)
        {
            return CreateBoolean(++this.lastVariableId, value);
        }

        public VInt16 CreateInt16(short value)
        {
            return CreateInt16(++this.lastVariableId, value);
        }

        public VInt32 CreateInt32(int value)
        {
            return CreateInt32(++this.lastVariableId, value);
        }

        public VInt64 CreateInt64(long value)
        {
            return CreateInt64(++this.lastVariableId, value);
        }

        public VFloat CreateFloat(float value)
        {
            return CreateFloat(++this.lastVariableId, value);
        }
    }
}
