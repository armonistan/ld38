using UnityEngine;
using Assets.Scripts;
using System;
using Random = UnityEngine.Random;

public class SpawnControl : MonoBehaviour
{
    public int BouncesToSpawn = 5;
    public Bounds SpawnSpace;
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
        Debug.Log(_numberOfBouncesSinceLastSpawnCounter + "|" + BouncesToSpawn);

        if (_numberOfBouncesSinceLastSpawnCounter >= BouncesToSpawn)
        {
            SpawnObstacle();
        }
    }

    private void SpawnObstacle()
    {
        var newObstacle = Instantiate(ObstaclePrefab, GetSpawnPosition(), Quaternion.Euler(0, 0, 0)) as GameObject;
        var obstacleControl = newObstacle.GetComponent<ObstacleControl>();
        obstacleControl.CurrentPowerupType =
            (ObstacleControl.PowerupType)UnityEngine.Random.Range(0,
                Enum.GetValues(typeof(ObstacleControl.PowerupType)).Length);

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
}
