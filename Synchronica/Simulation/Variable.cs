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
using Synchronica.Simulation.Modifiers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Synchronica.Simulation
{
    public abstract class Variable
    {
        private int id;
        private KeyFrame head;
        private KeyFrame tail;
        private KeyFrame current;

        protected Variable(int id, KeyFrame initialFrame)
        {
            this.id = id;
            this.head = initialFrame;
            this.tail = initialFrame;
            this.current = initialFrame;
        }

        public TValue GetValue<TValue>(int milliseconds)
        {
            var frame = (KeyFrame<TValue>)FindFrame(milliseconds);
            this.current = frame;
            return frame.GetValue(milliseconds);
        }

        protected void AppendFrame(KeyFrame frame)
        {
            if (this.tail.Milliseconds >= frame.Milliseconds)
                throw new ArgumentException("milliseconds must be greater than last frame");

            this.tail = frame;
            this.current = frame;
        }

        public void RemoveFramesBefore(int milliseconds)
        {
            if (milliseconds <= this.head.Milliseconds)
                return;

            if (milliseconds > this.tail.Milliseconds)
                throw new ArgumentException("Cannot remove last frame");

            var newHead = FindFrame(milliseconds).Interpolate(milliseconds);
            newHead.Previous = null;

            this.head = newHead;
            this.current = newHead;
        }

        public void RemoveFramesAfter(int milliseconds)
        {
            if (milliseconds >= this.tail.Milliseconds)
                return;

            if (milliseconds < this.head.Milliseconds)
                throw new ArgumentException("Cannot remove first frame");

            var newTail = FindFrame(milliseconds).Interpolate(milliseconds);
            newTail.Next = null;

            this.tail = newTail;
            this.current = newTail;
        }

        public PropertyData GetData(int startMilliseconds, int endMilliseconds)
        {
            PropertyData propertyData = null;

            var keyFrames = FindFrames(startMilliseconds, endMilliseconds);
            foreach (var frame in keyFrames)
            {
                if (propertyData == null)
                    propertyData = new PropertyData(this.id);

                propertyData.AddFrame(frame.GetData());
            }

            return propertyData;
        }

        private KeyFrame FindFrame(int milliseconds)
        {
            if (milliseconds <= this.head.Milliseconds)
                return this.head;

            if (milliseconds >= this.tail.Milliseconds)
                return this.tail;

            if (milliseconds > this.current.Milliseconds)
                return FindNextFrame(this.current.Next, f => f.Milliseconds >= milliseconds);

            return FindPreviousFrame(this.current, f => f.Previous.Milliseconds < milliseconds);
        }

        private IEnumerable<KeyFrame> FindFrames(int startMilliseconds, int endMilliseconds)
        {
            for (var frame = this.head; frame.Next != null; frame = frame.Next)
            {
                if (frame.Milliseconds > endMilliseconds)
                {
                    yield return frame;
                    yield break;
                }
                else if (frame.Milliseconds > startMilliseconds)
                {
                    yield return frame;
                }
            }
        }

        private static KeyFrame FindNextFrame(KeyFrame frame, Func<KeyFrame, bool> predicate)
        {
            for (; frame != null; frame = frame.Next)
            {
                if (predicate(frame))
                    break;
            }

            return frame;
        }

        private static KeyFrame FindPreviousFrame(KeyFrame frame, Func<KeyFrame, bool> predicate)
        {
            for (; frame != null; frame = frame.Previous)
            {
                if (predicate(frame))
                    break;
            }

            return frame;
        }

        public int Id
        {
            get { return this.id; }
        }

        protected KeyFrame Head
        {
            get { return this.head; }
        }

        protected KeyFrame Tail
        {
            get { return this.tail; }
        }
    }

    public abstract class Variable<TValue> : Variable
    {
        protected Variable(int id, TValue initialValue)
            : base(id, CreateInitialFrame(initialValue))
        { }

        private static KeyFrame CreateInitialFrame(TValue initialValue)
        {
            return new KeyFrame<TValue>(null, null, 0, initialValue, new StepModifier<TValue>());
        }

        public TValue GetValue(int milliseconds)
        {
            return base.GetValue<TValue>(milliseconds);
        }

        protected new KeyFrame<TValue> Head
        {
            get { return (KeyFrame<TValue>)base.Head; }
        }

        protected new KeyFrame<TValue> Tail
        {
            get { return (KeyFrame<TValue>)base.Tail; }
        }
    }
}
