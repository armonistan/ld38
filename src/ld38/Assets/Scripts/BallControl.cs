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

	public float DegAngle;
	public float StrongReflectMultiplier = 2;
    public float Speed;
	public float MaxSpeed = 4;
	public float BallSteeringMagnitude = 1;
    
    public int PauseFrames = 5;

    public float RadAngle
    {
        get { return Mathf.Deg2Rad * DegAngle; }
        set { DegAngle = value * Mathf.Rad2Deg; }
    }

	public KeyCode LeftBallSteering = KeyCode.LeftArrow;
	public KeyCode RightBallSteering = KeyCode.RightArrow;

	private float BallSteeringRadian;

	private float PERPENDICULAR_RADIAN = Mathf.PI/2;

    public Vector2 Velocity
    {
        get
        {
			return new Vector2 (Mathf.Cos (RadAngle), Mathf.Sin (RadAngle)) * Speed * Time.deltaTime;
		}
        set
        {
            Speed = value.magnitude / Time.deltaTime;
			if (Speed > MaxSpeed) {
				Speed = MaxSpeed;
			}
            RadAngle = Mathf.Atan2(value.y, value.x);
        }
    }

	public Vector2 BallSteeringVector
	{
		get
		{
			BallSteeringRadian = (PERPENDICULAR_RADIAN + RadAngle) % (Mathf.PI * 2);

			return new Vector2 (Mathf.Cos (BallSteeringRadian), Mathf.Sin (BallSteeringRadian)) * BallSteeringMagnitude * Time.deltaTime;
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
				GetComponent<Renderer> ().material.color = Color.white;
				UpdateBallPosition ();
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
				GetComponent<Renderer> ().material.color = Color.red;

				UpdateBallPosition ();
                break;
	        case States.GameOver:
                SceneManager.LoadScene("ArmonTestBed");
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
        ObstacleControl obs;

        if ((wall = other.gameObject.GetComponent<WallControl>()) != null)
        {
            HandleWall(wall);
        }
        else if ((obs = other.GetComponent<ObstacleControl>()) != null)
        {
            HandleObstacle(obs);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        WallControl wall;
        ObstacleControl obs;

        if ((wall = other.gameObject.GetComponent<WallControl>()) != null)
        {
            State = States.Idle;
        }
        else if ((obs = other.GetComponent<ObstacleControl>()) != null)
        {
            State = States.Idle;
        }
    }

    private void HandleObstacle(ObstacleControl obs)
    {
        if (State == States.Idle)
        {
            HandleBounce(transform.position - obs.transform.position);
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
					//sweet spot scoring
                    HandleBounce(wall.Normal);
                    wall.State = WallControl.States.Idle;
                }
                else if (State == States.Idle)
                {
                    HandleBounce(wall.Normal);
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
					//sweet spot scoring
                    HandleStrongBounce(wall.Normal);
                    wall.State = WallControl.States.Idle;
                }
                else if (State == States.Idle)
                {
                    HandleStrongBounce(wall.Normal);
                    wall.State = WallControl.States.ShortCooldown;
                }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void HandleBounce(Vector2 normal)
    {
        // Source: http://stackoverflow.com/questions/573084/how-to-calculate-bounce-angle
        var u = (Vector2.Dot(Velocity, normal) / Vector2.Dot(normal, normal)) * normal;
        var w = Velocity - u;

        //TODO: Add friction?
        Velocity = w - u;

        State = States.Bounce;
    }

	private void HandleStrongBounce(Vector2 normal){
		Speed *= StrongReflectMultiplier;
		HandleBounce (normal);
	}

	private void UpdateBallPosition(){
		if (Input.GetKey (LeftBallSteering)) {
			Velocity += BallSteeringVector;
		} else if (Input.GetKey (RightBallSteering)) {
			Velocity -= BallSteeringVector;
		}

		gameObject.transform.Translate(Velocity);
	}
}
