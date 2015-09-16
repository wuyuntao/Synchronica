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
using Synchronica.Simulation.Variables;
using System;

namespace Synchronica.Recorders
{
    public class ActorFactory
    {
        internal static void Initiailize(Actor actor, Action<ActorFactory> initializer)
        {
            if (actor == null)
                throw new ArgumentNullException("actor");

            if (initializer == null)
                throw new ArgumentNullException("initializer");

            var factory = new ActorFactory(actor);
            initializer(factory);

            // Reset actor instance to avoid being initialized multiple times
            factory.actor = null;
        }

        private Actor actor;

        ActorFactory(Actor actor)
        {
            this.actor = actor;
        }

        #region Variable

        public Variable<bool> AddBoolean(int id, bool value)
        {
            if (this.actor == null)
                throw new InvalidOperationException("Cannot add variable after this.actor initialized");

            var variable = new VBoolean(this.actor, id, new RecordState());
            variable.AddStepFrame(this.actor.StartTime, value);

            this.actor.AddVariable(variable);
            return variable;
        }

        public Variable<short> AddInt16(int id, short value)
        {
            if (this.actor == null)
                throw new InvalidOperationException("Cannot add variable after this.actor initialized");

            var variable = new VInt16(this.actor, id, new RecordState());
            variable.AddStepFrame(this.actor.StartTime, value);

            this.actor.AddVariable(variable);
            return variable;
        }

        public Variable<int> AddInt32(int id, int value)
        {
            if (this.actor == null)
                throw new InvalidOperationException("Cannot add variable after this.actor initialized");

            var variable = new VInt32(this.actor, id, new RecordState());
            variable.AddStepFrame(this.actor.StartTime, value);

            this.actor.AddVariable(variable);
            return variable;
        }

        public Variable<long> AddInt64(int id, long value)
        {
            if (this.actor == null)
                throw new InvalidOperationException("Cannot add variable after this.actor initialized");

            var variable = new VInt64(this.actor, id, new RecordState());
            variable.AddStepFrame(this.actor.StartTime, value);

            this.actor.AddVariable(variable);
            return variable;
        }

        public Variable<float> AddFloat(int id, float value)
        {
            if (this.actor == null)
                throw new InvalidOperationException("Cannot add variable after this.actor initialized");

            var variable = new VFloat(this.actor, id, new RecordState());
            variable.AddStepFrame(this.actor.StartTime, value);

            this.actor.AddVariable(variable);
            return variable;
        }

        #endregion
    }
}
