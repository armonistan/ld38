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
    public class PowerupData
    {
        public PowerupControl.PowerupType PowerupType;
        public int BounceNumber;
        public Color PowerupColor;
    }

    public PowerupType LastPowerup;
    public int PowerupCounter = 0;
    public PowerupData[] Data;

    public int PointmaniaCount;
    public PointManiaControl PointManiaPrefab;

    private SpawnControl _spawnControl;

    public PowerupType ActivePowerup
    {
        get { return PowerupCounter > 0 ? LastPowerup : PowerupType.None; }
    }

    // Use this for initialization
    void Start ()
    {
        _spawnControl = FindObjectOfType<SpawnControl>();
    }

    public void Reset()
    {
        LastPowerup = PowerupType.None;
        PowerupCounter = 0;

        CleanupPointManiaPoints();
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
	            if (LastPowerup == PowerupType.Pointmania)
	            {
	                CleanupPointManiaPoints();
                }

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

        PowerupCounter = Data.First(duration => duration.PowerupType == power).BounceNumber;

        if (power == PowerupType.Pointmania)
        {
            SpawnPointManiaPoints();
        }
    }

    public Color GetPowerupColor(PowerupType powerup)
    {
        return Data.First(data => data.PowerupType == powerup).PowerupColor;
    }

    private void SpawnPointManiaPoints()
    {
        for (int p = 0; p < PointmaniaCount; p++)
        {
            Instantiate(PointManiaPrefab, _spawnControl.GetRandomSpawnPoint(_spawnControl.SpawnBounds),
                Quaternion.identity);
        }
    }

    private void CleanupPointManiaPoints()
    {
        foreach (var pointMania in FindObjectsOfType<PointManiaControl>())
        {
            Destroy(pointMania.gameObject);
        }
    }
}
