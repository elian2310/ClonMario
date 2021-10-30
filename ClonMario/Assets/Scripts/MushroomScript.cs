using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomScript : MonoBehaviour
{
    public float speed = 1.5f;
    public bool moveLeft;
    public bool born;
    private Animator animator;
    public BoxCollider2D bc;
    private Rigidbody2D rb;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        bc.enabled = false;
        born = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (born)
        {
            animator.SetBool("Born", born);
            speed = 0;
            StartCoroutine(Move());
            rb.bodyType = RigidbodyType2D.Kinematic;
        }
        if (moveLeft)
        {
            transform.Translate(-2 * Time.deltaTime * speed, 0, 0);
        }
        else
        {
            transform.Translate(2 * Time.deltaTime * speed, 0, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("tube"))
        {
            if (moveLeft)
            {
                moveLeft = false;
            }
            else
            {
                moveLeft = true;
            }
        }
        if (collision.gameObject.tag == "player")
        {
            Destroy(gameObject);
            PlayerController.growUp = true;
        }
    }
    private IEnumerator Move()
    {
        yield return new WaitForSeconds(1f);
        born = false;
        animator.SetBool("Born", born);
        bc.enabled = true;
        speed = 1.5f;
        rb.bodyType = RigidbodyType2D.Dynamic;
        StopCoroutine(Move());
    }
}
