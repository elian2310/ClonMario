using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoopaController : MonoBehaviour
{
    public int speed;
    private bool moveRight;

    public Animator animator;

    public Transform shellSpawn;
    public GameObject shell;

    private GameObject player;
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
            transform.Translate(2 * Time.deltaTime * speed, 0, 0);
        }
        else
        {
            transform.Translate(-2 * Time.deltaTime * speed, 0, 0);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("tube") || collision.gameObject.CompareTag("enemies"))
        {
            if (moveRight)
            {
                Flip();
            }
            else
            {
                Flip();
            }
        }
        if (collision.CompareTag("player"))
        {
            float yOffset = 0.5f;

            if (transform.position.y+yOffset < collision.transform.position.y)
            {
                player.GetComponent<Rigidbody2D>().velocity = Vector2.up * 7;
                Instantiate(shell, shellSpawn.position, shellSpawn.rotation);
                Destroy(gameObject);
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
    }
    private void Flip()
    {
        moveRight = !moveRight;
        Vector2 ls = gameObject.transform.localScale;
        ls.x *= -1;
        transform.localScale = ls;
    }
}
