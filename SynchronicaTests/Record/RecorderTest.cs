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
using Synchronica.Simulation.Variables;
using Synchronica.Tests.Mock;
using System;

namespace Synchronica.Tests.Record
{
    public class RecorderTest
    {
        [Test]
        public void TestRecorder()
        {
            var recorder = new Recorder();
            var obj1 = recorder.AddObject(2);
            var var1 = recorder.AddInt16(obj1, 10);
            var var2 = recorder.AddInt32(obj1, -10);
            var var3 = recorder.AddFloat(obj1, 5.7f);

            var obj2 = recorder.GetObject(1);
            Assert.AreEqual(obj1.Id, obj2.Id);
            Assert.AreEqual(obj1.StartTime, obj2.StartTime);
            Assert.AreEqual(obj1.EndTime, obj2.EndTime);

            var var4 = obj2.GetVariable<short>(var1.Id);
            var var5 = obj2.GetVariable<int>(var2.Id);
            var var6 = obj2.GetVariable<float>(var3.Id);
            //Assert.Throws<ArgumentException>(() => var4.GetValue(1));
            Assert.AreEqual(10, var4.GetValue(2));

            recorder.AddLinearFrame(var4, 100, (short)30);
            recorder.AddStepFrame(var5, 110, 10);
            recorder.AddLinearFrame(var6, 90, 9.3f);

            var data = recorder.Record(100);
            Assert.AreEqual(0, data.StartTime);
            Assert.AreEqual(100, data.EndTime);
        }
    }
}
