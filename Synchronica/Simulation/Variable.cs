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
using System;
using System.Collections.Generic;
using System.Linq;

namespace Synchronica.Simulation
{
    public abstract class Variable<TValue>
    {
        private KeyFrame<TValue> head;
        private KeyFrame<TValue> tail;
        private KeyFrame<TValue> current;

        protected Variable(TValue initialValue)
        {
            var initialFrame = new KeyFrame<TValue>(null, 0, initialValue, new StepModifier<TValue>());

            this.head = initialFrame;
            this.tail = initialFrame;
            this.current = initialFrame;
        }

        public TValue GetValue(int milliseconds)
        {
            this.current = FindFrame(milliseconds);

            return this.current.GetValue(milliseconds);
        }

        public void AppendFrame(int milliseconds, TValue value, IModifier<TValue> modifier)
        {
            if (this.tail.Milliseconds >= milliseconds)
                throw new ArgumentException("milliseconds must be greater than last frame");

            var frame = new KeyFrame<TValue>(this.tail, milliseconds, value, modifier);

            this.tail = frame;
            this.current = frame;
        }

        public void RemoveFramesBefore(int milliseconds)
        {
            if (milliseconds <= this.head.Milliseconds)
                return;

            if (milliseconds >= this.tail.Milliseconds)
                throw new ArgumentNullException("Cannot remove last frame");

            var frame = FindFrame(milliseconds);
            if (frame == this.tail )
                throw new ArgumentNullException("Cannot remove last frame");

            this.head = frame;
            this.head.Previous = null;

            this.current = this.head;
        }

        public void RemoveFramesAfter(int milliseconds)
        {
            if (milliseconds >= this.tail.Milliseconds)
                return;

            if (milliseconds <= this.head.Milliseconds)
                throw new ArgumentNullException("Cannot remove first frame");

            var frame = FindFrame(milliseconds);
            if (frame == this.head )
                throw new ArgumentNullException("Cannot remove first frame");

            this.tail = frame.Previous;
            this.tail.Next = null;

            this.current = this.tail;
        }

        private KeyFrame<TValue> FindFrame(int milliseconds)
        {
            if (milliseconds <= this.head.Milliseconds)
            {
                return this.head;
            }
            else if (milliseconds >= this.tail.Milliseconds)
            {
                return this.tail;
            }
            else if (milliseconds > this.current.Milliseconds)
            {
                var frame = this.current.Next;
                while (frame.Next != null && frame.Milliseconds < milliseconds)
                    frame = frame.Next;

                return frame;
            }
            else
            {
                var frame = this.current;
                while (frame.Previous != null && frame.Previous.Milliseconds >= milliseconds)
                    frame = frame.Previous;

                return frame;
            }
        }
    }
}
