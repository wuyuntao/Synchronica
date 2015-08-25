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
using System.Collections.Generic;

namespace Synchronica.Simulation
{
    public abstract class GameObject
    {
        private Scene scene;
        private int id;
        private int nextPropertyId;
        private List<Property> properties = new List<Property>();

        protected GameObject(Scene scene)
        {
            this.scene = scene;
            this.id = scene.NextObjectId;

            this.scene.AddObject(this);
        }

        internal GameObjectData GetData(int startMilliseconds, int endMilliseconds)
        {
            GameObjectData od = null;

            foreach (var property in this.properties)
            {
                var propertyData = property.GetData(startMilliseconds, endMilliseconds);
                if (propertyData != null)
                {
                    if (od == null)
                        od = new GameObjectData(this.id);

                    od.AddProperty(propertyData);
                }
            }

            return od;
        }

        protected int AddProperty<TValue>(Variable<TValue> variable)
        {
            var propertyId = this.nextPropertyId++;
            this.properties.Add(new Property(propertyId, variable));
            return propertyId;
        }

        protected void RemoveProperty(int propertyId)
        {
            this.properties.RemoveAll(p => p.Id == propertyId);
        }

        #region Property

        class Property
        {
            private int id;
            private IVariable variable;

            public Property(int id, IVariable variable)
            {
                this.id = id;
                this.variable = variable;
            }

            public PropertyData GetData(int startMilliseconds, int endMilliseconds)
            {
                PropertyData propertyData = null;

                var keyFrames = variable.GetKeyFrameData(startMilliseconds, endMilliseconds);
                if (keyFrames != null && keyFrames.Length > 0)
                {
                    propertyData = new PropertyData(this.id);

                    foreach (var keyFrame in keyFrames)
                        propertyData.AddFrame(keyFrame);
                }

                return propertyData;
            }

            public int Id
            {
                get { return this.id; }
            }

            public TVariable GetVariable<TVariable>()
                where TVariable : IVariable
            {
                return (TVariable)this.variable;
            }
        }

        #endregion

        public int Id
        {
            get { return this.id; }
        }
    }
}
