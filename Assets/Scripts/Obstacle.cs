using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float destroyXPosition = -10f;

    private GameManager gameManager;

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }

    void Update()
    {
        if (gameManager.IsPlaying)
        {
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;

            if (transform.position.x < destroyXPosition)
            {
                Destroy(gameObject);
            }
        }
    }
}
