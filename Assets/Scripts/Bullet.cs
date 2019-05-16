using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    public Text score_Text;
    Game game;
    public Sprite spr_brick2, spr_brick1;
    // Start is called before the first frame update
    void Start()
    {
        game = new Game();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Game.instance.IsThisEnd();
        // if the bullet tag brick objects destroy bullet and brick
        if (collision.CompareTag("tas")){
            Destroy(collision.gameObject);
            Destroy(gameObject);
            Game.instance.IsThisEnd();
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
                Destroy(gameObject);
                
            }
            else
            {
                Vector2 vector2 = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
                Destroy(collision.gameObject);
                Game.instance.IsThisEnd();

            }
        }

        if (collision.gameObject.CompareTag("3rd_brick"))
        {
            collision.gameObject.transform.tag = "2rd_brick";
            collision.gameObject.GetComponent<SpriteRenderer>().sprite = spr_brick2;
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("2rd_brick"))
        {
            collision.gameObject.tag = "1rd_brick";
            collision.gameObject.GetComponent<SpriteRenderer>().sprite = spr_brick1;
            Destroy(gameObject);
        } 
        else if (collision.gameObject.CompareTag("1rd_brick"))
        {
            Destroy(collision.gameObject);
            Game.instance.IsThisEnd();
            Destroy(gameObject);
        }      
    }
    
}
