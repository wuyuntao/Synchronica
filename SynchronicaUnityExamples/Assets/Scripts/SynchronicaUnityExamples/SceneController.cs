using Synchronica.Examples.Schema;
using Synchronica.Simulation;
using Synchronica.Unity.Examples;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    private SimpleClient client;

    private float elapsedTime;

    private void Start()
    {
        this.client = new SimpleClient("Unity1", "127.0.0.1", 4000);
        this.client.Replayer.OnNewActor += Replayer_OnNewActor;

        this.client.Login();
    }

    private void Replayer_OnNewActor(Actor gameObject)
    {
        CubeController.Instantiate(this, gameObject);
    }

    private void Update()
    {
        this.client.Update();

        this.elapsedTime += Time.deltaTime;
    }

    private void OnGUI()
    {
        if (Event.current.isKey)
        {
            Command command = 0;
            switch (Event.current.keyCode)
            {
                case KeyCode.UpArrow:
                    command = Command.Up;
                    break;

                case KeyCode.DownArrow:
                    command = Command.Down;
                    break;

                case KeyCode.LeftArrow:
                    command = Command.Left;
                    break;

                case KeyCode.RightArrow:
                    command = Command.Right;
                    break;

                default:
                    return;
            }

            this.client.Input(command);
        }
    }

    public int ElapsedTime
    {
        get { return Mathf.FloorToInt(Mathf.Max(0, this.elapsedTime * 1000)); }
    }
}
