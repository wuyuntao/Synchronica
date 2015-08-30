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

namespace Synchronica.Simulation.KeyFrames
{
    interface ILinearKeyFrameVariable<TValue>
    {
        void AddLinearFrame(int time, TValue value);
    }

    sealed class LinearKeyFrame_Int16 : KeyFrame<short>
    {
        internal LinearKeyFrame_Int16(int time, short value)
            : base(time, value)
        { }

        internal override short GetValue(int time)
        {
            var slope = (float)(Value - Previous.Value) / (float)(Time - Previous.Time);
            var intercept = Previous.Value;
            var value = slope * (time - Previous.Time) + intercept;

            return (short)Math.Round(value);
        }

        internal override KeyFrame Clone(int time)
        {
            return new LinearKeyFrame_Int16(time, GetValue(time));
        }
    }

    sealed class LinearKeyFrame_Int32 : KeyFrame<int>
    {
        internal LinearKeyFrame_Int32(int time, int value)
            : base(time, value)
        { }

        internal override int GetValue(int time)
        {
            var slope = (float)(Value - Previous.Value) / (float)(Time - Previous.Time);
            var intercept = Previous.Value;
            var value = slope * (time - Previous.Time) + intercept;

            return (int)Math.Round(value);
        }

        internal override KeyFrame Clone(int time)
        {
            return new LinearKeyFrame_Int32(time, GetValue(time));
        }
    }

    sealed class LinearKeyFrame_Int64 : KeyFrame<long>
    {
        internal LinearKeyFrame_Int64(int time, long value)
            : base(time, value)
        { }

        internal override long GetValue(int time)
        {
            var slope = (float)(Value - Previous.Value) / (float)(Time - Previous.Time);
            var intercept = Previous.Value;
            var value = slope * (time - Previous.Time) + intercept;

            return (long)Math.Round(value);
        }

        internal override KeyFrame Clone(int time)
        {
            return new LinearKeyFrame_Int64(time, GetValue(time));
        }
    }

    sealed class LinearKeyFrame_Float : KeyFrame<float>
    {
        internal LinearKeyFrame_Float(int time, float value)
            : base(time, value)
        { }

        internal override float GetValue(int time)
        {
            var slope = (float)(Value - Previous.Value) / (float)(Time - Previous.Time);
            var intercept = Previous.Value;
            return slope * (time - Previous.Time) + intercept;
        }

        internal override KeyFrame Clone(int time)
        {
            return new LinearKeyFrame_Float(time, GetValue(time));
        }
    }
}
