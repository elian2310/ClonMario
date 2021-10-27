using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickController : MonoBehaviour
{
    public float bounceHeight = 0.5f;
    public float bounceSpeed = 4f;

    private Vector2 originalPosition;

    private bool canBounce = true;
    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("player"))
        {
            if (canBounce)
            {
                canBounce = false;
                StartCoroutine(Bounce());
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
}
