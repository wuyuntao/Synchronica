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

using FlatBuffers;
using Synchronica.Schema;
using Synchronica.Simulation;
using Synchronica.Simulation.KeyFrames;
using System;
using System.Collections.Generic;

namespace Synchronica.Recorders
{
    public sealed class FlatBufferRecorder : Recorder<FlatBufferBuilder>
    {
        private int bufferSize;

        public FlatBufferRecorder(int bufferSize = 1024)
        {
            this.bufferSize = bufferSize;
        }

        protected override FlatBufferBuilder SerializeRecord(int endTime)
        {
            var fbb = new FlatBufferBuilder(bufferSize);

            var oObjects = new List<Offset<GameObjectData>>();
            foreach (var gameObject in Objects)
            {
                var oObject = SerializeGameObject(fbb, gameObject);
                if (oObject != null)
                    oObjects.Add(oObject.Value);
            }

            if (oObjects.Count > 0)
            {
                var vObjects = SynchronizeSceneData.CreateObjectsVector(fbb, oObjects.ToArray());
                var oData = SynchronizeSceneData.CreateSynchronizeSceneData(fbb, Scene.ElapsedTime, endTime, vObjects);
                SynchronizeSceneData.FinishSynchronizeSceneDataBuffer(fbb, oData);

                return fbb;
            }
            else
            {
                return null;
            }
        }

        private Offset<GameObjectData>? SerializeGameObject(FlatBufferBuilder fbb, GameObject gameObject)
        {
            var oVariables = new List<Offset<VariableData>>();
            foreach (var variable in gameObject.Variables)
            {
                var oVariable = SerializeVariable(fbb, variable, gameObject.StartTime >= Scene.ElapsedTime);
                if (oVariable != null)
                    oVariables.Add(oVariable.Value);
            }

            var oEvents = new List<Offset<GameObjectEventData>>();
            if (gameObject.StartTime >= Scene.ElapsedTime)
            {
                var oEvent = GameObjectEventData.CreateGameObjectEventData(fbb, GameObjectEventType.Start, gameObject.StartTime);
                oEvents.Add(oEvent);
            }

            if (gameObject.EndTime >= Scene.ElapsedTime)
            {
                var oEvent = GameObjectEventData.CreateGameObjectEventData(fbb, GameObjectEventType.End, gameObject.EndTime);
                oEvents.Add(oEvent);
            }

            if (oVariables.Count > 0 || oEvents.Count > 0)
            {
                var vVariables = GameObjectData.CreateVariablesVector(fbb, oVariables.ToArray());
                var vEvents = GameObjectData.CreateEventsVector(fbb, oEvents.ToArray());
                var oObject = GameObjectData.CreateGameObjectData(fbb, gameObject.Id, vEvents, vVariables);

                return oObject;
            }
            else
            {
                return null;
            }
        }

        private Offset<VariableData>? SerializeVariable(FlatBufferBuilder fbb, Variable variable, bool includingParameters)
        {
            var oFrames = new List<Offset<KeyFrameData>>();
            foreach (var frame in variable.FindFramesAfter(Scene.ElapsedTime))
            {
                var oFrame = SerializeKeyFrame(fbb, frame);
                if (oFrame != null)
                    oFrames.Add(oFrame.Value);
            }

            if (oFrames.Count > 0 || includingParameters)
            {
                VariableData.StartVariableData(fbb);

                if (oFrames.Count > 0)
                {
                    var vFrames = VariableData.CreateKeyFramesVector(fbb, oFrames.ToArray());
                    VariableData.AddKeyFrames(fbb, vFrames);
                }

                if (includingParameters)
                {
                    var oParameters = VariableParameters.CreateVariableParameters(fbb, GetVariableType(variable));
                    VariableData.AddParameters(fbb, oParameters);
                }

                VariableData.AddId(fbb, variable.Id);

                return VariableData.EndVariableData(fbb);
            }
            else
            {
                return null;
            }
        }

        private static VariableType GetVariableType(Variable variable)
        {
            if (variable is Variable<bool>)
                return VariableType.Boolean;

            if (variable is Variable<short>)
                return VariableType.Int16;

            if (variable is Variable<int>)
                return VariableType.Int32;

            if (variable is Variable<long>)
                return VariableType.Int64;

            if (variable is Variable<float>)
                return VariableType.Float;

            throw new NotSupportedException("VariableType");
        }

