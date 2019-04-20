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
    }

}
