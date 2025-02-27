using Assets.PixelFantasy.PixelHeroes.Common.Scripts.ExampleScripts;
using DG.Tweening;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterAnimation _animation;

    private Rigidbody2D rb;
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    private bool isGrounded;
    public bool isInvincible;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _animation = GetComponentInChildren<CharacterAnimation>();
    }
    void Update()
    {
        // Horizontal Movement
        float moveInput = Input.GetAxisRaw("Horizontal"); // A/D or Left/Right Arrow
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        if(isGrounded)
        {
            if ((moveInput != 0))
            {
                if(moveInput > 0)
                {
                    transform.GetChild(0).localScale = new Vector3(1,1,1);
                }
                else
                {
                    transform.GetChild(0).localScale = new Vector3(-1, 1, 1);
                }

                _animation.Run();
            }
            else
                _animation.Idle();
        }
       

        // Jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isGrounded = false;

            _animation.Jump();

            SoundManager.instance.PlaySound(SoundManager.instance.playerJump);
        }

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if Mario lands on the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            _animation.Fall();
            isGrounded = true;
        }
        
        if (collision.gameObject.CompareTag("DeadZone"))
        {
           GetComponent<PlayerHealth>().OnDamageTaken();
        }

        if (collision.gameObject.CompareTag("Goomba") && isInvincible)
        {
            collision.gameObject.GetComponent<HealthSystem>().OnDamageTaken();
        }

        if (collision.gameObject.CompareTag("Mashroom"))
        {
            StartCoroutine(Mashroom());
            SoundManager.instance.PlaySound(SoundManager.instance.playerPowerUp);
            collision.gameObject.gameObject.SetActive(false);
        }
    }

   

    IEnumerator Mashroom()
    {
        isInvincible = true;

        var orignalScale = transform.localScale;

        transform.DOScale(orignalScale * 1.5f, .2f);

        yield return new WaitForSeconds(10f);

        transform.DOScale(orignalScale, .1f);

        isInvincible = false;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("WinZone"))
        {
            _animation.Slash();

            this.enabled = false;
            GameManager.instance.OnLevelComplete();
            SoundManager.instance.PlaySound(SoundManager.instance.levelComplete);
        }
    }
}