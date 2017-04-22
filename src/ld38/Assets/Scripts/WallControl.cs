using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WallControl : MonoBehaviour
{
    public enum States
    {
        Idle,
        HitReady,
        HitPause,
        Cooldown,
        Dead
    }

    public Vector2 Normal;
    public States State;

    public int HitPauseFrames = 5;
    public int HitReadyFrames = 10;
    public int CooldownFrames = 10;

    private Vector2? ballInitialVelocity;
    private Dictionary<States, int> counters;

	// Use this for initialization
	void Start () {
	    counters = new Dictionary<States, int>();

	    foreach (States state in Enum.GetValues(typeof(States)))
	    {
	        counters.Add(state, 0);
	    }
	}
	
	// Update is called once per frame
	void Update () {
	    switch (State)
	    {
	        case States.Idle:
	            if (Input.GetKey(KeyCode.Space))
	            {
	                State = States.HitReady;
	            }
	            break;
	        case States.HitReady:
	            if (counters[States.HitReady] > HitReadyFrames)
	            {
	                counters[States.HitReady] = 0;
	                State = States.Cooldown;
	            }
	            else
	            {
	                counters[States.HitReady]++;
	            }
	            break;
	        case States.HitPause:
	            if (counters[States.HitPause] > HitPauseFrames)
	            {
	                counters[States.HitPause] = 0;
	                State = States.Cooldown;
	            }
	            else
	            {
	                counters[States.HitPause]++;
	            }
                break;
	        case States.Cooldown:
	            if (counters[States.Cooldown] > CooldownFrames)
	            {
	                counters[States.Cooldown] = 0;
	                State = States.Idle;
	            }
	            else
	            {
	                counters[States.Cooldown]++;
	            }
                break;
	        case States.Dead:
	            GetComponent<Renderer>().enabled = false;
	            break;
	        default:
	            throw new ArgumentOutOfRangeException();
	    }
    }

    void OnCollisionStay2D(Collision2D coll)
    {
        BallControl ball;

        if ((ball = coll.gameObject.GetComponent<BallControl>()) != null)
        {
            switch (State)
            {
                case States.Idle:
                    if (ball.Velocity == Vector2.zero)
                    {
                        Debug.Log("wat");
                    }

                    ballInitialVelocity = ball.Velocity;
                    ball.Velocity = Vector2.zero;
                    counters[States.HitPause] = 0;
                    State = States.HitPause;
                    break;
                case States.HitReady:
                    HandleBallBounce(ball, ball.Velocity);
                    GetComponent<Renderer>().material.color = Color.blue;
                    State = States.Cooldown;
                    break;
                case States.HitPause:
                    if (counters[States.HitPause] > HitPauseFrames)
                    {
                        State = States.Dead;
                    }
                    else if (Input.GetKey(KeyCode.Space))
                    {
                        HandleBallBounce(ball, ballInitialVelocity.Value);
                        GetComponent<Renderer>().material.color = Color.green;
                        ballInitialVelocity = null;
                        State = States.Cooldown;
                    }
                    break;
                case States.Cooldown:
                    break;
                case States.Dead:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    private void HandleBallBounce(BallControl ball, Vector2 velocity)
    {
        // Source: http://stackoverflow.com/questions/573084/how-to-calculate-bounce-angle
        var u = (Vector2.Dot(velocity, Normal) / Vector2.Dot(Normal, Normal)) * Normal;
        var w = velocity - u;

        //TODO: Add friction?
        ball.Velocity = w - u;

        if (ball.Velocity == Vector2.zero)
        {
            Debug.Log("wat");
        }
    }
}
