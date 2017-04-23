using System;
using UnityEngine;
using System.Collections;

public class PowerupControl : MonoBehaviour
{
    public enum PowerupType
    {
        None,
        Multiball,
        Slower,
        Faster,
        Shield,
        Pointmania
    }

    [Serializable]
    public class PowerupDurations
    {
        public PowerupControl.PowerupType PowerupType;
        public int BounceNumber;
    }

    public PowerupType LastPowerup;
    public int PowerupCounter = 0;
    public PowerupDurations[] Durations;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetAllPowerups(PowerupType power)
    {
        var balls = FindObjectsOfType<BallControl>();

        foreach (var ball in balls)
        {
            ball.PersonalPowerupType = power;
        }

        LastPowerup = power;

        //PowerupCounter = 
    }
}
