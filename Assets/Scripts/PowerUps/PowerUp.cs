using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    public float popUpSpeed = 2f;
    public float moveSpeed = 2f;
    public float popUpHeight = 0.5f;

    protected Vector2 initialPosition;
    protected bool isPoppedUp = false;
    protected bool isMoving = false;
    protected Rigidbody2D rb;

    public bool CanMove;

    protected virtual void Start()
    {
        initialPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0; // Disable gravity until fully popped up
    }

    protected virtual void Update()
    {
        if (!isPoppedUp)
        {
            transform.position += Vector3.up * popUpSpeed * Time.deltaTime;
            if (transform.position.y >= initialPosition.y + popUpHeight)
            {
                transform.position = new Vector2(transform.position.x, initialPosition.y + popUpHeight);
                isPoppedUp = true;
                isMoving = true;
                rb.gravityScale = 1; // Enable gravity after popping up
            }
        }
    }

    protected virtual void FixedUpdate()
    {
        if (isMoving && CanMove)
        {
            rb.linearVelocity = new Vector2(moveSpeed, rb.linearVelocity.y);
        }
    }
}