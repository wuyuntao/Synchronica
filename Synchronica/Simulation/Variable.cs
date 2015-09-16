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
using System.Linq;

namespace Synchronica.Simulation
{
    public abstract class Variable
    {
        private Actor actor;
        private int id;
        private Type valueType;

        private KeyFrame firstFrame;
        private KeyFrame lastFrame;

        protected Variable(Actor actor, int id, Type valueType)
        {
            this.actor = actor;
            this.id = id;
            this.valueType = valueType;
        }

        protected KeyFrame FindFrameBefore(int time)
        {
            return FindFramesBefore(time).LastOrDefault();
        }

        protected KeyFrame FindFrameAfter(int time)
        {
            return FindFramesAfter(time).FirstOrDefault();
        }

        public IEnumerable<KeyFrame> FindFramesBefore(int time)
        {
            return from frame in KeyFrames
                   where frame.Time <= time
                   select frame;
        }

        public IEnumerable<KeyFrame> FindFramesAfter(int time)
        {
            return from frame in KeyFrames
                   where frame.Time >= time
                   select frame;
        }

        protected void AddFirstFrame(KeyFrame frame)
        {
            if (this.firstFrame == null)
            {
                this.firstFrame = frame;
                this.lastFrame = frame;
            }
            else
            {
                if (frame.Time >= this.firstFrame.Time)
                    throw new ArgumentException("time must be greater than last frame");

                AddFrameAfter(this.firstFrame, frame);
            }
        }

        protected void AddLastFrame(KeyFrame frame)
        {
            if (this.lastFrame == null)
            {
                this.firstFrame = frame;
                this.lastFrame = frame;
            }
            else
            {
                if (frame.Time <= this.lastFrame.Time)
                    throw new ArgumentException("time must be greater than last frame");

                AddFrameAfter(this.lastFrame, frame);
            }
        }

        protected void AddFrameBefore(KeyFrame frame, KeyFrame newFrame)
        {
            if (frame.Previous != null)
            {
                frame.Previous.Next = newFrame;
                newFrame.Previous = frame.Previous;
            }
            else
            {
                this.firstFrame = newFrame;
            }

            frame.Previous = newFrame;
            newFrame.Next = frame;
        }

        protected void AddFrameAfter(KeyFrame frame, KeyFrame newFrame)
        {
            if (frame.Next != null)
            {
                frame.Next.Previous = newFrame;
                newFrame.Next = frame.Next;
            }
            else
            {
                this.lastFrame = newFrame;
            }

            frame.Next = newFrame;
            newFrame.Previous = frame;
        }

        internal void RemoveFramesBefore(int time)
        {
            if (LastFrame == null)
                throw new ArgumentException("No key frame is added");

            if (time >= LastFrame.Time)
                throw new ArgumentException("Cannot remove first frame");

            var frame = FindFrameBefore(time);
            if (frame != null)
            {
                frame.Next.Previous = null;

                this.firstFrame = frame.Next;
            }
        }

        internal void RemoveFramesAfter(int time)
        {
            if (FirstFrame == null)
                throw new ArgumentException("No key frame is added");

            if (time <= FirstFrame.Time)
                throw new ArgumentException("Cannot remove first frame");

            var frame = FindFrameAfter(time);
            if (frame != null)
            {
                frame.Previous.Next = null;

                this.lastFrame = frame.Previous;
            }
        }

        public Actor Actor
        {
            get { return this.actor; }
        }

        public int Id
        {
            get { return this.id; }
        }

        public Type ValueType
        {
            get { return this.valueType; }
        }

        internal bool IsNew
        {
            get { return this.firstFrame == null; }
        }

        protected KeyFrame FirstFrame
        {
            get { return this.firstFrame; }
        }

        protected KeyFrame LastFrame
        {
            get { return this.lastFrame; }
        }

        protected IEnumerable<KeyFrame> KeyFrames
        {
            get
            {
                for (var frame = this.firstFrame; frame != null; frame = frame.Next)
                    yield return frame;
            }
        }
    }

    public class Variable<TValue> : Variable
    {
        internal Variable(Actor actor, int id)
            : base(actor, id, typeof(TValue))
        { }

        protected Variable(Actor actor, int id, TValue initialValue)
            : base(actor, id, typeof(TValue))
        {
            AddFirstFrame(new StepKeyFrame<TValue>(actor.StartTime, initialValue));
        }

        internal void Interpolate(int time)
        {
            if (FirstFrame == null)
                throw new ArgumentException("No key frame is added");

            if (time < FirstFrame.Time)
            {
                AddFirstFrame(new StepKeyFrame<TValue>(time, ((KeyFrame<TValue>)FirstFrame).Value));
            }
            else if (time > LastFrame.Time)
            {
                AddLastFrame(new StepKeyFrame<TValue>(time, ((KeyFrame<TValue>)LastFrame).Value));
            }
            else
            {
                foreach (var frame in KeyFrames)
                {
                    if (time < frame.Time)
                    {
                        AddFrameBefore(frame, frame.Clone(time));
                        break;
                    }
                    else if (time == frame.Time)
                    {
                        break;
                    }
                }
            }
        }

        public TValue GetValue(int time)
        {
            if (time < Actor.StartTime)
                throw new ArgumentException("Cannot get value before actor starts");

            if (FirstFrame == null)
                throw new ArgumentException("No key frame is added");

            object value;
            if (time <= FirstFrame.Time)
            {
                value = FirstFrame.Value;
            }
            else
            {
                var frame = FindFrameAfter(time);
                if (frame == null)
                {
                    value = LastFrame.Value;

                }
                else if (frame.Previous == null || time >= frame.Time)
                {
                    value = frame.Value;
                }
                else
                {
                    value = ((KeyFrame<TValue>)frame).GetValue(time);
                }
            }

            return (TValue)value;
        }
    }
}