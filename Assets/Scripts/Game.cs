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
    public int point;
    int balltype,sticktype,a;
    public Text score_Text,zaman_Text;
    float radius = 0.5f;
    private Collider2D[] hitCol2D;
    public Sprite spr,spr2,tp_spr,spr_brick2,spr_brick1;
    public GameObject[] luck_spec;
    public Button button_nextlevel;
    AdManager ad;
    public Text[] texts = new Text[10];

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, 0);
        point = int.Parse(score_Text.text);
        balltype = 0;
        sticktype = 0;
        button_nextlevel.onClick.AddListener(SonrakiBolum);
        ad = new AdManager();
        LangCheck();
      
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
                    puanEkle(10);
                    isThisEnd();
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
                Destroy(collision.gameObject);
            }
        }
        if (balltype == 1) // that means ball have fire power and when tag with tas(brick) it explode and destroy near bricks to.
        {
            if (collision.gameObject.CompareTag("tas") || collision.gameObject.CompareTag("3rd_brick") || collision.gameObject.CompareTag("inv_brick") || 
                collision.gameObject.CompareTag("gold_brick") || collision.gameObject.CompareTag("2rd_brick") || collision.gameObject.CompareTag("1rd_brick")) {

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
            if (hitCol.gameObject.CompareTag("tas") || hitCol.gameObject.CompareTag("inv_brick") || hitCol.gameObject.CompareTag("3rd_brick") || hitCol.gameObject.CompareTag("2rd_brick")
                || hitCol.gameObject.CompareTag("1rd_brick") || hitCol.gameObject.CompareTag("gold_brick"))
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
            if(PlayerPrefs.GetInt("Comp_Levels") <= SceneManager.GetActiveScene().buildIndex)
            {
                PlayerPrefs.SetInt("Comp_Levels", SceneManager.GetActiveScene().buildIndex + 1);
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
    void LangCheck()
    {
        if(PlayerPrefs.GetInt("Lang", 1)== 1)
        {

            texts[0].text = "Skor :";
            texts[1].text = "Tebrikler";
            texts[2].text = "Bölümü Tamamladınız";
            texts[3].text = "Sonraki Bölüme Geç";
            texts[4].text = "Oyun Bitti";
            texts[5].text = "Tekrar Deneyin";
            texts[6].text = "Skorunuz";
            texts[7].text = "Tekrar Dene";
            texts[8].text = "Reklam İle Devam Et";
            texts[9].text = "Çıkış";


        }
        if (PlayerPrefs.GetInt("Lang", 1) == 2)
        {


            texts[0].text = "Score :";
            texts[1].text = "Congrats!";
            texts[2].text = "Level Completed";
            texts[3].text = "Next Level";
            texts[4].text = "Game Over";
            texts[5].text = "Try Again";
            texts[6].text = "Your Score";
            texts[7].text = "Try Again";
            texts[8].text = "Continue with Ads";
            texts[9].text = "Exit";


        }
    }
}