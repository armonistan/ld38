using System;
using UnityEngine;
using System.Linq;
using Assets;
using Assets.Scripts;

public class WallControl : StatefulMonoBehavior<WallControl.States>
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

	            if (Input.GetKey(EnableKey) && !FindObjectsOfType<WallControl>().Any(wall => wall.EnableKey != EnableKey && (wall.State == States.Primed || wall.State == States.Charging)) && !Input.GetKey(KeyCode.Space))
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

                if (Counter < ReflectFrames)
                {
                    IncrementCounter();
                }
                else
                {
                    State = States.LongCooldown;
                }
                break;
	        case States.ShortCooldown:
	            GetComponent<Renderer>().material.color = Color.cyan;

	            if (Counter < ShortCooldownFrames)
	            {
	                IncrementCounter();
	            }
	            else
	            {
	                State = States.Idle;
	            }
                break;
	        case States.LongCooldown:
	            GetComponent<Renderer>().material.color = Color.magenta;

	            if (Counter < LongCooldownFrames)
	            {
	                IncrementCounter();
	            }
	            else
	            {
	                State = States.Idle;
	            }
                break;
	        case States.Charging:
	            GetComponent<Renderer>().material.color = Color.yellow;

                if (Input.GetKey(EnableKey))
                {
                    if (Input.GetKey(KeyCode.Space))
                    {
                        IncrementCounter();
                    }
                    else
                    {
                        State = Counter > ChargeFrames ? States.StrongReflect : States.Reflect;
                    }
                }
                else
                {
                    State = States.Reflect;
                }
                break;
	        case States.StrongReflect:
	            GetComponent<Renderer>().material.color = Color.red;

	            if (Counter < StrongReflectFrames)
	            {
	                IncrementCounter();
	            }
	            else
	            {
	                State = States.LongCooldown;
	            }
                break;
	        default:
	            throw new ArgumentOutOfRangeException();
	    }
    }
}
