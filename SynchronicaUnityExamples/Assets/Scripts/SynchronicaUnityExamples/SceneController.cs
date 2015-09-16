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

        if (Input.GetKeyDown(KeyCode.UpArrow))
            this.client.Input(Command.Up);
        else if (Input.GetKeyDown(KeyCode.DownArrow))
            this.client.Input(Command.Down);
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            this.client.Input(Command.Left);
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            this.client.Input(Command.Right);

        var replayerElapsedTime = this.client.Replayer.Scene.ElapsedTime;
        var timeDelay = replayerElapsedTime - ElapsedTime;
        if (timeDelay > 1000)
        {
            Debug.Log(string.Format("Fix elapsed time: {0} -> {1}", ElapsedTime, replayerElapsedTime));

            this.elapsedTime = ((float)replayerElapsedTime - 100) / 1000;
        }
        else if (timeDelay > 200)
        {
            Debug.Log("Fast forward elapsed time");

            this.elapsedTime += Time.deltaTime * 2;
        }
        else
            this.elapsedTime += Time.deltaTime;
    }

    public int ElapsedTime
    {
        get { return Mathf.FloorToInt(Mathf.Max(0, this.elapsedTime * 1000)); }
    }
}
