using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public int speed = 5;
    private bool FacingRight = true;
    private float horizontalMove;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
        if (horizontalMove < 0.0f && FacingRight)
        {
            FlipPlayer();
        }
        if (horizontalMove > 0.0f && !FacingRight)
        {
            FlipPlayer();
        }
    }
    void FlipPlayer()
    {
        FacingRight = !FacingRight;
        Vector2 playerScale = gameObject.transform.localScale;
        playerScale.x *= -1;
        transform.localScale = playerScale;
    }
}
