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

using NUnit.Framework;
using Synchronica.Simulation;
using Synchronica.Simulation.Variables;
using System;

namespace Synchronica.Tests.Simulation
{
    public class VInt16Test
    {
        [Test]
        public void TestAddFrames()
        {
            var actor = new Actor(new Scene(), 1, 0);

            var value = new VInt16(actor, 1);
            value.AddStepFrame(actor.StartTime, -10);
            Assert.Throws<ArgumentException>(() => value.GetValue(-1));
            Assert.AreEqual(-10, value.GetValue(0));
            Assert.AreEqual(-10, value.GetValue(1));

            value.AddStepFrame(10, 5);
            Assert.AreEqual(-10, value.GetValue(0));
            Assert.AreEqual(-10, value.GetValue(9));
            Assert.AreEqual(5, value.GetValue(10));
            Assert.AreEqual(5, value.GetValue(11));

            value.AddLinearFrame(20, 15);
            Assert.AreEqual(5, value.GetValue(10));
            Assert.AreEqual(6, value.GetValue(11));
            Assert.AreEqual(14, value.GetValue(19));
            Assert.AreEqual(15, value.GetValue(20));
            Assert.AreEqual(15, value.GetValue(21));
        }

        [Test]
        public void TestRemoveFramesBefore()
        {
            var actor = new Actor(new Scene(), 1, 0);

            var value = new VInt16(actor, 1);
            value.AddStepFrame(actor.StartTime, 0);

            Assert.Throws<ArgumentException>(() => value.RemoveFramesBefore(1));

            value.AddStepFrame(10, 5);
            value.Interpolate(5);
            value.RemoveFramesBefore(4);
            Assert.AreEqual(0, value.GetValue(4));
            Assert.AreEqual(0, value.GetValue(5));
            Assert.AreEqual(0, value.GetValue(9));
            Assert.AreEqual(5, value.GetValue(10));
            Assert.AreEqual(5, value.GetValue(11));

            value.AddLinearFrame(20, 15);
            value.Interpolate(12);
            value.RemoveFramesBefore(11);
            Assert.AreEqual(7, value.GetValue(11));
            Assert.AreEqual(7, value.GetValue(12));
            Assert.AreEqual(8, value.GetValue(13));
            Assert.AreEqual(14, value.GetValue(19));
            Assert.AreEqual(15, value.GetValue(20));
        }

        [Test]
        public void TestRemoveFramesAfter()
        {
            var actor = new Actor(new Scene(), 1, 0);

            var value = new VInt16(actor, 1);
            value.AddStepFrame(actor.StartTime, 0);

            Assert.Throws<ArgumentException>(() => value.RemoveFramesAfter(-1));

            value.AddStepFrame(10, 5);
            value.Interpolate(9);
            value.RemoveFramesAfter(10);
            Assert.AreEqual(0, value.GetValue(8), 0);
            Assert.AreEqual(0, value.GetValue(9), 0);
            Assert.AreEqual(0, value.GetValue(10), 0);

            value.AddLinearFrame(19, 10);
            value.Interpolate(17);
            value.RemoveFramesAfter(18);
            Assert.AreEqual(7, value.GetValue(16));
            Assert.AreEqual(8, value.GetValue(17));
            Assert.AreEqual(8, value.GetValue(18));
        }
    }
}
