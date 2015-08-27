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

using Synchronica.Simulation.Data;
using Synchronica.Simulation.Variables;
using System;
using System.Collections.Generic;

namespace Synchronica.Simulation
{
    public sealed class GameObject
    {
        public const int MaxInternalEventId = 10;
        private const int CreateEventId = 1;
        private const int DestroyEventId = 2;

        private Scene scene;
        private int id;
        private int nextVariableId;
        private List<Variable> variables = new List<Variable>();
        private VInt32 events;

        internal GameObject(Scene scene, int id)
        {
            this.scene = scene;
            this.id = id;

            this.events = CreateInt32(0);
            CreateEventTrigger(scene.Milliseconds, CreateEventId, true);
        }

        internal GameObjectData GetData(int startMilliseconds, int endMilliseconds)
        {
            GameObjectData data = null;

            foreach (var variable in this.variables)
            {
                var variableData = variable.GetData(startMilliseconds, endMilliseconds);
                if (variableData != null)
                {
                    if (data == null)
                        data = new GameObjectData(this.id);

                    data.AddVariable(variableData);
                }
            }

            return data;
        }

        public void Destroy(int milliseconds)
        {
            CreateEventTrigger(milliseconds, DestroyEventId, true);
        }

        #region Variable definitions

        public VBoolean CreateBoolean(bool value)
        {
            var variable = new VBoolean(GetNextVariableId(), value);
            this.variables.Add(variable);
            return variable;
        }

        public VInt16 CreateInt16(short value)
        {
            var variable = new VInt16(GetNextVariableId(), value);
            this.variables.Add(variable);
            return variable;
        }

        public VInt32 CreateInt32(int value)
        {
            var variable = new VInt32(GetNextVariableId(), value);
            this.variables.Add(variable);
            return variable;
        }

        public VInt64 CreateInt64(long value)
        {
            var variable = new VInt64(GetNextVariableId(), value);
            this.variables.Add(variable);
            return variable;
        }

        public VFloat CreateFloat(float value)
        {
            var variable = new VFloat(GetNextVariableId(), value);
            this.variables.Add(variable);
            return variable;
        }

        private int GetNextVariableId()
        {
            return this.nextVariableId++;
        }

        #endregion
        
        public void CreateEventTrigger(int milliseconds, int eventId)
        {
            CreateEventTrigger(milliseconds, eventId, false);
        }

        private void CreateEventTrigger(int milliseconds, int eventId, bool isInternalEvent)
        {
            if (!isInternalEvent && eventId <= MaxInternalEventId)
                throw new ArgumentException("Event id is reserved for internal usage");

            this.events.AppendPulseFrame(milliseconds, eventId);
        }

        public int Id
        {
            get { return this.id; }
        }

        public bool IsDestroyed
        {
            get
            {
                var lastEvent = this.events.Tail;
                return lastEvent.Value == DestroyEventId && lastEvent.Milliseconds <= scene.Milliseconds;
            }
        }
    }
}
