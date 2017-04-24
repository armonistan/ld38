using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Assets.Scripts;

public class GameControl : StatefulMonoBehavior<GameControl.States>
{
    public int TargetFrameRate = 60;
    public static float Paused = 0;
    public string Scene = "TestBed";

    public GameObject BallPrefab;

    public KeyCode StartEasyMode = KeyCode.Alpha1;
    public KeyCode StartHardMode = KeyCode.Alpha2;

    private PowerupControl _powerupControl;

    public enum States
    {
        Menu,
        EasyMode,
        HardMode,
        GameOver
    };

    // Use this for initialization
    void Start ()
	{
	    QualitySettings.vSyncCount = 0;
	    Application.targetFrameRate = TargetFrameRate;

	    _powerupControl = FindObjectOfType<PowerupControl>();
	}
	
	// Update is called once per frame
	void Update () {
	    QualitySettings.vSyncCount = 0;
	    Application.targetFrameRate = TargetFrameRate;

        switch(State)
        {
            case States.Menu:
                if (Input.GetKey(StartEasyMode))
                {
                    State = States.EasyMode;
                    SetupGame();
                }
                else if (Input.GetKey(StartHardMode))
                {
                    State = States.HardMode;
                    SetupGame();
                }
                break;
            case States.EasyMode:
            case States.HardMode:
                GameplayUpdate();
                break;
            case States.GameOver:
                // need to put together a game over screen
                GameOver();
                State = States.Menu;
                break;
            default:
                break;
        }
    }

    public GameControl.States GetGameState()
    {
        return State;
    }

    public void Quit()
    {
        Debug.Log("quitting");
        Application.Quit();
    }

    private void GameplayUpdate()
    {
        if (FindObjectsOfType<BallControl>().Length == 0)
        {
            State = States.GameOver;
        }
    }

    private void SetupGame()
    {
        FindObjectOfType<UiControl>().Restart();
        FindObjectOfType<SpawnControl>().Restart();
        FindObjectOfType<StoryControl>().Restart();
        FindObjectOfType<StoryControl>().tellStory = true;
        Instantiate(BallPrefab, Vector2.zero, Quaternion.Euler(0, 0, 0));
    }

    private void GameOver()
    {
        BallControl[] balls = FindObjectsOfType<BallControl>();
        foreach (BallControl ball in balls)
        {
            Destroy(ball.gameObject);
        }

        ObstacleControl[] obstacles = FindObjectsOfType<ObstacleControl>();
        foreach (ObstacleControl obstacle in obstacles)
        {
            Destroy(obstacle.gameObject);
        }

        FindObjectOfType<StoryControl>().tellStory = false;

        var pointManias = FindObjectsOfType<PointManiaControl>();
        foreach (var pointMania in pointManias)
        {
            Destroy(pointMania.gameObject);
        }

        _powerupControl.Reset();
    }
}
