﻿/*
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

namespace Synchronica.Simulation.Variables
{
    sealed class VInt64 : Variable<long>, ILinearKeyFrameVariable<long>, IPulseKeyFrameVariable<long>, IStepKeyFrameVariable<long>
    {
        internal VInt64(GameObject gameObject, int id, long initialValue)
            : base(gameObject, id, initialValue)
        { }

        public void AddLinearFrame(int time, long value)
        {
            AddKeyFrame(new LinearKeyFrame_Int64(Tail, null, time, value));
        }

        public void AddPulseFrame(int time, long value)
        {
            AddKeyFrame(new PulseKeyFrame_Int64(Tail, null, time, value));
        }

        public void AddStepFrame(int time, long value)
        {
            AddKeyFrame(new StepKeyFrame<long>(Tail, null, time, value));
        }
    }
}