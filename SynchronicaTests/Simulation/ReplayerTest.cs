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
            var replayer = new Replayer();

            #region Time: 0-100ms

            var obj1 = recorder.AddActor(0, f =>
            {
                f.AddInt16(1, 10);
                f.AddInt32(2, -10);
                f.AddFloat(3, 5.7f);
            });

            var var1 = obj1.GetVariable<short>(1);
            var var2 = obj1.GetVariable<int>(2);
            var var3 = obj1.GetVariable<float>(3);

            recorder.AddLinearFrame(var1, 100, (short)30);
            recorder.AddStepFrame(var2, 110, 10);
            recorder.AddLinearFrame(var3, 90, 9.3f);

            var data = recorder.Record(100);
            Assert.AreEqual(0, data.StartTime);
            Assert.AreEqual(100, data.EndTime);

            replayer.Replay(data.StartTime, data.EndTime, data);
            
            var mObj1 = replayer.GetActor(1);
            Assert.AreEqual(obj1.StartTime, mObj1.StartTime);
            Assert.AreEqual(obj1.EndTime, mObj1.EndTime);

            var mVar1 = mObj1.GetVariable<short>(1);
            Assert.AreEqual(10, mVar1.GetValue(0));
            Assert.AreEqual(20, mVar1.GetValue(50));
            Assert.AreEqual(30, mVar1.GetValue(100));

            var mVar2 = mObj1.GetVariable<int>(2);
            Assert.AreEqual(-10, mVar2.GetValue(50));

            var mVar3 = mObj1.GetVariable<float>(3);
            Assert.AreEqual(7.7f, mVar3.GetValue(50));
            Assert.AreEqual(9.3f, mVar3.GetValue(95));

            #endregion

            #region Time: 100-200ms

            recorder.InterpolateKeyFrame(var1, 110);
            recorder.RemoveKeyFramesAfter(var1, 111);
            recorder.AddLinearFrame(var1, 150, (short)40);

            #endregion
        }
    }
}
