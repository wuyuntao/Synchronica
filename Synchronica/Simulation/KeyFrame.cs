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
    public abstract class KeyFrame
    {
        private int time;
        private object value;

        protected KeyFrame(int time, object value)
        {
            this.time = time;
            this.value = value;
        }

        internal abstract KeyFrame Clone(int time);

        internal KeyFrame Previous { get; set; }

        internal KeyFrame Next { get; set; }

        public int Time
        {
            get { return this.time; }
        }

        public object Value
        {
            get { return this.value; }
        }
    }

    public abstract class KeyFrame<TValue> : KeyFrame
    {
        protected KeyFrame(int time, TValue value)
            : base(time, value)
        {
        }

        internal abstract TValue GetValue(int time);

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

        public new TValue Value
        {
            get { return (TValue)base.Value; }
        }
    }
}
