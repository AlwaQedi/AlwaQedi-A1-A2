using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(SpriteRenderer))]
public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer marioSprite;

    public float speed;
    public int jumpForce;
    public bool isGrounded;
    public LayerMask isGroundLayer;
    public Transform groundCheck;
    public float groundCheckRadius;
    private int jumpScore;
    public int score = 0;
    public int lives = 3;
    public bool isAlive = true;
    public GameObject heartImages;

    bool coroutineRunning = false;

    public AudioSource audioSource;
    public AudioClip hitSound, deathSound, pickupSound;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        marioSprite = GetComponent<SpriteRenderer>();

        if (speed <= 0)
        {
            speed = 5.0f;
        }

        if (jumpForce <= 0)
        {
            jumpForce = 300;
        }

        if (groundCheckRadius <= 0)
        {
            groundCheckRadius = 0.2f;
        }

        if (!groundCheck)
        {
            Debug.Log("Groundcheck does not exist, please assign a ground check object");
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive)
            return;

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);

        if (Input.GetButtonDown("Jump") && jumpScore < 1)
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * jumpForce);
            jumpScore++;
        }
        if (isGrounded)
        {
            jumpScore = 0;
        }

        Vector2 moveDirection = new Vector2(horizontalInput * speed, rb.velocity.y);
        rb.velocity = moveDirection;

        anim.SetFloat("speed", Mathf.Abs(horizontalInput));
        anim.SetBool("isGrounded", isGrounded);

        if (marioSprite.flipX && horizontalInput > 0 || !marioSprite.flipX && horizontalInput < 0)
            marioSprite.flipX = !marioSprite.flipX;

    }

    public void StartJumpForceChange()
    {
        if (!coroutineRunning)
        {
            StartCoroutine(JumpForceChange());
        }
        else
        {
            StopCoroutine(JumpForceChange());
            StartCoroutine(JumpForceChange());
        }
    }

    IEnumerator JumpForceChange()
    {
        coroutineRunning = true;
        jumpForce = 7000;
        yield return new WaitForSeconds(1.0f);
        jumpForce = 3500;
        coroutineRunning = false;
    }

    public void ApplyDamage()
    {
        lives--;
        audioSource.PlayOneShot(hitSound);
        SetHerts(lives);
        if (lives <= 0 && isAlive)
        {
            isAlive = false;
            audioSource.PlayOneShot(deathSound);
            anim.SetBool("Death", true);
            StartCoroutine(loadScene());
        }
    }

    IEnumerator loadScene()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(2);
    }

    public void SetHerts(int value)
    {
        if (value < 0 || value > 2)
            return;
        heartImages.transform.GetChild(value).gameObject.SetActive(false);
    }

    public void PickupSound()
    {
        audioSource.PlayOneShot(pickupSound);
    }
}
