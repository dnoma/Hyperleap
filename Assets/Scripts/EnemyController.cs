using Assets.PixelFantasy.PixelHeroes.Common.Scripts.ExampleScripts;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    internal CharacterAnimation _animation;
    private Rigidbody2D rb;
    public float moveSpeed = 2f;
    private bool movingRight = true;

    internal bool isDead = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _animation = GetComponentInChildren<CharacterAnimation>();

    }

    void Update()
    {
        if (!isDead)
        {
            // Move Goomba
            rb.linearVelocity = new Vector2(movingRight ? moveSpeed : -moveSpeed, rb.linearVelocity.y);
            _animation.Run();
            // Simple turn-around logic (replace with raycasting for edges later)
            if (transform.position.x > 5f) { movingRight = false; transform.GetChild(0).localScale = new Vector3(-1, 1, 1); }// Turn at x = 5
            if (transform.position.x < -5f) { movingRight = true; transform.GetChild(0).localScale = new Vector3(1, 1, 1); }// Turn at x = -5
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Check if Mario jumped on top
            if (collision.contacts[0].normal.y < -0.5f) // Mario from above
            {
                Debug.Log("Mario Hit Goomba ");

                GetComponent<HealthSystem>().OnDamageTaken();

                //Destroy(gameObject); // Goomba dies
            }
            else
            {
                // Mario hit from side - reset game (simplified)
                Debug.Log("Goomba  Hit Mario - Game Over!");
                collision.gameObject.GetComponent<HealthSystem>().OnDamageTaken();

            }
        }
    }
}