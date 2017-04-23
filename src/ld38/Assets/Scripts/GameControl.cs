using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    public int TargetFrameRate = 60;
    public static float Paused = 0;
    public string Scene = "TestBed";

    // Use this for initialization
    void Start ()
	{
	    QualitySettings.vSyncCount = 0;
	    Application.targetFrameRate = TargetFrameRate;
	}
	
	// Update is called once per frame
	void Update () {
	    QualitySettings.vSyncCount = 0;
	    Application.targetFrameRate = TargetFrameRate;

	    if (FindObjectsOfType<BallControl>().Length == 0)
	    {
	        SceneManager.LoadScene(Scene);
        }
    }
}
