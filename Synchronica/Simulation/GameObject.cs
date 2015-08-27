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
using System.Collections.Generic;

namespace Synchronica.Simulation
{
    public sealed class GameObject
    {
        private Scene scene;
        private int id;
        private int nextVariableId;
        private List<IVariable> variables = new List<IVariable>();

        internal GameObject(Scene scene, int id)
        {
            this.scene = scene;
            this.id = id;
        }

        internal GameObjectData GetData(int startMilliseconds, int endMilliseconds)
        {
            GameObjectData od = null;

            foreach (var variable in this.variables)
            {
                var propertyData = variable.GetData(startMilliseconds, endMilliseconds);
                if (propertyData != null)
                {
                    if (od == null)
                        od = new GameObjectData(this.id);

                    od.AddProperty(propertyData);
                }
            }

            return od;
        }

        #region Variable definitions

        public VInt16 AddInt16(short value)
        {
            var variable = new VInt16(NextVariableId, value);
            this.variables.Add(variable);
            return variable;
        }

        public VInt32 AddInt32(short value)
        {
            var variable = new VInt32(NextVariableId, value);
            this.variables.Add(variable);
            return variable;
        }

        public VInt64 AddInt64(short value)
        {
            var variable = new VInt64(NextVariableId, value);
            this.variables.Add(variable);
            return variable;
        }

        public VFloat AddFloat(short value)
        {
            var variable = new VFloat(NextVariableId, value);
            this.variables.Add(variable);
            return variable;
        }
        
        #endregion

        public int Id
        {
            get { return this.id; }
        }

        private int NextVariableId
        {
            get { return this.nextVariableId++; }
        }
    }
}
