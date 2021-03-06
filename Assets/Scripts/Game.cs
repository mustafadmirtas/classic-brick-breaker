﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
public class Game : MonoBehaviour
{
    public static Game instance;
    public Rigidbody2D rb;
    public GameObject stick,levelComp,soundManager;
    public Collision2D collision2;
    public int point;
    private int balltype;
    private int sticktype;
    private readonly int a;
    private Animator anim;
    float radius = 0.3f,speed = 4.8f;
    private Collider2D[] hitCol2D;
    public Sprite spr,spr2,tp_spr,spr_brick2,spr_brick1;
    public GameObject[] luck_spec;   
    Play play;
    SoundScript soundScript;
    private IEnumerator cr; 

    // Start is called before the first frame update
    void Start()
    {
      
            if (instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, 0);
        balltype = 0;
        sticktype = 0;    
        soundScript = soundManager.GetComponent<SoundScript>();
        play = new Play();
       




    }
    private void FixedUpdate()
    {
    
       
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        rb.velocity = new Vector2(rb.velocity.x + 0.1f,rb.velocity.y);
        SoundScript.instance.PlaySound(3);
        Collider2D collider = collision.collider;
        IsThisEnd();
        if (collision.gameObject.CompareTag("cubuk")) // if tag with cubuk
        {

            Vector3 contactPoint = collision.contacts[0].point;
            if (contactPoint.y > -3.2f) { 
            Vector3 center = collider.bounds.center;
            Vector3 right = new Vector3(contactPoint.x - center.x, 0);  
            // take contact point and find where it touch after that give some rotate like left and right
            rb.velocity = new Vector2(right.x * 5.5f, speed);


            }
        }
        if (balltype == 0)
        {
            /* if ball tag with tas(brick) take where the ball and destroy object add point
                look is it last one ItemCreate with ball location*/
            if (collision.gameObject.CompareTag("tas"))
            {
               
               Destroy(collision.gameObject);
               Vector2 vector2 = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);                
               IsThisEnd();
               ItemCreate(vector2);
                
            }
            if (collision.gameObject.CompareTag("inv_brick")) // if ball tag inv_brick brick will be visible and second tag brick destory
            {
                if(collision.gameObject.GetComponent<SpriteRenderer>().enabled == false)
                {
                    collision.gameObject.GetComponent<SpriteRenderer>().enabled = true;
                }
                else
                {
                    Vector2 vector2 = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
                    Destroy(collision.gameObject);
                    IsThisEnd();
                    ItemCreate(vector2);
                }
            }
            if (collision.gameObject.CompareTag("3rd_brick"))
            {
                collision.gameObject.transform.tag = "2rd_brick";
                collision.gameObject.GetComponent<SpriteRenderer>().sprite = spr_brick2;
            }
            else if (collision.gameObject.CompareTag("2rd_brick"))
            {
                collision.gameObject.tag = "1rd_brick";
                collision.gameObject.GetComponent<SpriteRenderer>().sprite = spr_brick1;
            }
            else if (collision.gameObject.CompareTag("1rd_brick"))
            {
                Vector2 vector2 = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
                Destroy(collision.gameObject);
                IsThisEnd();
                ItemCreate(vector2);
            }
        }
        if (balltype == 1) // that means ball have fire power and when tag with tas(brick) it explode and destroy near bricks to.
        {
            if (collision.gameObject.CompareTag("tas") || collision.gameObject.CompareTag("3rd_brick") || collision.gameObject.CompareTag("inv_brick") || 
                collision.gameObject.CompareTag("gold_brick") || collision.gameObject.CompareTag("2rd_brick") || collision.gameObject.CompareTag("1rd_brick")) {

                Vector2 vector2 = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
                Patla(collision.contacts[0].point);
                ItemCreate(vector2);
                IsThisEnd();
                
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // we use this when the ball has through skill when ball because take through  all brick are be trigger.
        if (collision.gameObject.CompareTag("tas"))          
        {
            Vector2 vector2 = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
            Destroy(collision.gameObject);
            ItemCreate(vector2);
            IsThisEnd();
        }
        if (collision.gameObject.CompareTag("inv_brick"))
        {
            Vector2 vector2 = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
            Destroy(collision.gameObject);
            ItemCreate(vector2);
            IsThisEnd();
        }
        if (collision.gameObject.CompareTag("3rd_brick"))
        {
            Vector2 vector2 = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
            Destroy(collision.gameObject);        
            ItemCreate(vector2);
            IsThisEnd();
        }
        if (collision.gameObject.CompareTag("2rd_brick"))
        {
            Vector2 vector2 = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
            Destroy(collision.gameObject);
            ItemCreate(vector2);
            IsThisEnd();
        }
        if (collision.gameObject.CompareTag("1rd_brick"))
        {
            Vector2 vector2 = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
            Destroy(collision.gameObject);
            IsThisEnd();
            ItemCreate(vector2);
        }
        if (collision.gameObject.CompareTag("gold_brick"))
        {
            Vector2 vector2 = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
            Destroy(collision.gameObject);
            IsThisEnd();
            ItemCreate(vector2);
        }

    }

    void Patla(Vector2 patlamaNoktası)            
    {
        /* when ball have fire skill we take contact point and this function destroy 
          brick with given radius where the touch ball to brick*/
        hitCol2D = Physics2D.OverlapCircleAll(patlamaNoktası, radius);

        foreach(Collider2D hitCol in hitCol2D)
        {
            if (hitCol.gameObject.CompareTag("tas") || hitCol.gameObject.CompareTag("inv_brick") || hitCol.gameObject.CompareTag("3rd_brick") || hitCol.gameObject.CompareTag("2rd_brick")
                || hitCol.gameObject.CompareTag("1rd_brick") || hitCol.gameObject.CompareTag("gold_brick"))
            {
                Destroy(hitCol.gameObject);
            }
        }
        IsThisEnd();
    }
    void ItemCreate(Vector2 vector2)
    {
        /* Item create is give skills to player, take random number if it is bigger than 85 gives one skill. It means %15 chance */
        int a = Random.Range(0, 100);
        if( a < 90) { 
        GameObject go2 = Instantiate(luck_spec[Random.Range(0,6)], vector2, Quaternion.identity);
        Rigidbody2D r2d = go2.GetComponent<Rigidbody2D>();
        r2d.bodyType = RigidbodyType2D.Dynamic;
        r2d.gravityScale = 0.2f;
        r2d.velocity = new Vector2(Random.Range(0, 1), 1);
        Destroy(go2, 6f);
        }
    }
    public void IsThisEnd()
    {
        if(SceneManager.GetActiveScene().buildIndex > 0) {
          
        // look scene find object with tag which is tas(brick) if there is no tas finish game
        GameObject[] taslar,taslar2,taslar3,taslar4,taslar5;
        taslar = GameObject.FindGameObjectsWithTag("tas");
        taslar2 = GameObject.FindGameObjectsWithTag("inv_brick");
        taslar3 = GameObject.FindGameObjectsWithTag("3rd_brick");
        taslar4 = GameObject.FindGameObjectsWithTag("2rd_brick");
        taslar5 = GameObject.FindGameObjectsWithTag("1rd_brick");
           
                taslar = taslar.Concat(taslar2).ToArray();
            if(taslar2 != null) 
                taslar = taslar.Concat(taslar3).ToArray();
            if (taslar3 != null)
                taslar = taslar.Concat(taslar4).ToArray();
            if (taslar4 != null)
                taslar = taslar.Concat(taslar5).ToArray();
            MuteAndPause.instance.brick_count.text = taslar.Length.ToString();
        if (taslar.Length == 0)
        {
            MuteAndPause.instance.levelComp.SetActive(true);
            int a = SceneManager.GetActiveScene().buildIndex;
            if(a%4 == 0)
            {
                    GP_AdManager.instance.Display_InsterstitialAD();
            }
            if(PlayerPrefs.GetInt("Comp_Levels") <= PlayerPrefs.GetInt("CurrentLevel"))
            {
                
                PlayerPrefs.SetInt("Comp_Levels", PlayerPrefs.GetInt("CurrentLevel") + 1);
                SQLCon.instance.UpdateLoad();
            }
                balltype = 0;
                Time.timeScale = 0;
            }
            
        }
    }
    public void SetBallType(int a)
    {        
        balltype = a;
    }
    void ChangeSpeed()
    {
        // return time seconds
        
        
    }
    IEnumerator Example() // every 5 second speed increases
    {
        while (true) { 
            if(speed < 7.1f) { 
        GameObject go = GameObject.FindGameObjectWithTag("ball");
        Rigidbody2D r2d = go.GetComponent<Rigidbody2D>();
        r2d.velocity = new Vector2(r2d.velocity.x, (r2d.velocity.y + 0.07f));
        
      
        
             speed = speed + 0.3f;
       
            }
            yield return new WaitForSeconds(4);
        }
    }

    public void ResetSpeed()
    {
        speed = 4.5f;
    }
    public void StartSpeedChange()
    {
        StartCoroutine(Example());
    }
    public void StopSpeedChange()
    {
        StopCoroutine(Example());
    }
}