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

    [Serializable]
    public class PulseData
    {
        public SpriteRenderer Sprite;
        public GameObject Start;
        public GameObject Destination;
    }

    public Vector2 Normal;
    public KeyCode EnableKey;

    public PulseData[] Pulses;
    public float PulseLerpRate = 0.1f;
    public Color ChargedPulseColor;

    public int ChargeFrames = 100;
    public int ReflectFrames = 5;
    public int StrongReflectFrames = 5;
    public int ShortCooldownFrames = 10;
    public int LongCooldownFrames = 30;

	public bool NeedsEnabled = true;

	// Use this for initialization
	void Start () {
	    if (this.NeedsEnabled)
	    {
	    	State = States.Idle;
	    }
	    else
	    {
	    	State = States.Primed;
	    }
	}

    public void Restart()
    {
        if (FindObjectOfType<GameControl>().GetGameState() == GameControl.States.EasyMode)
        {
            ChargeFrames = 37;
            ReflectFrames = 14;
            StrongReflectFrames = 20;
            ShortCooldownFrames = 11;
            LongCooldownFrames = 26;
        }
        else if (FindObjectOfType<GameControl>().GetGameState() == GameControl.States.HardMode)
        {
            ChargeFrames = 12;
            ReflectFrames = 9;
            StrongReflectFrames = 13;
            ShortCooldownFrames = 10;
            LongCooldownFrames = 27;
        }
    }

    // Update is called once per frame
    void Update () {
	    if (Time.timeScale == GameControl.Paused)
	    {
	        return;
	    }

        LerpPulses((State == States.Primed && NeedsEnabled) || State == States.Charging || State == States.Reflect || State == States.StrongReflect);
	    SetPulseColor((State == States.Charging && Counter > ChargeFrames) || State == States.StrongReflect);

        switch (State)
	    {
		case States.Idle:
				GetComponent<Renderer> ().material.color = Color.white;
				if (NeedsEnabled) 
				{
					if (Input.GetKey(EnableKey) && !FindObjectsOfType<WallControl>().Any(wall => wall.EnableKey != EnableKey && (wall.State == States.Primed || wall.State == States.Charging)) && !Input.GetKey(KeyCode.Space))
					{
						State = States.Primed;
					}
				}
	            break;
	        case States.Primed:
	            GetComponent<Renderer>().material.color = Color.gray;

				if (!Input.GetKey(EnableKey) && NeedsEnabled)
	            {
	                State = States.Idle;
	            }
				else if (Input.GetKey(KeyCode.Space) && NeedsEnabled)
	            {
	                State = States.Charging;
	            } 
	            else if (Input.GetKey(EnableKey) && !NeedsEnabled && !FindObjectsOfType<WallControl>().Any(wall => wall.EnableKey != EnableKey && wall.State == States.Charging))
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
	            else if (NeedsEnabled)
	            {
	                State = States.Idle;
	            }
	            else
	            {
	            	State = States.Primed;
	            }
                break;
	        case States.LongCooldown:
	            GetComponent<Renderer>().material.color = Color.magenta;

	            if (Counter < LongCooldownFrames)
	            {
	                IncrementCounter();
	            }
	            else if (NeedsEnabled)
	            {
	                State = States.Idle;
	            }
	            else
	            {
	            	State = States.Primed;
	            }
                break;
	        case States.Charging:
	            GetComponent<Renderer>().material.color = Color.yellow;
	            
                if (Input.GetKey(EnableKey))
                {
                    if ((Input.GetKey(KeyCode.Space) && NeedsEnabled) || !NeedsEnabled)
                    {
                        IncrementCounter();
                    }
                    else
                    {
                        State = Counter > ChargeFrames ? States.StrongReflect : States.Reflect;
                    }
                }
                else if (NeedsEnabled)
                {
                    State = States.Reflect;
                }
                else
                {
					State = Counter > ChargeFrames ? States.StrongReflect : States.Reflect;
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

    private void LerpPulses(bool on)
    {
        foreach (var pulse in Pulses)
        {
            var pointB = on ? pulse.Destination.transform.position : pulse.Start.transform.position;

            pulse.Sprite.transform.position = Vector2.Lerp(pulse.Sprite.transform.position, pointB, PulseLerpRate);
        }
    }

    public void SetPulseColor(bool charged)
    {
        foreach (var pulse in Pulses)
        {
            pulse.Sprite.color = charged ? ChargedPulseColor : Color.white;
        }
    }
}
