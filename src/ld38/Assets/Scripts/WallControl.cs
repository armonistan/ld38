using System;
using UnityEngine;
using System.Linq;
using Assets;

public class WallControl : StatefulMonobehavior<WallControl.States>
{
    public enum States
    {
        Idle,
        Primed,
        Reflect,
        ShortCooldown,
        LongCooldown,
        Charging,
        StrongReflect
    }

    public Vector2 Normal;
    public KeyCode EnableKey;

    public int ChargeFrames = 100;
    public int ReflectFrames = 5;
    public int StrongReflectFrames = 5;
    public int ShortCooldownFrames = 10;
    public int LongCooldownFrames = 30;

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

	            if (Input.GetKey(EnableKey) && !FindObjectsOfType<WallControl>().Any(wall => wall.EnableKey != EnableKey && (wall.State == States.Primed || wall.State == States.Charging)))
	            {
	                State = States.Primed;
	            }
	            break;
	        case States.Primed:
	            GetComponent<Renderer>().material.color = Color.gray;

	            if (!Input.GetKey(EnableKey))
	            {
	                State = States.Idle;
	            }
                else if (Input.GetKey(KeyCode.Space))
	            {
	                State = States.Charging;
	            }
	            break;
            case States.Reflect:
                GetComponent<Renderer>().material.color = Color.blue;

                if (StateCounters[State] < ReflectFrames)
                {
                    IncrementCounter(State);
                }
                else
                {
                    ResetCounter(State);
                    State = States.ShortCooldown;
                }
                break;
	        case States.ShortCooldown:
	            GetComponent<Renderer>().material.color = Color.cyan;

	            if (StateCounters[State] < ShortCooldownFrames)
	            {
	                IncrementCounter(State);
	            }
	            else
	            {
	                ResetCounter(State);
	                State = States.Idle;
	            }
                break;
	        case States.LongCooldown:
	            GetComponent<Renderer>().material.color = Color.magenta;

	            if (StateCounters[State] < LongCooldownFrames)
	            {
	                IncrementCounter(State);
	            }
	            else
	            {
	                ResetCounter(State);
	                State = States.Idle;
	            }
                break;
	        case States.Charging:
	            GetComponent<Renderer>().material.color = Color.yellow;

	            if (Input.GetKey(EnableKey) && Input.GetKey(KeyCode.Space))
	            {
	                if (StateCounters[State] > ChargeFrames)
	                {
	                    ResetCounter(State);
	                    State = States.StrongReflect;
	                }
	                else
	                {
	                    IncrementCounter(State);
                    }
                }
	            else
	            {
	                ResetCounter(State);
	                State = States.Reflect;
                }
                break;
	        case States.StrongReflect:
	            GetComponent<Renderer>().material.color = Color.red;

	            if (StateCounters[State] < StrongReflectFrames)
	            {
	                IncrementCounter(State);
	            }
	            else
	            {
	                ResetCounter(State);
	                State = States.LongCooldown;
	            }
                break;
	        default:
	            throw new ArgumentOutOfRangeException();
	    }
    }

    void OnCollisionStay2D(Collision2D coll)
    {
        if (Time.timeScale == GameControl.Paused)
        {
            return;
        }

        BallControl ball;

        if ((ball = coll.gameObject.GetComponent<BallControl>()) != null)
        {
            switch (State)
            {
                case States.Idle:
                case States.Primed:
                    if (ball.State == BallControl.States.Idle)
                    {
                        ball.State = BallControl.States.Pause;
                    }
                    break;
                case States.Reflect:
                    if (ball.State != BallControl.States.Bounce)
                    {
                        HandleBallBounce(ball);
                        ResetCounter(State);
                        State = States.Idle;
                        ball.State = BallControl.States.Bounce;
                    }
                    break;
                case States.ShortCooldown:
                    ball.State = BallControl.States.GameOver;
                    break;
                case States.LongCooldown:
                    ball.State = BallControl.States.GameOver;
                    break;
                case States.Charging:
                    if (ball.State == BallControl.States.Idle)
                    {
                        ball.State = BallControl.States.Pause;
                    }
                    break;
                case States.StrongReflect:
                    if (ball.State != BallControl.States.Bounce)
                    {
                        HandleBallBounce(ball);
                        ResetCounter(State);
                        State = States.Idle;
                        ball.State = BallControl.States.Bounce;
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        BallControl ball;

        if ((ball = coll.gameObject.GetComponent<BallControl>()) != null)
        {
            ball.State = BallControl.States.Idle;
        }
    }

    private void HandleBallBounce(BallControl ball)
    {
        // Source: http://stackoverflow.com/questions/573084/how-to-calculate-bounce-angle
        var u = (Vector2.Dot(ball.Velocity, Normal) / Vector2.Dot(Normal, Normal)) * Normal;
        var w = ball.Velocity - u;

        //TODO: Add friction?
        ball.Velocity = w - u;
    }
}
