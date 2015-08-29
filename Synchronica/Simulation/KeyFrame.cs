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
    abstract class KeyFrame
    {
        private KeyFrame previous;
        private KeyFrame next;
        private int milliseconds;

        protected KeyFrame(KeyFrame previous, KeyFrame next, int milliseconds)
        {
            if (previous != null && previous.milliseconds >= milliseconds)
                throw new ArgumentException("time must be greater than time of previous frame");

            Previous = previous;
            Next = next;
            this.milliseconds = milliseconds;
        }

        internal abstract KeyFrame Interpolate(int milliseconds);

        public KeyFrame Previous
        {
            get { return this.previous; }
            internal set
            {
                this.previous = value;

                if (value != null)
                    value.next = this;
            }
        }

        public KeyFrame Next
        {
            get { return this.next; }
            internal set
            {
                this.next = value;

                if (value != null)
                    value.previous = this;
            }

        }

        public int Milliseconds
        {
            get { return this.milliseconds; }
        }
    }

    abstract class KeyFrame<TValue> : KeyFrame
    {
        private TValue value;

        protected KeyFrame(KeyFrame<TValue> previous, KeyFrame<TValue> next, int milliseconds, TValue value)
            : base(previous, next, milliseconds)
        {
            this.value = value;
        }

        internal abstract TValue GetValue(int milliseconds);

        public new KeyFrame<TValue> Previous
        {
            get { return (KeyFrame<TValue>)base.Previous; }
            internal set { base.Previous = value; }
        }

        public new KeyFrame<TValue> Next
        {
            get { return (KeyFrame<TValue>)base.Next; }
            internal set { base.Next = value; }
        }

        public TValue Value
        {
            get { return this.value; }
        }
    }
}
