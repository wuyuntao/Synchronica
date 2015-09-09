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

using System;
using System.Collections.Generic;

namespace Synchronica.Simulation
{
    public sealed class Scene
    {
        private int elapsedTime;
        private Dictionary<int, Actor> actors = new Dictionary<int, Actor>();

        public Actor GetActor(int id)
        {
            Actor obj;
            this.actors.TryGetValue(id, out obj);
            return obj;
        }

        internal void AddActor(Actor actor)
        {
            this.actors.Add(actor.Id, actor);
        }

        internal void RemoveActor(Actor actor)
        {
            this.actors.Remove(actor.Id);
        }

        public int ElapsedTime
        {
            get { return this.elapsedTime; }
            internal set
            {
                if (value <= this.elapsedTime)
                    throw new ArgumentException("Already locked");

                this.elapsedTime = value;
            }
        }

        public IEnumerable<Actor> Actors
        {
            get { return this.actors.Values; }
        }

        public int Count
        {
            get { return this.actors.Count; }
        }
    }
}
