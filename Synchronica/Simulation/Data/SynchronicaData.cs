using System.Collections.Generic;

namespace Synchronica.Simulation.Data
{
    public sealed class SynchronicaData
    {
        private List<SceneData> scenes = new List<SceneData>();

        public void AddScene(SceneData scene)
        {
            this.scenes.Add(scene);
        }

        public IEnumerable<SceneData> Scenes
        {
            get { return this.scenes; }
        }
    }
}
