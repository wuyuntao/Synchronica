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

using Synchronica.Replay;
using Synchronica.Simulation;
using Synchronica.Simulation.Variables;
using System;

namespace Synchronica.Tests.Mock
{
    public sealed class Replayer : Replayer<RecorderData>
    {
        protected override void Replay(RecorderData data)
        {
            foreach (var gameObjectData in data.GameObjects)
            {
                var gameObject = GetObject(gameObjectData.Id);
                if (gameObject == null)
                {
                    gameObject = CreateGameObject(gameObjectData);
                }

                ReplayGameObject((ReplayerGameObject)gameObject, gameObjectData, data.StartTime);
            }
        }

        private GameObject CreateGameObject(GameObjectData data)
        {
            var gameObject = AddObject(data.Id, data.StartTime);

            if (data.Definitions != null)
            {
                foreach (var definition in data.Definitions)
                {
                    switch (definition.Type)
                    {
                        case VariableType.VBoolean:
                            gameObject.CreateBoolean(definition.Id, (bool)definition.InitialValue);
                            break;

                        case VariableType.VInt16:
                            gameObject.CreateInt16(definition.Id, (short)definition.InitialValue);
                            break;

                        case VariableType.VInt32:
                            gameObject.CreateInt32(definition.Id, (int)definition.InitialValue);
                            break;

                        case VariableType.VInt64:
                            gameObject.CreateInt64(definition.Id, (long)definition.InitialValue);
                            break;

                        case VariableType.VFloat:
                            gameObject.CreateFloat(definition.Id, (float)definition.InitialValue);
                            break;

                        default:
                            throw new NotSupportedException("VariableType");
                    }
                }
            }

            return gameObject;
        }

        private void ReplayGameObject(ReplayerGameObject gameObject, GameObjectData data, int startTime)
        {
            if (data.Variables != null)
            {
                foreach (var variableData in data.Variables)
                {
                    var variable = gameObject.GetVariable(variableData.Id);
                    if (variable == null)
                    {
                        throw new InvalidOperationException("Missing variable");
                    }

                    ReplayVariable(variable, variableData, startTime);
                }
            }

            if (data.EndTime > 0)
                gameObject.Destroy(data.EndTime);
        }

        private void ReplayVariable(Variable variable, VariableData data, int startTime)
        {
            variable.RemoveFramesAfter(startTime);

            foreach (var keyFrame in data.KeyFrames)
            {
                switch (keyFrame.Type)
                {
                    case KeyFrameType.Linear_Int32:
                        ((VInt32)variable).AppendLinearFrame(keyFrame.Time, (int)keyFrame.Value);
                        break;

                    case KeyFrameType.Pulse_Int32:
                        ((VInt32)variable).AppendPulseFrame(keyFrame.Time, (int)keyFrame.Value);
                        break;

                    case KeyFrameType.Step_Int32:
                        ((VInt32)variable).AppendStepFrame(keyFrame.Time, (int)keyFrame.Value);
                        break;

                    default:
                        throw new NotSupportedException("KeyFrameType");
                }
            }
        }
    }
}