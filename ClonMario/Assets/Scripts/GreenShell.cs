using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenShell : MonoBehaviour
{
    private int speed;
    private bool moveRight;

    private bool shellAttack;

    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
    }

    // Update is called once per frame
    void Update()
    {
        if (moveRight)
        {
            transform.Translate(Time.deltaTime * speed,0,0);
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
                }
                else
                {
                    /*if (PlayerController.growUp)
                {
                    if (PlayerController.isFlowerUp)
                    {
                        PlayerController.isFlowerUp = false;
                    }
                    PlayerController.growUp = false;
                }
                else
                {*/
                    PlayerController.death = true;
                    //}
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
                }
                else
                {
                    speed = 5;
                    moveRight = true;
                    shellAttack = true;
                }
            }         
        }
        if (collision.gameObject.CompareTag("tube"))
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
    }
}
