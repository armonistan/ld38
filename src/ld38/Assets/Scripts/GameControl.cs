using UnityEngine;
using System.Collections;

public class GameControl : MonoBehaviour
{
    public int TargetFrameRate = 60;

    public static float Paused = 0;

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
    }
}
