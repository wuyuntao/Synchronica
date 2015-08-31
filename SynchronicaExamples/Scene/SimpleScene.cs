using Synchronica.Examples.Schema;
using Synchronica.Recorders;
using Synchronica.Simulation;
using System.Collections.Generic;

namespace Synchronica.Examples.Scene
{
    class SimpleScene
    {
        private FlatBufferRecorder recorder = new FlatBufferRecorder();

        private List<Cube> cubes = new List<Cube>();

        private object cubesLock = new object();

        #region Cube

        class Cube
        {
            public GameObject gameObject;
            public string clientName;

            public Cube(GameObject gameObject)
            {
                this.gameObject = gameObject;
            }
        }

        #endregion

        public SimpleScene()
        {
            CreateCube(10, 5, 10);
            CreateCube(-10, 5, 10);
            CreateCube(-10, 5, -10);
            CreateCube(10, 5, -10);
        }

        private void CreateCube(float posX, float posY, float posZ)
        {
            var gameObject = recorder.AddObject(0);
            recorder.AddFloat(gameObject, posX);
            recorder.AddFloat(gameObject, posY);
            recorder.AddFloat(gameObject, posZ);

            var cube = new Cube(gameObject);
            this.cubes.Add(cube);
        }

        public int AllocateCube(string clientName)
        {
            lock (this.cubesLock)
            {
                var cube = this.cubes.Find(c => c.clientName == null);
                if (cube != null)
                {
                    cube.clientName = clientName;
                    return cube.gameObject.Id;
                }
                else
                {
                    return 0;
                }
            }
        }

        public void Input(int objectId, int time, Command command)
        {
        }
    }
}
