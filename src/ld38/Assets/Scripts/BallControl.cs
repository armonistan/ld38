using System;
using UnityEngine;
using System.Collections;
using Assets;
using UnityEngine.SceneManagement;

public class BallControl : StatefulMonobehavior<BallControl.States>
{
    public enum States
    {
        Idle,
        Pause,
        Bounce,
        GameOver
    }

    public float Speed;
    public float DegAngle;

    public int PauseFrames = 5;

    public float RadAngle
    {
        get { return Mathf.Deg2Rad * DegAngle; }
        set { DegAngle = value * Mathf.Rad2Deg; }
    }

    public Vector2 Velocity
    {
        get
        {
            return new Vector2(Mathf.Cos(RadAngle), Mathf.Sin(RadAngle)) * Speed * Time.fixedDeltaTime;
        }
        set
        {
            Speed = value.magnitude / Time.fixedDeltaTime;
            RadAngle = Mathf.Atan2(value.y, value.x);
        }
    }

    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (Time.timeScale == GameControl.Paused)
	    {
	        return;
	    }

	    Debug.Log(Counter);

        switch (State)
	    {
	        case States.Idle:
                GetComponent<Renderer>().material.color = Color.white;

	            this.gameObject.transform.Translate(Velocity);
                break;
	        case States.Pause:
	            GetComponent<Renderer>().material.color = Color.yellow;

                if (Counter > PauseFrames)
	            {
	                State = States.GameOver;
	            }
	            else
	            {
	                IncrementCounter();
	            }
	            break;
	        case States.Bounce:
	            GetComponent<Renderer>().material.color = Color.red;

                this.gameObject.transform.Translate(Velocity);
                break;
	        case States.GameOver:
                SceneManager.LoadScene("TestBed");
	            break;
	        default:
	            throw new ArgumentOutOfRangeException();
	    }
	}
}
