using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICounterControl : StatefulMonoBehavior<UICounterControl.States> {

    public GameObject[] counterBubbles;
    //number of frames to sit at the full counter display
    public int FullFrames = 30;

    private SpawnControl _spawnControl;
    private PowerupControl _powerupControl;

    public enum States
    {
        Empty,
        Filling,
        Full,
        Emptying
    };

	// Use this for initialization
	void Start () {
        State = States.Empty;
        _spawnControl = FindObjectOfType<SpawnControl> ();
	    _powerupControl = FindObjectOfType<PowerupControl>();
	}
	
	// Update is called once per frame
	void Update () {
        switch(State)
        {
            case States.Empty:
            case States.Filling:
                if (_powerupControl.ActivePowerup != PowerupControl.PowerupType.None)
                {
                    State = States.Emptying;
                }
                else
                {
                    int illuminatedBubbles = _spawnControl.GetBouncesSinceLastSpawn();

                    ColorBubbles(illuminatedBubbles, Color.white);

                    if (illuminatedBubbles == counterBubbles.Length)
                    {
                        State = States.Full;
                    }
                }
                break;
            case States.Full:
                if (Counter > FullFrames)
                {
                    State = States.Empty;
                }
                else
                {
                    IncrementCounter();
                }
                break;
            case States.Emptying:
                int powerupBubbles = _powerupControl.PowerupCounter;

                ColorBubbles(powerupBubbles, _powerupControl.ActivePowerupColor);

                if (powerupBubbles == 0)
                {
                    State = States.Empty;
                }
                break;
            default:
                break;
        }
	}

    private void ColorBubbles(int illuminatedBubbles, Color baseColor)
    {
        for (int i = 0; i < counterBubbles.Length; i++)
        {
            Image imageComponent = counterBubbles[i].GetComponent<Image>();
            Color c = baseColor;

            if (i < illuminatedBubbles)
            {
                c.a = 1f;
                imageComponent.color = c;
            }
            else
            {
                c.a = .5f;
                imageComponent.color = c;
            }
        }
    }
}
