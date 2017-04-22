using System;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallControl : StatefulMonoBehavior<BallControl.States>
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
            return new Vector2(Mathf.Cos(RadAngle), Mathf.Sin(RadAngle)) * Speed * Time.deltaTime;
        }
        set
        {
            Speed = value.magnitude / Time.deltaTime;
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

        switch (State)
	    {
	        case States.Idle:
                GetComponent<Renderer>().material.color = Color.white;

	            gameObject.transform.Translate(Velocity);
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

                gameObject.transform.Translate(Velocity);
                break;
	        case States.GameOver:
                SceneManager.LoadScene("TestBed");
	            break;
	        default:
	            throw new ArgumentOutOfRangeException();
	    }
	}

    void OnTriggerStay2D(Collider2D other)
    {
        if (Time.timeScale == GameControl.Paused)
        {
            return;
        }

        WallControl wall;

        if ((wall = other.gameObject.GetComponent<WallControl>()) != null)
        {
            HandleWall(wall);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        WallControl wall;

        if ((wall = other.gameObject.GetComponent<WallControl>()) != null)
        {
            State = States.Idle;
        }
    }

    private void HandleWall(WallControl wall)
    {
        switch (wall.State)
        {
            case WallControl.States.Idle:
            case WallControl.States.Primed:
            case WallControl.States.Charging:
                if (State == States.Idle)
                {
                    State = States.Pause;
                }
                break;
            case WallControl.States.Reflect:
                if (State == States.Pause)
                {
                    HandleWallBounce(wall);
                    wall.State = WallControl.States.Idle;
                }
                else if (State == States.Idle)
                {
                    HandleWallBounce(wall);
                    wall.State = WallControl.States.ShortCooldown;
                }
                break;
            case WallControl.States.ShortCooldown:
            case WallControl.States.LongCooldown:
                if (State != States.Bounce)
                {
                    State = States.GameOver;
                }
                break;
            case WallControl.States.StrongReflect:
                if (State == States.Pause)
                {
                    HandleWallBounce(wall);
                    wall.State = WallControl.States.Idle;
                }
                else if (State == States.Idle)
                {
                    HandleWallBounce(wall);
                    wall.State = WallControl.States.ShortCooldown;
                }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void HandleWallBounce(WallControl wall)
    {
        // Source: http://stackoverflow.com/questions/573084/how-to-calculate-bounce-angle
        var u = (Vector2.Dot(Velocity, wall.Normal) / Vector2.Dot(wall.Normal, wall.Normal)) * wall.Normal;
        var w = Velocity - u;

        //TODO: Add friction?
        Velocity = w - u;

        State = States.Bounce;
    }
}
