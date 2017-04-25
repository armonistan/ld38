using UnityEngine;
using Assets.Scripts;
using System;
using Random = UnityEngine.Random;

public class SpawnControl : MonoBehaviour
{
    public int BouncesToSpawn = 5;
    public int[] PowerupSpawningWeights = new int[Enum.GetValues(typeof(PowerupControl.PowerupType)).Length];

    public int xVariance = 100;
    public int yVariance = 100;

    public GameObject ObstaclePrefab;
    public GameObject BallPrefab;

    private int _numberOfBouncesSinceLastSpawnCounter;

    private PowerupControl _powerupControl;

    public Bounds SpawnBounds
    {
        get { return GetComponent<SpriteRenderer>().bounds; }
    }

    public PowerupControl.PowerupType NextPowerup { get; private set; }

    // Use this for initialization
    void Start()
    {
        Restart();
        _powerupControl = FindObjectOfType<PowerupControl>();
    }

    public void Restart()
    {
        _numberOfBouncesSinceLastSpawnCounter = 0;
        NextPowerup = GenerateObstaclePowerupType();
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
        var newObstacle = Instantiate(ObstaclePrefab, GetRandomSpawnPoint(SpawnBounds), Quaternion.Euler(0, 0, 0)) as GameObject;
        var obstacleControl = newObstacle.GetComponent<ObstacleControl>();
        obstacleControl.CurrentPowerupType = NextPowerup;

        Restart();
    }

    private Vector2 GetObstacleSpawnPoint(BallControl ball)
    {
        //TODO: Make this work
        float ballSlope = 0;
        float lineConstant = 0;
        float ballMinBoundX = ball.transform.position.x;
        float ballMaxBoundX = ball.transform.position.x;
        float ballMinBoundY = ball.transform.position.y;
        float ballMaxBoundY = ball.transform.position.y;
        float xPositionVariance = (Random.Range(-1 * xVariance, xVariance));
        float yPositionVariance = (Random.Range(-1 * yVariance, yVariance));
        bool infiniteBallSlope = ball.Velocity.x != 0;

        if (!infiniteBallSlope)
        {
            ballSlope = ball.Velocity.y / ball.Velocity.x;
            lineConstant = ball.transform.position.y - (ballSlope * ball.transform.position.x);

            if (ballSlope != 0)
            {
                ballMinBoundX = (SpawnBounds.min.y - lineConstant) / ballSlope;
                ballMaxBoundX = (SpawnBounds.max.y - lineConstant) / ballSlope;
            }
            else
            {
                ballMinBoundX = SpawnBounds.min.x;
                ballMaxBoundX = SpawnBounds.max.x;
            }
        }

        float spawnPositionX = Random.Range(ballMinBoundX, ballMaxBoundX);
        float spawnPositionY = Random.Range(1, 1);

        return new Vector2(spawnPositionX, spawnPositionY);
    }

    public Vector2 GetRandomSpawnPoint(Bounds bounds)
    {
        return new Vector2(Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y));
    }

    public void SpawnBalls()
    {
        var existingBalls = FindObjectsOfType<BallControl>();

        foreach (var ball in existingBalls)
        {
            var newBall = Instantiate(BallPrefab, ball.transform.position, Quaternion.Euler(0, 0, 0));

            if (newBall != null && _powerupControl.LastPowerup != PowerupControl.PowerupType.Multiball &&
                _powerupControl.LastPowerup != PowerupControl.PowerupType.None)
            {
                newBall.GetComponent<BallControl>().PersonalPowerupType = _powerupControl.LastPowerup;
            }
        }
        
    }

    public void RegisterBounce()
    {
        if (_powerupControl.ActivePowerup != PowerupControl.PowerupType.None)
        {
            _powerupControl.PowerupCounter--;
            Restart();
        }
        else
        {
            _numberOfBouncesSinceLastSpawnCounter++;
        }
    }

    public int GetBouncesSinceLastSpawn()
    {
        return _numberOfBouncesSinceLastSpawnCounter;
    }

    private PowerupControl.PowerupType GenerateObstaclePowerupType()
    {
        int weightRange = 0;
        int[] powerupSpawnWeightsCalculated = new int[6];

        for (int powerupSpawnWeightsIndex = 0;
            powerupSpawnWeightsIndex < PowerupSpawningWeights.Length;
            powerupSpawnWeightsIndex++)
        {
            int powerupWeight = PowerupSpawningWeights[powerupSpawnWeightsIndex];
            weightRange += powerupWeight;
            powerupSpawnWeightsCalculated[powerupSpawnWeightsIndex] = weightRange;
        }

        int randomNumber = UnityEngine.Random.Range(0, weightRange + 1); //+1 because range is exclusive
        int powerupIndex = 0;

        for (int powerupSpawnWeightsIndex = 0;
            powerupSpawnWeightsIndex < powerupSpawnWeightsCalculated.Length;
            powerupSpawnWeightsIndex++)
        {
            if (randomNumber <= powerupSpawnWeightsCalculated[powerupSpawnWeightsIndex])
            {
                powerupIndex = powerupSpawnWeightsIndex;
                break;
            }
        }

        return (PowerupControl.PowerupType) powerupIndex;
    }
}
