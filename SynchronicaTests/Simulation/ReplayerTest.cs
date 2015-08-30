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

using NUnit.Framework;
using Synchronica.Tests.Mock;

namespace Synchronica.Tests.Simulation
{
    public class ReplayerTest
    {
        [Test]
        public void TestReplayer()
        {
            var recorder = new Recorder();
            var obj1 = recorder.AddObject(2);
            var var1 = recorder.AddInt16(obj1, 10);
            var var2 = recorder.AddInt32(obj1, -10);
            var var3 = recorder.AddFloat(obj1, 5.7f);

            recorder.AddLinearFrame(var1, 100, (short)30);
            recorder.AddStepFrame(var2, 110, 10);
            recorder.AddLinearFrame(var3, 90, 9.3f);

            var data = recorder.Record(100);

            var replayer = new Replayer();
            replayer.Replay(data.StartTime, data.EndTime, data);
        }

    }
}
