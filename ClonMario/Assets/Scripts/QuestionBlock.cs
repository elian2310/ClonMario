using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionBlock : MonoBehaviour
{
    public Transform objectSpawn;
    public GameObject block;
    public GameObject coin;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("player") && collision.contacts[0].normal.y > 0.5f)
        {
            if (coin != null)
            {
                var coinValue = 1;

                ScoreManager.instance.ChangeCoinScore(coinValue);

                Instantiate(coin, objectSpawn.position, objectSpawn.rotation);
            }

            Instantiate(block, objectSpawn.position, objectSpawn.rotation);
            
            Destroy(gameObject);
        }
    }
}