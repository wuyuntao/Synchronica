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

using Synchronica.Record;
using Synchronica.Simulation;
using Synchronica.Simulation.KeyFrames;
using System;
using System.Linq;

namespace Synchronica.Tests.Mock
{
    public sealed class Recorder : Recorder<RecorderData>
    {
        protected override RecorderData Record(int startTime, int endTime)
        {
            var data = new RecorderData()
            {
                StartTime = startTime,
                EndTime = endTime,
                GameObjects = (from gameObject in Objects
                               select RecordGameObject(gameObject, startTime) into gameObjectData
                               where gameObjectData != null
                               select gameObjectData).ToArray(),
            };

            return data.GameObjects.Length > 0 ? data : null;
        }

        private GameObjectData RecordGameObject(GameObject gameObject, int startTime)
        {
            var data = new GameObjectData()
            {
                Id = gameObject.Id,
                StartTime = gameObject.StartTime,
                EndTime = gameObject.EndTime,
                Variables = (from variable in gameObject.Variables
                             select RecordVariable(variable, startTime) into variableData
                             where variableData != null
                             select variableData).ToArray(),
            };

            if (gameObject.StartTime >= startTime)
            {
                data.Definitions = (from variable in gameObject.Variables
                                    select DefineVariable(variable)).ToArray();
            }

            return (data.StartTime >= startTime || data.EndTime >= startTime || data.Variables.Length > 0) ? data : null;
        }

        private VariableDefinition DefineVariable(Variable variable)
        {
            var definition = new VariableDefinition()
            {
                Id = variable.Id,
                InitialValue = variable.Head.Value,
            };

            if (definition.InitialValue is short)
                definition.Type = VariableType.VInt16;
            else if (definition.InitialValue is int)
                definition.Type = VariableType.VInt32;
            else if (definition.InitialValue is long)
                definition.Type = VariableType.VInt64;
            else if (definition.InitialValue is float)
                definition.Type = VariableType.VFloat;

            return definition;
        }

        private VariableData RecordVariable(Variable variable, int startTime)
        {
            var data = new VariableData()
            {
                Id = variable.Id,
                KeyFrames = (from keyFrame in variable.GetKeyFramesAfter(startTime)
                             select RecordKeyFrame(keyFrame)).ToArray(),
            };

            return data.KeyFrames.Length > 0 ? data : null;
        }

        private KeyFrameData RecordKeyFrame(KeyFrame keyFrame)
        {
            var data = new KeyFrameData()
            {
                Time = keyFrame.Milliseconds,
                Value = keyFrame.Value,
            };

            if (keyFrame is LinearKeyFrame_Int16)
                data.Type = KeyFrameType.Linear_Int16;
            else if (keyFrame is LinearKeyFrame_Int32)
                data.Type = KeyFrameType.Linear_Int32;
            else if (keyFrame is LinearKeyFrame_Int64)
                data.Type = KeyFrameType.Linear_Int64;
            else if (keyFrame is LinearKeyFrame_Float)
                data.Type = KeyFrameType.Linear_Float;

            else if (keyFrame is PulseKeyFrame_Int16)
                data.Type = KeyFrameType.Pulse_Int16;
            else if (keyFrame is PulseKeyFrame_Int32)
                data.Type = KeyFrameType.Pulse_Int32;
            else if (keyFrame is PulseKeyFrame_Int64)
                data.Type = KeyFrameType.Pulse_Int64;
            else if (keyFrame is PulseKeyFrame_Float)
                data.Type = KeyFrameType.Pulse_Float;

            else if (keyFrame is StepKeyFrame<short>)
                data.Type = KeyFrameType.Step_Int16;
            else if (keyFrame is StepKeyFrame<int>)
                data.Type = KeyFrameType.Step_Int32;
            else if (keyFrame is StepKeyFrame<long>)
                data.Type = KeyFrameType.Step_Int64;
            else if (keyFrame is StepKeyFrame<float>)
                data.Type = KeyFrameType.Step_Float;

            else
                throw new NotSupportedException("KeyFrameType");

            return data;
        }
    }
}
