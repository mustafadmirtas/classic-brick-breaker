using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Game : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject gameObject,stick,levelComp;
    public Collision2D collision2;
    public static int point;
    int balltype,sticktype,a;
    public Text score_Text,zaman_Text;
    float radius = 0.4f;
    private Collider2D[] hitCol2D;
    public Sprite spr,spr2,tp_spr;
    public GameObject[] luck_spec;
    public Button button;
    AdManager ad;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, 0);
        point = int.Parse(score_Text.text);
        balltype = 0;
        sticktype = 0;
        button.onClick.AddListener(SonrakiBolum);
        ad = new AdManager();
    }
    private void FixedUpdate()
    {
        // write time every update time
        int saniye = zamanYaz();
        zaman_Text.text = saniye.ToString();
        isThisEnd();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D collider = collision.collider;
        if (collision.gameObject.CompareTag("cubuk")) // if tag with cubuk
        {

            Vector3 contactPoint = collision.contacts[0].point;
            Vector3 center = collider.bounds.center;
            Vector3 right = new Vector3(contactPoint.x - center.x, 0);  
            // take contact point and find where it touch after that give some rotate like left and right
            rb.velocity = new Vector2(right.x * 5.5f, 6f);
        }

        if (balltype == 0)
        {
            /* if ball tag with tas(brick) take where the ball and destroy object add point
                look is it last one ItemCreate with ball location*/
            if (collision.gameObject.CompareTag("tas"))
            {
               Vector2 vector2 = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
               Destroy(collision.gameObject);
               puanEkle(10);
               isThisEnd();
               ItemCreate(vector2);
               
            }
        }
        if (balltype == 1) // that means ball have fire power and when tag with tas(brick) it explode and destroy near bricks to.
        {
            if (collision.gameObject.CompareTag("tas"))
            {
                Vector2 vector2 = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
                patla(collision.contacts[0].point);
                ItemCreate(vector2);
                isThisEnd();
              
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
            puanEkle(10);
            isThisEnd();
            ItemCreate(vector2);
        }
    }
    public void puanEkle(int a) // add point 
    {
        point = point + a;    
        score_Text.text = point.ToString();
    }
    void patla(Vector2 patlamaNoktası)            
    {
        /* when ball have fire skill we take contact point and this function destroy 
          brick with given radius where the touch ball to brick*/
        hitCol2D = Physics2D.OverlapCircleAll(patlamaNoktası, radius);

        foreach(Collider2D hitCol in hitCol2D)
        {
            if (hitCol.gameObject.CompareTag("tas"))
            {
                Destroy(hitCol.gameObject);
                puanEkle(30);
            }
        }
    }
    void ItemCreate(Vector2 vector2)
    {
        /* Item create is give skills to player, take random number if it is bigger than 70 gives one skill. It means %30 chance */
        int a = Random.Range(0, 100);

        if( a > 70) { 
        GameObject go2 = Instantiate(luck_spec[Random.Range(0,6)], vector2, Quaternion.identity);
        Rigidbody2D r2d = go2.GetComponent<Rigidbody2D>();
        r2d.bodyType = RigidbodyType2D.Dynamic;
        r2d.gravityScale = 0.2f;
        r2d.velocity = new Vector2(Random.Range(0, 1), 1);
        Destroy(go2, 6f);
        }
    }
    int zamanYaz()
    {
        // return time seconds
        float time =Time.time;
        int saniye = (int) time;
        return saniye;
    }
    void isThisEnd()
    {
        // look scene find object with tag which is tas(brick) if there is no tas finish game
        GameObject[] taslar;
        taslar = GameObject.FindGameObjectsWithTag("tas");
        if(taslar.Length == 0)
        {
            Time.timeScale = 0;
            levelComp.SetActive(true);
            int a = SceneManager.GetActiveScene().buildIndex;
            if(a%4 == 0)
            {
                    ad.Video_Ads();
            }
            if(PlayerPrefs.GetInt("Comp_Level") < SceneManager.GetActiveScene().buildIndex)
            {
                PlayerPrefs.SetInt("Comp_Level", SceneManager.GetActiveScene().buildIndex + 1);
            }
            
        }
       
    }
    public void setBallType()
    {
        balltype = 1;
    }
    public void SonrakiBolum() // Go back scene which selecting levels
    {
        SceneManager.LoadScene(0);
        PlayerPrefs.SetInt("Level", 1);
    }

}