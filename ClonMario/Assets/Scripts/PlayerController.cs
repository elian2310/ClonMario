using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public int speed = 5;
    private bool FacingRight = true;
    private float horizontalMove;
    public Animator animator;

    public int jumpPower = 40;
    private bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;

    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;

    public static bool death;
    private float countdown = 0.5f;
    public static bool isStarUp;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        death = false;
        isStarUp = false;
    }

    void FixedUpdate()
    {
        horizontalMove = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontalMove * speed, rb.velocity.y);
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
    }
    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
        animator.SetBool("IsGrounded", isGrounded);
        if (horizontalMove < 0.0f && FacingRight)
        {
            FlipPlayer();
        }
        if (horizontalMove > 0.0f && !FacingRight)
        {
            FlipPlayer();
        }
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
        if (Input.GetKey(KeyCode.Space) && isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * jumpPower;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }
        if (death)
        {
            speed = 0;
            jumpPower = 0;
            animator.SetTrigger("Death");
            countdown -= Time.deltaTime;
            if (countdown > 0f)
            {
                Invoke("Death", 0.5f);
            }
            if (transform.position.y < -30)
            {
                SceneManager.LoadScene("SampleScene");
            }
            
            
        }

        if(isStarUp)
        {
            animator.SetBool("IsStarUp", isStarUp);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("void"))
        {
            death = true;
        }
        if (collision.gameObject.CompareTag("brick"))
        {
            isJumping = false;
            rb.velocity = Vector2.down * 5;
        }
    }
    void FlipPlayer()
    {
        FacingRight = !FacingRight;
        Vector2 playerScale = gameObject.transform.localScale;
        playerScale.x *= -1;
        transform.localScale = playerScale;
    }
    void Jump()
    {
        rb.velocity = Vector2.up * jumpPower;
        isJumping = true;
        jumpTimeCounter = jumpTime;
    }
    void Death()
    {
        GetComponent<Collider2D>().isTrigger = true;
        rb.velocity = new Vector2(rb.velocity.x, 5f);
    }
}
