using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public float spawnInterval = 2f;
    public float minY = -1.5f;
    public float maxY = 1.5f;
    public float spawnXPosition = 5f;

    private float timer;

    private GameManager gameManager;


    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        timer = spawnInterval;
    }

    void Update()
    {
        if (gameManager.IsPlaying)
        {
            timer += Time.deltaTime;

            if (timer >= spawnInterval)
            {
                SpawnObstacle();
                timer = 0;
            }
        }
    }


    void SpawnObstacle()
    {
        Vector3 position = new Vector3(spawnXPosition, Random.Range(minY, maxY), transform.position.z);
        Instantiate(obstaclePrefab, position, Quaternion.identity);
    }


}
