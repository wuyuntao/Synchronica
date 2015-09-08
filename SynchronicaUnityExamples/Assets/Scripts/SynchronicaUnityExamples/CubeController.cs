using UnityEngine;

public class CubeController : MonoBehaviour
{
    private SceneController scene;

    private Synchronica.Simulation.GameObject syncObject;
    private Synchronica.Simulation.Variable<float> posX;
    private Synchronica.Simulation.Variable<float> posY;
    private Synchronica.Simulation.Variable<float> posZ;

    private Transform root;

    public static CubeController Instantiate(SceneController sceneController, Synchronica.Simulation.GameObject syncObject)
    {
        var prefab = Resources.Load<GameObject>("Models/Cube/Cube");
        var cube = GameObject.Instantiate(prefab);

        var controller = cube.AddComponent<CubeController>();
        controller.name = string.Format("Cube #{0}", syncObject.Id);
        controller.scene = sceneController;
        controller.syncObject = syncObject;
        controller.root = cube.transform.Find("Root");

        return controller;
    }

    private void Update()
    {
        // Check for start game object
        if (!root.gameObject.activeSelf)
        {
            if (this.syncObject.StartTime <= this.scene.ElapsedTime)
            {
                this.posX = this.syncObject.GetVariable<float>(1);
                this.posY = this.syncObject.GetVariable<float>(2);
                this.posZ = this.syncObject.GetVariable<float>(3);

                Debug.Log(string.Format("x: {0}, y: {1}, z: {2}", this.posX, this.posY, this.posZ));

                UpdatePosition(this.syncObject.StartTime);

                root.gameObject.SetActive(true);
            }
        }
        else
        {
            UpdatePosition(this.scene.ElapsedTime);
        }
    }

    private void UpdatePosition(int time)
    {
        var x = this.posX.GetValue(time);
        var y = this.posY.GetValue(time);
        var z = this.posZ.GetValue(time);

        var pos = new Vector3(x, y, z);

        transform.position = pos;
    }
}
