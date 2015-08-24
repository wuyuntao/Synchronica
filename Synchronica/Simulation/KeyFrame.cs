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

namespace Synchronica.Simulation
{
    public sealed class KeyFrame<TValue>
    {
        private KeyFrame<TValue> previous;
        private KeyFrame<TValue> next;
        private IModifier<TValue> modifier;

        private int milliseconds;
        private TValue value;

        internal KeyFrame(KeyFrame<TValue> previous, int milliseconds, TValue value, IModifier<TValue> modifier)
        {
            if (previous != null && previous.milliseconds >= milliseconds)
                throw new ArgumentException("time must be greater than time of previous frame");

            this.previous = previous;
            this.milliseconds = milliseconds;
            this.value = value;
            this.modifier = modifier;

            if (previous != null)
                this.previous.next = this;
        }

        internal TValue GetValue(int milliseconds)
        {
            if (this.previous == null)
                throw new InvalidOperationException("Previous frame is null");

            return this.modifier.GetValue(this.previous, this, milliseconds);
        }

        public KeyFrame<TValue> Previous
        {
            get { return this.previous; }
            internal set { this.previous = value; }
        }

        public KeyFrame<TValue> Next
        {
            get { return this.next; }
            internal set { this.next = value; }
        }

        public int Milliseconds
        {
            get { return this.milliseconds; }
        }

        public TValue Value
        {
            get { return this.value; }
        }
    }
}
