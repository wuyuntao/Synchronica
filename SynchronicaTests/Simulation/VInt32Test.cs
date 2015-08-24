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
using System;

namespace Synchronica.Tests.Simulation
{
    public class VInt32Test
    {
        [Test]
        public void TestAppendFrames()
        {
            var value = new VInt32(-10);
            Assert.AreEqual(-10, value.GetValue(-1));
            Assert.AreEqual(-10, value.GetValue(0));
            Assert.AreEqual(-10, value.GetValue(1));

            value.AppendStepFrame(10, 5);
            Assert.AreEqual(-10, value.GetValue(-1));
            Assert.AreEqual(-10, value.GetValue(0));
            Assert.AreEqual(-10, value.GetValue(9));
            Assert.AreEqual(5, value.GetValue(10));
            Assert.AreEqual(5, value.GetValue(11));

            value.AppendLinearFrame(20, 15);
            Assert.AreEqual(5, value.GetValue(10));
            Assert.AreEqual(6, value.GetValue(11));
            Assert.AreEqual(14, value.GetValue(19));
            Assert.AreEqual(15, value.GetValue(20));
            Assert.AreEqual(15, value.GetValue(21));
        }

        [Test]
        public void TestRemoveFramesBefore()
        {
            var value = new VInt32();
            Assert.Throws<ArgumentException>(() => value.RemoveFramesBefore(1));

            value.AppendStepFrame(10, 5);
            value.RemoveFramesBefore(5);
            Assert.AreEqual(0, value.GetValue(4));
            Assert.AreEqual(0, value.GetValue(5));
            Assert.AreEqual(0, value.GetValue(9));
            Assert.AreEqual(5, value.GetValue(10));
            Assert.AreEqual(5, value.GetValue(11));

            value.AppendLinearFrame(20, 15);
            value.RemoveFramesBefore(12);
            Assert.AreEqual(7, value.GetValue(11));
            Assert.AreEqual(7, value.GetValue(12));
            Assert.AreEqual(8, value.GetValue(13));
            Assert.AreEqual(14, value.GetValue(19));
            Assert.AreEqual(15, value.GetValue(20));
        }

        [Test]
        public void TestRemoveFramesAfter()
        {
            var value = new VInt32();
            Assert.Throws<ArgumentException>(() => value.RemoveFramesAfter(-1));

            value.AppendStepFrame(10, 5);
            value.RemoveFramesAfter(9);
            Assert.AreEqual(0, value.GetValue(8), 0);
            Assert.AreEqual(0, value.GetValue(9), 0);
            Assert.AreEqual(0, value.GetValue(10), 0);

            value.AppendLinearFrame(19, 10);
            value.RemoveFramesAfter(17);
            Assert.AreEqual(7, value.GetValue(16));
            Assert.AreEqual(8, value.GetValue(17));
            Assert.AreEqual(8, value.GetValue(18));
        }
    }
}
