using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenShell : MonoBehaviour
{
    private int speed;
    private bool moveRight;

    private bool shellAttack;

    GameObject player;

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
            transform.Translate(Time.deltaTime * speed, 0, 0);
        }
        else
        {
            transform.Translate(Time.deltaTime * -speed, 0, 0);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("player"))
        {
            if (shellAttack)
            {
                float yOffset = 0.5f;
                if (transform.position.y + yOffset < collision.transform.position.y)
                {
                    player.GetComponent<Rigidbody2D>().velocity = Vector2.up * 7;
                    speed = 0;
                    shellAttack = false;
                    gameObject.tag = "shell";
                }
                else
                {
                    if (PlayerController.growUp)
                    {
                        /*if (PlayerController.isFlowerUp)
                        {
                            PlayerController.isFlowerUp = false;
                        }*/
                        PlayerController.growUp = false;
                    }
                    else
                    {
                        PlayerController.death = true;
                    }
                }
            }
            else
            {
                float xOffset = 0.5f;
                if (transform.position.x + xOffset < collision.transform.position.x)
                {
                    speed = 5;
                    moveRight = false;
                    shellAttack = true;
                    gameObject.tag = "shellAttack";
                }
                else
                {
                    speed = 5;
                    moveRight = true;
                    shellAttack = true;
                    gameObject.tag = "shellAttack";
                }
            }
        }
        if (collision.gameObject.CompareTag("tube") )
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
        if (collision.gameObject.CompareTag("shellAttack"))
        {
            if (shellAttack)
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
            else
            {
                Vector2 goombaScale = gameObject.transform.localScale;
                goombaScale.y = -1;
                transform.localScale = goombaScale;
                speed = 0;
                GetComponent<Collider2D>().isTrigger = true;

                float countdown = 0.5f;
                countdown -= Time.deltaTime;
                if (countdown > 0)
                {
                    rb.velocity = new Vector2(1, 5);
                }
            }
        }
    }
}
