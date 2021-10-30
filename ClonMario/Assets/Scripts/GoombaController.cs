using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoombaController : MonoBehaviour
{
    public int speed;
    private bool moveRight;

    private bool isCrushed;
    public Animator animator;

    GameObject player;
    public GameObject goombaCollider;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (moveRight)
        {
            transform.Translate(2 * Time.deltaTime * speed, 0, 0);
        }
        else
        {
            transform.Translate(-2 * Time.deltaTime * speed, 0, 0);
        }
        if (transform.position.y < -30)
        {
            Death();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("tube") || collision.gameObject.CompareTag("enemies") || collision.CompareTag("shell"))
        {
            if (moveRight)
            {
                moveRight = false;
            }
            else
            {
                moveRight = true;
            }
        }
        if (collision.gameObject.CompareTag("player"))
        {
            
            float yOffset = 0.5f;
            if (transform.position.y + yOffset < collision.transform.position.y && !isCrushed)
            {
                player.GetComponent<Rigidbody2D>().velocity = Vector2.up * 7;
                isCrushed = true;
                animator.SetBool("IsCrushed", isCrushed);
                speed = 0;
                goombaCollider.GetComponent<CircleCollider2D>().isTrigger = false;
                Invoke("Death", 0.5f);
            }
            else
            {
                PlayerController.death = true;
            }
        }

        if (collision.gameObject.CompareTag("shellAttack"))
        {
            Vector2 goombaScale = gameObject.transform.localScale;
            goombaScale.y = -1;
            transform.localScale = goombaScale;
            speed = 0;
            animator.SetBool("FireBallDeath", true);
            GetComponent<Collider2D>().isTrigger = true;

            float countdown = 0.5f;
            countdown -= Time.deltaTime;
            if (countdown > 0)
            {
                rb.velocity = new Vector2(1, 5);
            }
        }
        if (collision.CompareTag("void"))
        {
            Death();
        }
    }
    private void Death()
    {
        Destroy(gameObject);
    }
}
