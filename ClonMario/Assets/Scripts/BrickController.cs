using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickController : MonoBehaviour
{
    public float bounceHeight = 0.5f;
    public float bounceSpeed = 4f;

    private Vector2 originalPosition;

    private bool canBounce = true;
    private ParticleSystem particle;
    private SpriteRenderer sr;
    private BoxCollider2D bc;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.localPosition;
        particle = GetComponentInChildren<ParticleSystem>();
        sr = GetComponent<SpriteRenderer>();
        bc = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("player") && collision.contacts[0].normal.y > 0.5f)
        {
            if (PlayerController.growUp)
            {
                StartCoroutine(Break());
            }
            else
            {
                if (canBounce)
                {
                    canBounce = false;
                    StartCoroutine(Bounce());
                }
            }
        }
    }
    private IEnumerator Bounce()
    {
        while (true)
        {
            transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y + bounceSpeed * Time.deltaTime);

            if(transform.localPosition.y >= originalPosition.y + bounceHeight)
            {
                break;
            }
            yield return null;
        }

        while (true)
        {
            transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y + bounceSpeed * Time.deltaTime);

            if (transform.localPosition.y >= originalPosition.y)
            {
                transform.localPosition = originalPosition;
                break;
            }
            yield return null;
        }

        canBounce = true;
    }
    
    private IEnumerator Break()
    {
        particle.Play();
        sr.enabled = false;
        bc.enabled = false;

        yield return new WaitForSeconds(particle.main.startLifetime.constantMax);
        Destroy(gameObject);
    }
}
