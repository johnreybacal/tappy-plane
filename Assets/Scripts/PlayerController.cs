using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 5f;

    public float rotationSpeed = 5f;
    public float maxRotationAngle = 30f;

    public AudioSource jumpSound;
    public AudioSource hitSound;

    private Rigidbody2D rb;
    private Collider2D collider2d;

    private GameManager gameManager;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider2d = GetComponent<Collider2D>();
        gameManager = FindFirstObjectByType<GameManager>();
    }

    void Update()
    {
        bool isTapped = Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0);
        if (isTapped && gameManager.IsPlaying)
        {
            Jump();
        }

        RotatePlayer();
    }

    public void Jump()
    {
        jumpSound.pitch = Random.Range(0.7f, 1.3f);
        jumpSound.Play();
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    void RotatePlayer()
    {
        // Calculate target rotation based on vertical velocity
        float targetRotation = Mathf.Clamp(rb.linearVelocity.y * rotationSpeed, -maxRotationAngle, maxRotationAngle);

        // Smoothly rotate towards target
        float currentRotation = transform.eulerAngles.z;
        if (currentRotation > 180) currentRotation -= 360; // Convert to -180 to 180 range

        float newRotation = Mathf.LerpAngle(currentRotation, targetRotation, Time.deltaTime * 3f);
        transform.rotation = Quaternion.Euler(0, 0, newRotation);
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        hitSound.pitch = Random.Range(0.7f, 1.3f);
        hitSound.Play();
        collider2d.enabled = false;
        Destroy(gameObject, 2);
        if (gameManager.IsPlaying)
        {
            gameManager.GameOver();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ScoreTrigger"))
        {
            gameManager.AddScore();
            other.enabled = false;
        }
    }
}
