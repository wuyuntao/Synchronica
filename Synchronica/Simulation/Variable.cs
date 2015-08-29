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

using Synchronica.Simulation.KeyFrames;
using System;
using System.Collections.Generic;

namespace Synchronica.Simulation
{
    public abstract class Variable
    {
        private GameObject gameObject;
        private int id;
        private KeyFrame head;
        private KeyFrame tail;

        internal Variable(GameObject gameObject, int id, KeyFrame initialFrame)
        {
            this.gameObject = gameObject;
            this.id = id;
            this.head = initialFrame;
            this.tail = initialFrame;
        }

        public TValue GetValue<TValue>(int milliseconds)
        {
            if (milliseconds < this.gameObject.StartTime)
                throw new ArgumentException("Cannot get value before game object starts");

            if (milliseconds <= this.head.Milliseconds)
                return ((KeyFrame<TValue>)this.head).Value;

            if (milliseconds >= this.tail.Milliseconds)
                return ((KeyFrame<TValue>)this.tail).Value;

            var frame = (KeyFrame<TValue>)FindFrame(milliseconds);
            return frame.GetValue(milliseconds);
        }

        protected void AddKeyFrame(KeyFrame frame)
        {
            if (this.tail.Milliseconds >= frame.Milliseconds)
                throw new ArgumentException("milliseconds must be greater than last frame");

            this.tail = frame;
        }

        public IEnumerable<KeyFrame> GetKeyFramesAfter(int milliseconds)
        {
            for (var frame = this.head; frame.Next != null; frame = frame.Next)
            {
                if (frame.Milliseconds >= milliseconds)
                    yield return frame;
            }
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
        }

        private KeyFrame FindFrame(int milliseconds)
        {
            if (milliseconds <= this.head.Milliseconds)
                return this.head;

            if (milliseconds >= this.tail.Milliseconds)
                return this.tail;

            return FindNextFrame(this.head, f => f.Milliseconds >= milliseconds);
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

        internal KeyFrame Head
        {
            get { return this.head; }
        }

        internal KeyFrame Tail
        {
            get { return this.tail; }
        }
    }

    public abstract class Variable<TValue> : Variable
    {
        protected Variable(GameObject gameObject, int id, TValue initialValue)
            : base(gameObject, id, new StepKeyFrame<TValue>(null, null, 0, initialValue))
        { }

        public TValue GetValue(int milliseconds)
        {
            return base.GetValue<TValue>(milliseconds);
        }

        internal new KeyFrame<TValue> Head
        {
            get { return (KeyFrame<TValue>)base.Head; }
        }

        internal new KeyFrame<TValue> Tail
        {
            get { return (KeyFrame<TValue>)base.Tail; }
        }
    }
}