        private Offset<KeyFrameData>? SerializeKeyFrame(FlatBufferBuilder fbb, KeyFrame frame)
        {
            #region Linear

            if (frame is LinearKeyFrame_Int16)
            {
                var oFrame = LinearKeyFrameData_Int16.CreateLinearKeyFrameData_Int16(fbb, frame.Time, (short)frame.Value);

                return KeyFrameData.CreateKeyFrameData(fbb, KeyFrameUnion.LinearKeyFrameData_Int16, oFrame.Value);
            }

            if (frame is LinearKeyFrame_Int32)
            {
                var oFrame = LinearKeyFrameData_Int32.CreateLinearKeyFrameData_Int32(fbb, frame.Time, (int)frame.Value);

                return KeyFrameData.CreateKeyFrameData(fbb, KeyFrameUnion.LinearKeyFrameData_Int32, oFrame.Value);
            }

            if (frame is LinearKeyFrame_Int64)
            {
                var oFrame = LinearKeyFrameData_Int64.CreateLinearKeyFrameData_Int64(fbb, frame.Time, (long)frame.Value);

                return KeyFrameData.CreateKeyFrameData(fbb, KeyFrameUnion.LinearKeyFrameData_Int64, oFrame.Value);
            }

            if (frame is LinearKeyFrame_Float)
            {
                var oFrame = LinearKeyFrameData_Float.CreateLinearKeyFrameData_Float(fbb, frame.Time, (float)frame.Value);

                return KeyFrameData.CreateKeyFrameData(fbb, KeyFrameUnion.LinearKeyFrameData_Float, oFrame.Value);
            }

            #endregion

            #region Pulse

            if (frame is PulseKeyFrame<short>)
            {
                var oFrame = PulseKeyFrameData_Int16.CreatePulseKeyFrameData_Int16(fbb, frame.Time, (short)frame.Value);

                return KeyFrameData.CreateKeyFrameData(fbb, KeyFrameUnion.PulseKeyFrameData_Int16, oFrame.Value);
            }

            if (frame is PulseKeyFrame<int>)
            {
                var oFrame = PulseKeyFrameData_Int32.CreatePulseKeyFrameData_Int32(fbb, frame.Time, (int)frame.Value);

                return KeyFrameData.CreateKeyFrameData(fbb, KeyFrameUnion.PulseKeyFrameData_Int32, oFrame.Value);
            }

            if (frame is PulseKeyFrame<long>)
            {
                var oFrame = PulseKeyFrameData_Int64.CreatePulseKeyFrameData_Int64(fbb, frame.Time, (long)frame.Value);

                return KeyFrameData.CreateKeyFrameData(fbb, KeyFrameUnion.PulseKeyFrameData_Int64, oFrame.Value);
            }

            if (frame is PulseKeyFrame<float>)
            {
                var oFrame = PulseKeyFrameData_Float.CreatePulseKeyFrameData_Float(fbb, frame.Time, (float)frame.Value);

                return KeyFrameData.CreateKeyFrameData(fbb, KeyFrameUnion.PulseKeyFrameData_Float, oFrame.Value);
            }

            #endregion

            #region Step

            if (frame is StepKeyFrame<bool>)
            {
                var oFrame = StepKeyFrameData_Boolean.CreateStepKeyFrameData_Boolean(fbb, frame.Time, (bool)frame.Value);

                return KeyFrameData.CreateKeyFrameData(fbb, KeyFrameUnion.StepKeyFrameData_Boolean, oFrame.Value);
            }

            if (frame is StepKeyFrame<short>)
            {
                var oFrame = StepKeyFrameData_Int16.CreateStepKeyFrameData_Int16(fbb, frame.Time, (short)frame.Value);

                return KeyFrameData.CreateKeyFrameData(fbb, KeyFrameUnion.StepKeyFrameData_Int16, oFrame.Value);
            }

            if (frame is StepKeyFrame<int>)
            {
                var oFrame = StepKeyFrameData_Int32.CreateStepKeyFrameData_Int32(fbb, frame.Time, (int)frame.Value);

                return KeyFrameData.CreateKeyFrameData(fbb, KeyFrameUnion.StepKeyFrameData_Int32, oFrame.Value);
            }

            if (frame is StepKeyFrame<long>)
            {
                var oFrame = StepKeyFrameData_Int64.CreateStepKeyFrameData_Int64(fbb, frame.Time, (long)frame.Value);

                return KeyFrameData.CreateKeyFrameData(fbb, KeyFrameUnion.StepKeyFrameData_Int64, oFrame.Value);
            }

            if (frame is StepKeyFrame<float>)
            {
                var oFrame = StepKeyFrameData_Float.CreateStepKeyFrameData_Float(fbb, frame.Time, (float)frame.Value);

                return KeyFrameData.CreateKeyFrameData(fbb, KeyFrameUnion.StepKeyFrameData_Float, oFrame.Value);
            }

            #endregion

            throw new NotSupportedException("KeyFrameType");
        }
    }
}
