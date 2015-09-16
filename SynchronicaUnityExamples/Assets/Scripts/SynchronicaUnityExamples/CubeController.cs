using Synchronica.Simulation;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    private SceneController scene;

    private Actor actor;
    private Variable<float> posX;
    private Variable<float> posY;
    private Variable<float> posZ;

    private Transform root;

    public static CubeController Instantiate(SceneController sceneController, Actor actor)
    {
        var prefab = Resources.Load<GameObject>("Models/Cube/Cube");
        var cube = GameObject.Instantiate(prefab);

        var controller = cube.AddComponent<CubeController>();
        controller.name = string.Format("Cube #{0}", actor.Id);
        controller.scene = sceneController;
        controller.actor = actor;
        controller.root = cube.transform.Find("Root");

        return controller;
    }

    private void Update()
    {
        // Check for start game object
        if (!root.gameObject.activeSelf)
        {
            if (this.actor.StartTime <= this.scene.ElapsedTime)
            {
                this.posX = this.actor.GetVariable<float>(1);
                this.posY = this.actor.GetVariable<float>(2);
                this.posZ = this.actor.GetVariable<float>(3);

                UpdatePosition(this.actor.StartTime);

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

        if (pos != transform.position)
        {
            Debug.Log(string.Format("Cube: {0}, pos: {1}, time: {2}", this.actor.Id, pos, time));

            transform.position = pos;
        }
    }
}
