using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Synchronica.Simulation
{
    sealed class GameObjectManager<TGameObject>
        where TGameObject : GameObject
    {
        private Dictionary<int, TGameObject> objects = new Dictionary<int, TGameObject>();

        internal void AddObject(TGameObject gameObject)
        {
            this.objects.Add(gameObject.Id, gameObject);
        }

        internal void RemoveObject(TGameObject gameObject)
        {
            this.objects.Remove(gameObject.Id);
        }

        public TGameObject GetObject(int id)
        {
            TGameObject obj;
            this.objects.TryGetValue(id, out obj);
            return obj;
        }

        public IEnumerable<TGameObject> Objects
        {
            get { return this.objects.Values; }
        }

        public int Count
        {
            get { return this.objects.Count; }
        }
    }
}
