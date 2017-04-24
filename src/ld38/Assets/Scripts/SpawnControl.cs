using UnityEngine;
using Assets.Scripts;
using System;
using Random = UnityEngine.Random;

public class SpawnControl : MonoBehaviour
{
    public int BouncesToSpawn = 5;
	public int[] PowerupSpawningWeights = new int[Enum.GetValues(typeof(ObstacleControl.PowerupType)).Length];
    public GameObject ObstaclePrefab;
    public GameObject BallPrefab;

    private int _numberOfBouncesSinceLastSpawnCounter;

    // Use this for initialization
    void Start()
    {
        _numberOfBouncesSinceLastSpawnCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (_numberOfBouncesSinceLastSpawnCounter >= BouncesToSpawn)
        {
            SpawnObstacle();
        }
    }

    private void SpawnObstacle()
    {
        var newObstacle = Instantiate(ObstaclePrefab, GetSpawnPosition(), Quaternion.Euler(0, 0, 0)) as GameObject;
        var obstacleControl = newObstacle.GetComponent<ObstacleControl>();
		obstacleControl.CurrentPowerupType = GenerateObstaclePowerupType ();     

        _numberOfBouncesSinceLastSpawnCounter -= BouncesToSpawn;
    }

    private Vector2 GetSpawnPosition()
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();
        return new Vector2(Random.Range(spriteRenderer.bounds.min.x, spriteRenderer.bounds.max.x), Random.Range(spriteRenderer.bounds.min.y, spriteRenderer.bounds.max.y));
    }

    public void SpawnBall()
    {
        Instantiate(BallPrefab, GetSpawnPosition(), Quaternion.Euler(0, 0, 0));
    }

    public void IncrementNumberOfBouncesSinceLastSpawnCounter()
    {
        _numberOfBouncesSinceLastSpawnCounter++;
    }

    public int GetBouncesSinceLastSpawn()
    {
        return _numberOfBouncesSinceLastSpawnCounter;
    }

	private ObstacleControl.PowerupType GenerateObstaclePowerupType(){
		int weightRange = 0;
		int[] powerupSpawnWeightsCalculated = new int[6];

		for(int powerupSpawnWeightsIndex = 0; powerupSpawnWeightsIndex < PowerupSpawningWeights.Length; powerupSpawnWeightsIndex++){
			int powerupWeight = PowerupSpawningWeights [powerupSpawnWeightsIndex];
			weightRange += powerupWeight;
			powerupSpawnWeightsCalculated [powerupSpawnWeightsIndex] = weightRange;
		}

		int randomNumber = UnityEngine.Random.Range (0, weightRange+1);//+1 because range is exclusive
		Debug.Log(randomNumber);
		int powerupIndex = 0;

		for(int powerupSpawnWeightsIndex = 0; powerupSpawnWeightsIndex < powerupSpawnWeightsCalculated.Length; powerupSpawnWeightsIndex++){
			if (randomNumber <= powerupSpawnWeightsCalculated[powerupSpawnWeightsIndex]) {
				powerupIndex = powerupSpawnWeightsIndex;
				break;
			}
		}


		return (ObstacleControl.PowerupType)powerupIndex;
	}
}
