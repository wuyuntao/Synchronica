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

using Synchronica.Schema;
using Synchronica.Simulation;
using System;

namespace Synchronica.Replayers
{
    public sealed class FlatBufferReplayer : Replayer<SynchronizeSceneData>
    {
        protected override void DeserializeRecord(SynchronizeSceneData data)
        {
            for (int i = 0; i < data.ObjectsLength; i++)
            {
                DeserializeGameObject(data.GetObjects(i));
            }
        }

        private void DeserializeGameObject(GameObjectData data)
        {
            var gameObject = GetObject(data.Id);
            if (gameObject == null)
            {
                if (data.EventsLength == 0 || data.GetEvents(0).Type != GameObjectEventType.Start)
                    throw new InvalidOperationException("Start event not found");

                gameObject = AddObject(data.Id, data.GetEvents(0).Time);
            }

            for (int i = 0; i < data.EventsLength; i++)
            {
                DeserializeGameObjectEvent(gameObject, data.GetEvents(i));
            }

            for (int i = 0; i < data.VariablesLength; i++)
            {
                DeserializeVariables(gameObject, data.GetVariables(i));
            }
        }

        private void DeserializeGameObjectEvent(GameObject gameObject, GameObjectEventData data)
        {
            switch (data.Type)
            {
                case GameObjectEventType.End:
                    RemoveObject(gameObject, data.Time);
                    break;

                case GameObjectEventType.Start:
                    break;

                default:
                    throw new NotSupportedException("EventType");
            }
        }

        private void DeserializeVariables(GameObject gameObject, VariableData data)
        {
            Variable variable = gameObject.GetVariable(data.Id);
            if (variable == null)
            {
                if (data.Parameters != null)
                {
                    variable = AddVariable(gameObject, data.Id, data.Parameters.Type);
                }
                else
                {
                    throw new InvalidOperationException("Parameters not found");
                }
            }

            for (int i = 0; i < data.KeyFramesLength; i++)
            {
                var frame = data.GetKeyFrames(i);

                DeserializeKeyFrame(variable, frame);
            }
        }

        private Variable AddVariable(GameObject gameObject, int id, VariableType type)
        {
            switch (type)
            {
                case VariableType.Boolean:
                    return AddBoolean(gameObject, id);

                case VariableType.Int16:
                    return AddInt16(gameObject, id);

                case VariableType.Int32:
                    return AddInt32(gameObject, id);

                case VariableType.Int64:
                    return AddInt64(gameObject, id);

                case VariableType.Float:
                    return AddFloat(gameObject, id);

                default:
                    throw new NotSupportedException("VariableType");
            }
        }

        private void DeserializeKeyFrame(Variable variable, KeyFrameData data)
        {
            switch (data.DataType)
            {
                #region Linear

                case KeyFrameUnion.LinearKeyFrameData_Int16:
                    {
                        var frame = data.GetData<LinearKeyFrameData_Int16>(new LinearKeyFrameData_Int16());

                        AddLinearFrame((Variable<short>)variable, frame.Time, frame.Value);
                    }
                    break;

                case KeyFrameUnion.LinearKeyFrameData_Int32:
                    {
                        var frame = data.GetData<LinearKeyFrameData_Int32>(new LinearKeyFrameData_Int32());

                        AddLinearFrame((Variable<int>)variable, frame.Time, frame.Value);
                    }
                    break;

                case KeyFrameUnion.LinearKeyFrameData_Int64:
                    {
                        var frame = data.GetData<LinearKeyFrameData_Int64>(new LinearKeyFrameData_Int64());

                        AddLinearFrame((Variable<long>)variable, frame.Time, frame.Value);
                    }
                    break;

                case KeyFrameUnion.LinearKeyFrameData_Float:
                    {
                        var frame = data.GetData<LinearKeyFrameData_Float>(new LinearKeyFrameData_Float());

                        AddLinearFrame((Variable<float>)variable, frame.Time, frame.Value);
                    }
                    break;

                #endregion

                #region Pulse

                case KeyFrameUnion.PulseKeyFrameData_Int16:
                    {
                        var frame = data.GetData<PulseKeyFrameData_Int16>(new PulseKeyFrameData_Int16());

                        AddPulseFrame((Variable<short>)variable, frame.Time, frame.Value);
                    }
                    break;

                case KeyFrameUnion.PulseKeyFrameData_Int32:
                    {
                        var frame = data.GetData<PulseKeyFrameData_Int32>(new PulseKeyFrameData_Int32());

                        AddPulseFrame((Variable<int>)variable, frame.Time, frame.Value);
                    }
                    break;

                case KeyFrameUnion.PulseKeyFrameData_Int64:
                    {
                        var frame = data.GetData<PulseKeyFrameData_Int64>(new PulseKeyFrameData_Int64());

                        AddPulseFrame((Variable<long>)variable, frame.Time, frame.Value);
                    }
                    break;

                case KeyFrameUnion.PulseKeyFrameData_Float:
                    {
                        var frame = data.GetData<PulseKeyFrameData_Float>(new PulseKeyFrameData_Float());

                        AddPulseFrame((Variable<float>)variable, frame.Time, frame.Value);
                    }
                    break;

                #endregion

                #region Step

                case KeyFrameUnion.StepKeyFrameData_Boolean:
                    {
                        var frame = data.GetData<StepKeyFrameData_Boolean>(new StepKeyFrameData_Boolean());

                        AddStepFrame((Variable<bool>)variable, frame.Time, frame.Value);
                    }
                    break;

                case KeyFrameUnion.StepKeyFrameData_Int16:
                    {
                        var frame = data.GetData<StepKeyFrameData_Int16>(new StepKeyFrameData_Int16());

                        AddStepFrame((Variable<short>)variable, frame.Time, frame.Value);
                    }
                    break;

                case KeyFrameUnion.StepKeyFrameData_Int32:
                    {
                        var frame = data.GetData<StepKeyFrameData_Int32>(new StepKeyFrameData_Int32());

                        AddStepFrame((Variable<int>)variable, frame.Time, frame.Value);
                    }
                    break;

                case KeyFrameUnion.StepKeyFrameData_Int64:
                    {
                        var frame = data.GetData<StepKeyFrameData_Int64>(new StepKeyFrameData_Int64());

                        AddStepFrame((Variable<long>)variable, frame.Time, frame.Value);
                    }
                    break;

                case KeyFrameUnion.StepKeyFrameData_Float:
                    {
                        var frame = data.GetData<StepKeyFrameData_Float>(new StepKeyFrameData_Float());

                        AddStepFrame((Variable<float>)variable, frame.Time, frame.Value);
                    }
                    break;

                #endregion

                default:
                    throw new NotSupportedException("Unknown type of keyFrame");
            }
        }
    }
}
