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
    public sealed class LinearKeyFrame_Int16 : KeyFrame<short>
    {
        public LinearKeyFrame_Int16(KeyFrame<short> previous, KeyFrame<short> next, int milliseconds, short value)
            : base(previous, next, milliseconds, value)
        { }

        internal override short GetValue(int milliseconds)
        {
            var slope = (float)(Value - Previous.Value) / (float)(Milliseconds - Previous.Milliseconds);
            var intercept = Previous.Value;
            var value = slope * (milliseconds - Previous.Milliseconds) + intercept;

            return (short)Math.Round(value);
        }

        internal override KeyFrame Interpolate(int milliseconds)
        {
            return new LinearKeyFrame_Int16(Previous, this, milliseconds, GetValue(milliseconds));
        }
    }

    public sealed class LinearKeyFrame_Int32 : KeyFrame<int>
    {
        public LinearKeyFrame_Int32(KeyFrame<int> previous, KeyFrame<int> next, int milliseconds, int value)
            : base(previous, next, milliseconds, value)
        { }

        internal override int GetValue(int milliseconds)
        {
            var slope = (float)(Value - Previous.Value) / (float)(Milliseconds - Previous.Milliseconds);
            var intercept = Previous.Value;
            var value = slope * (milliseconds - Previous.Milliseconds) + intercept;

            return (int)Math.Round(value);
        }

        internal override KeyFrame Interpolate(int milliseconds)
        {
            return new LinearKeyFrame_Int32(Previous, this, milliseconds, GetValue(milliseconds));
        }
    }

    public sealed class LinearKeyFrame_Int64 : KeyFrame<long>
    {
        public LinearKeyFrame_Int64(KeyFrame<long> previous, KeyFrame<long> next, int milliseconds, long value)
            : base(previous, next, milliseconds, value)
        { }

        internal override long GetValue(int milliseconds)
        {
            var slope = (float)(Value - Previous.Value) / (float)(Milliseconds - Previous.Milliseconds);
            var intercept = Previous.Value;
            var value = slope * (milliseconds - Previous.Milliseconds) + intercept;

            return (long)Math.Round(value);
        }

        internal override KeyFrame Interpolate(int milliseconds)
        {
            return new LinearKeyFrame_Int64(Previous, this, milliseconds, GetValue(milliseconds));
        }
    }

    public sealed class LinearKeyFrame_Float : KeyFrame<float>
    {
        public LinearKeyFrame_Float(KeyFrame<float> previous, KeyFrame<float> next, int milliseconds, float value)
            : base(previous, next, milliseconds, value)
        { }

        internal override float GetValue(int milliseconds)
        {
            var slope = (float)(Value - Previous.Value) / (float)(Milliseconds - Previous.Milliseconds);
            var intercept = Previous.Value;
            return slope * (milliseconds - Previous.Milliseconds) + intercept;
        }

        internal override KeyFrame Interpolate(int milliseconds)
        {
            return new LinearKeyFrame_Float(Previous, this, milliseconds, GetValue(milliseconds));
        }
    }
}
