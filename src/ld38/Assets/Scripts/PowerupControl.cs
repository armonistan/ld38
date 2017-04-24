using System;
using UnityEngine;
using System.Collections;
using System.Linq;

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

    public PowerupType ActivePowerup
    {
        get { return PowerupCounter > 0 ? LastPowerup : PowerupType.None; }
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (ActivePowerup != LastPowerup)
	    {
	        if (LastPowerup == PowerupType.Shield)
	        {
	            LastPowerup = PowerupType.None;
	        }
	        else
	        {
	            SetAllPowerups(PowerupType.None);
	        }
	    }
	}

    public void SetAllPowerups(PowerupType power)
    {
        var balls = FindObjectsOfType<BallControl>();

        foreach (var ball in balls)
        {
            ball.PersonalPowerupType = power;
        }

        LastPowerup = power;

        PowerupCounter = Durations.First(duration => duration.PowerupType == power).BounceNumber;
    }
}
