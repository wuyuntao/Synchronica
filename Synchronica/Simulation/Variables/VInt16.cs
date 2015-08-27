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

using Synchronica.Simulation.Modifiers;

namespace Synchronica.Simulation.Variables
{
    public sealed class VInt16 : Variable<short>
    {
        internal VInt16(int id, short initialValue)
            : base(id, initialValue)
        { }

        internal VInt16(int id)
            : this(id, 0)
        { }

        public void AppendStepFrame(int milliseconds, short value)
        {
            AppendFrame(new KeyFrame<short>(Tail, null, milliseconds, value, new StepModifier<short>()));
        }

        public void AppendLinearFrame(int milliseconds, short value)
        {
            AppendFrame(new KeyFrame<short>(Tail, null, milliseconds, value, new LinearModifier_Int16()));
        }
    }
}