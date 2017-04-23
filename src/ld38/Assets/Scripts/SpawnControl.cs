using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using System;

public class SpawnControl : MonoBehaviour {

	public int BouncesToSpawn = 5;
	public GameObject ObstaclePrefab;
	public GameObject BallPrefab;

	private int _numberOfBouncesSinceLastSpawnCounter;

	// Use this for initialization
	void Start () {
		_numberOfBouncesSinceLastSpawnCounter = 0;
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (_numberOfBouncesSinceLastSpawnCounter + "|" + BouncesToSpawn);	
		if (_numberOfBouncesSinceLastSpawnCounter >= BouncesToSpawn) {
			SpawnObstacle ();
		}
	}

	private void SpawnObstacle(){
		GameObject obstacleToSpawn = ObstaclePrefab;
		ObstacleControl obstacleControl = obstacleToSpawn.GetComponent<ObstacleControl> ();
		obstacleControl.CurrentPowerupType = (ObstacleControl.PowerupType) UnityEngine.Random.Range(0, Enum.GetValues (typeof(ObstacleControl.PowerupType)).Length);

		Instantiate (ObstaclePrefab, GetSpawnPosition (), Quaternion.Euler (0, 0, 0));
		_numberOfBouncesSinceLastSpawnCounter -= BouncesToSpawn;
	}
		
	private Vector3 GetSpawnPosition(){
		Vector3 spawnPosition = new Vector3();

		return spawnPosition;
	}

	public void SpawnBall(){
		Instantiate (BallPrefab, GetSpawnPosition (), Quaternion.Euler (0, 0, 0));
	}

	public void IncrementNumberOfBouncesSinceLastSpawnCounter(){
		_numberOfBouncesSinceLastSpawnCounter++;
	}
}
