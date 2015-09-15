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

using Synchronica.Simulation;
using System;

namespace Synchronica.Tests.Mock
{
    public sealed class Replayer : Replayer<RecorderData>
    {
        protected override void DeserializeRecord(RecorderData data)
        {
            foreach (var actorData in data.Actors)
            {
                var actor = GetActor(actorData.Id);
                if (actor == null)
                {
                    actor = CreateActor(actorData);
                }

                ReplayActor((Actor)actor, actorData, data.StartTime);
            }
        }

        private Actor CreateActor(ActorData data)
        {
            var actor = AddActor(data.Id, data.StartTime);

            if (data.Definitions != null)
            {
                foreach (var definition in data.Definitions)
                {
                    switch (definition.Type)
                    {
                        case VariableType.VBoolean:
                            AddBoolean(actor, definition.Id);
                            break;

                        case VariableType.VInt16:
                            AddInt16(actor, definition.Id);
                            break;

                        case VariableType.VInt32:
                            AddInt32(actor, definition.Id);
                            break;

                        case VariableType.VInt64:
                            AddInt64(actor, definition.Id);
                            break;

                        case VariableType.VFloat:
                            AddFloat(actor, definition.Id);
                            break;

                        default:
                            throw new NotSupportedException("VariableType");
                    }
                }
            }

            return actor;
        }

        private void ReplayActor(Actor actor, ActorData data, int startTime)
        {
            if (data.Variables != null)
            {
                foreach (var variableData in data.Variables)
                {
                    var variable = actor.GetVariable(variableData.Id);
                    if (variable == null)
                    {
                        throw new InvalidOperationException("Missing variable");
                    }

                    ReplayVariable(variable, variableData, startTime);
                }
            }

            if (data.EndTime > 0 && actor.EndTime < 0)
                RemoveActor(actor, data.EndTime);
        }

        private void ReplayVariable(Variable variable, VariableData data, int startTime)
        {
            RemoveFramesAfter(variable, startTime);

            foreach (var keyFrame in data.KeyFrames)
            {
                switch (keyFrame.Type)
                {
                    case KeyFrameType.Linear_Int16:
                        AddLinearFrame((Variable<short>)variable, keyFrame.Time, (short)keyFrame.Value);
                        break;

                    case KeyFrameType.Linear_Int32:
                        AddLinearFrame((Variable<int>)variable, keyFrame.Time, (short)keyFrame.Value);
                        break;

                    case KeyFrameType.Linear_Int64:
                        AddLinearFrame((Variable<long>)variable, keyFrame.Time, (long)keyFrame.Value);
                        break;

                    case KeyFrameType.Linear_Float:
                        AddLinearFrame((Variable<float>)variable, keyFrame.Time, (float)keyFrame.Value);
                        break;

                    case KeyFrameType.Pulse_Int16:
                        AddPulseFrame((Variable<short>)variable, keyFrame.Time, (short)keyFrame.Value);
                        break;

                    case KeyFrameType.Pulse_Int32:
                        AddPulseFrame((Variable<int>)variable, keyFrame.Time, (short)keyFrame.Value);
                        break;

                    case KeyFrameType.Pulse_Int64:
                        AddPulseFrame((Variable<long>)variable, keyFrame.Time, (long)keyFrame.Value);
                        break;

                    case KeyFrameType.Pulse_Float:
                        AddPulseFrame((Variable<float>)variable, keyFrame.Time, (float)keyFrame.Value);
                        break;

                    case KeyFrameType.Step_Int16:
                        AddStepFrame((Variable<short>)variable, keyFrame.Time, (short)keyFrame.Value);
                        break;

                    case KeyFrameType.Step_Int32:
                        AddStepFrame((Variable<int>)variable, keyFrame.Time, (int)keyFrame.Value);
                        break;

                    case KeyFrameType.Step_Int64:
                        AddStepFrame((Variable<long>)variable, keyFrame.Time, (long)keyFrame.Value);
                        break;

                    case KeyFrameType.Step_Float:
                        AddStepFrame((Variable<float>)variable, keyFrame.Time, (float)keyFrame.Value);
                        break;

                    default:
                        throw new NotSupportedException("KeyFrameType");
                }
            }
        }
    }
}