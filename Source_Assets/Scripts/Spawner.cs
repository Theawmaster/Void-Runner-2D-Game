using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Initialise
    [SerializeField] private GameObject[] obstaclePrefabs;
    [SerializeField] private Transform obstacleParent;
    public float obstacleSpawnTime = 2f;
    [Range(0.1f, 10f)] public float obstacleSpawnTimeFactor = 0.1f;
    private float timeAlive;
    private float timeUntilObstacleSpawn;
    public float obstacleSpeed = 1f;
    [Range(0.1f, 10f)] public float obstacleSpeedFactor   = 0.2f;

    private float accumulated_obstacle_spawn_time;
    private float accumulated_obstacle_speed;

    // Update is called once per frame
    private void Start()
    {
        GameManager.instance.on_GameOver.AddListener(ClearObstacles);
        GameManager.instance.on_Play.AddListener(ResetFactors); 
    }

    private void Update()
    {
        if (GameManager.instance.isPlaying)
        {
            // Update time alive
            timeAlive += Time.deltaTime;
            CalculateFactors();
            SpawnLoop();
        }
    }

    // Spawn loop
    private void SpawnLoop()
    {
        // Update time until obstacle spawn
        timeUntilObstacleSpawn += Time.deltaTime;
        if (timeUntilObstacleSpawn >= accumulated_obstacle_spawn_time) // Check if time until obstacle spawn is greater than or equal to accumulated obstacle spawn time 
        {
            Spawn();
            timeUntilObstacleSpawn = 0f; // Ensure spawn only once
        }
    }

    // Clear obstacles
    public void ClearObstacles()
    {
        Debug.Log("Clearing obstacles...");
        foreach (Transform child in obstacleParent)
        {
            Destroy(child.gameObject);
        }
    }

    // Calculate factors
    private void CalculateFactors()
    {
        // Calculate the accumulated obstacle spawn time and speed
        accumulated_obstacle_spawn_time = obstacleSpawnTime / Mathf.Pow(timeAlive, obstacleSpawnTimeFactor);
        // Calculate the accumulated obstacle speed
        accumulated_obstacle_speed = obstacleSpeed * Mathf.Pow(timeAlive, obstacleSpeedFactor);
    }

    private void ResetFactors()
    {
        timeAlive = 1f;
        accumulated_obstacle_spawn_time = obstacleSpawnTime;
        accumulated_obstacle_speed = obstacleSpeed; 
    }

    // Spawn obstacles
    private void Spawn()
    {
        GameObject obstacleToSpawn = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)]; // Randomly select an obstacle
        GameObject spawnedObstacle = Instantiate(obstacleToSpawn, transform.position, Quaternion.identity); // Instantiate the obstacle
        spawnedObstacle.transform.parent = obstacleParent; // Set the parent of the obstacle

        Rigidbody2D obstacleRB = spawnedObstacle.GetComponent<Rigidbody2D>(); // Get the rigidbody of the obstacle
        obstacleRB.linearVelocity = Vector2.left * accumulated_obstacle_speed ; // Move the obstacle to the left
        
    }
}
