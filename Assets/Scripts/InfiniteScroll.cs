using UnityEngine;

public class InfiniteScroll : MonoBehaviour
{
    public float scrollSpeed = 0.5f;
    public float width;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        transform.position += Vector3.left * scrollSpeed * Time.deltaTime;

        if (transform.position.x < -width)
        {
            RepositionBackground();
        }
    }

    void RepositionBackground()
    {
        Vector3 offset = new Vector3(width * 2f, 0, 0);

        transform.position += offset;

        spriteRenderer.flipX = Random.Range(0, 2) == 0;
    }

}
