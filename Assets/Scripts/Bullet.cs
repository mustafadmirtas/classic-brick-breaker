using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    public Text score_Text;
    Game game;
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if the bullet tag brick objects destroy bullet and brick
        if(collision.CompareTag("tas")){
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        if (collision.CompareTag("gold_brick"))
        {
            
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("inv_brick"))
        {
            if (collision.gameObject.GetComponent<SpriteRenderer>().enabled == false)
            {
                collision.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            }
            else
            {
                Vector2 vector2 = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
                Destroy(collision.gameObject);
            }


        }
        if (collision.gameObject.CompareTag("3rd_brick"))
        {
           
          


        }
    }

}
