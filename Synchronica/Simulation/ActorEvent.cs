using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Synchronica.Simulation
{
    public sealed class ActorEvent
    {
        const int StartEvent = 1;
        const int EndEvent = 2;
        const int ReservedInternalId = 10;

        private int id;
        private int time;

        internal ActorEvent(int id, int time, bool isInternalEvent)
        {
            if (!isInternalEvent && id <= ReservedInternalId)
                throw new ArgumentException(string.Format("Event id is reserved for internal usage"));

            this.id = id;
            this.time = time;
        }

        public int Id
        {
            get { return this.id; }
        }

        public int Time
        {
            get { return this.time; }
        }
    }
}
