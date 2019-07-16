using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Play : MonoBehaviour
{
    public static Play instance;
    public GameObject ball,weapon1,weapon2,weapon3,weapon4,bullet,soundManager,levelComp;
    public Rigidbody2D rigidbody,ball_rb;
    Vector2 vector = new Vector2(5, 0);
    readonly float maxSpeed = 10f;
    private readonly float width;
    private float height;
    private float timeLastShot;
    private float delayBetweenBullets = 0.5f;
    int stick_size,stick_type,i=0;
    Vector2 position;
    Game game;
    static bool firsttouch;
    public Sprite spr, spr2, tp_spr,normal_stick,normal_ball;
    SoundScript soundScript;
    public Button button_nextlevel, button_quit;
    GameObject go;
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
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
        }
        button_nextlevel.onClick.AddListener(NextLevel);
        button_quit.onClick.AddListener(Cikis);
        rigidbody = GetComponent<Rigidbody2D>();
        game = new Game();
        Time.timeScale = 1;
        firsttouch = false;
        stick_size = 0;
        stick_type = 0;
        soundScript = soundManager.GetComponent<SoundScript>();
        GameObject go = GameObject.FindGameObjectWithTag("ball");
        if (go == null)
        {
            Instantiate(ball, new Vector2(0f, -2.32f), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(SceneManager.GetActiveScene().buildIndex > 0) { 
        // if player have gun skills, call fire function when given delay pass
        if (stick_type > 0)
        {
            if ((Time.time > timeLastShot + delayBetweenBullets))
            {
                timeLastShot = Time.time;
                StartFire();
            }
        }
        // when player touch screen if it is first give ball velocity
        // after this touch every touch changes stick position to touch position x.
        if (Input.touchCount > 0)
        {

                Time.timeScale = 1;
                if (SceneManager.GetActiveScene().buildIndex > 0)
                {
                    GameObject go = GameObject.FindGameObjectWithTag("ball");
                    if (go == null)
                    {
                        Instantiate(ball, new Vector2(0f, -2.32f), Quaternion.identity);
                    }
                    if (firsttouch == false)
                    {

                        Rigidbody2D r2d = go.GetComponent<Rigidbody2D>();
                        Time.timeScale = 1;
                        r2d.velocity = new Vector2(0, 4.8f);
                        firsttouch = true;
                        Game.instance.ResetSpeed();
                        Game.instance.StartSpeedChange();
                    }
                    Touch touch = Input.GetTouch(0);
                    if (touch.phase == TouchPhase.Moved)
                    {
                        Vector2 pos = Camera.main.ScreenToWorldPoint(touch.position);
                        position = new Vector2(-pos.x, pos.y);
                        rigidbody.position = new Vector2(-position.x, -3.06f);
                    }
                }
         
           
        }
       float move = Input.GetAxis("Horizontal");

       rigidbody.velocity = new Vector2(move * maxSpeed, rigidbody.velocity.y);

        if (Input.GetMouseButton(0))
        {
            Time.timeScale = 1;
            if (SceneManager.GetActiveScene().buildIndex > 0)
            {
                    GameObject go = GameObject.FindGameObjectWithTag("ball");
                    if (go == null)
                    {
                        Instantiate(ball, new Vector2(0f, -2.32f), Quaternion.identity);
                    }
                    if (firsttouch == false)
                    {
                        
                        Rigidbody2D r2d = go.GetComponent<Rigidbody2D>();
                        Time.timeScale = 1;
                        r2d.velocity = new Vector2(0, 5f);
                        firsttouch = true;
                        Game.instance.ResetSpeed();
                        Game.instance.StartSpeedChange();
                    }
                    Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if (pos.y < 2.5f)
                {
                    position = new Vector2(-pos.x, pos.y);
                    rigidbody.position = new Vector2(-position.x, -3.06f);
                }
            }
        }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("big")) // if stick tag with big skill 
        {
            DoBig();
            Destroy(collision.gameObject);
            
        }
        if (collision.gameObject.CompareTag("fire")) // if stick tag with fire skill 
        {
            ChangeBall(tp_spr);
            Destroy(collision.gameObject);
            Game.instance.SetBallType(1);
        }
        if (collision.gameObject.CompareTag("gun")) // if stick tag with gun skill 
        {
            if(stick_type == 0)
            {  
                ChangeStick(spr);
                
                stick_type = 1;               
            }
            else if (stick_type == 1)
            {
                ChangeStick(spr2);
                
                stick_type = 2;
            }
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("small")) // if stick tag with small skill 
        {
            DoSmall();
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("triple")) // if stick tag with triple skill 
        {
            DoTriple();
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("through")) // if stick tag with through skill 
        {
            Through();
            Destroy(collision.gameObject);
        }
    }
    void DoBig()  // when call this function do stick size bigger
    {
        if (stick_size < 2)
        {
            gameObject.transform.localScale += new Vector3(0.2F, 0, 0);
            stick_size += 1;
        }
    }
    void DoSmall() // when call this function do stick size smaller
    {
        if (stick_size > -2)
        { 
            gameObject.transform.localScale += new Vector3(-0.1F, 0, 0);
            stick_size -= 1;
        }
    }
    void DoTriple() 
    {
        // take balls position and create new balls there. give them force 
        GameObject[] balls;
        balls = GameObject.FindGameObjectsWithTag("tas");
        if(balls.Length > 3)
         for(i=0; i < 3; i++)
            {
                /*Vector2 vector2 = new Vector2(balls[i].transform.position.x, balls[i].transform.position.y);
                GameObject go2 = Instantiate(balls[i], vector2, Quaternion.identity);
                Rigidbody2D r2d = go2.GetComponent<Rigidbody2D>();
                r2d.AddForce(new Vector2(3f, 20f));
                go2.tag = "ball";
                GameObject go3 = Instantiate(balls[i], vector2, Quaternion.identity);
                Rigidbody2D r3d = go3.GetComponent<Rigidbody2D>();
                go3.tag = "ball";
                r3d.AddForce(new Vector2(-3f, 20f));*/
                Destroy(balls[i]);
            }
        else
        {
            for (i = 0; i < balls.Length; i++)
            {
                Destroy(balls[i]);
            }
        }
        
    }
    void ChangeStick(Sprite spr)
    {     
        gameObject.GetComponent<SpriteRenderer>().sprite = spr;
    }
    void ChangeBall(Sprite tp_spr)
    {
        GameObject[] balls = GameObject.FindGameObjectsWithTag("ball");
        foreach (GameObject ball in balls)
        {
            ball.GetComponent<SpriteRenderer>().sprite = tp_spr; 
        }         
    }
    void StartFire()
    {
        if (stick_type == 1)
        {
            Vector2 vector2 = new Vector2(weapon1.transform.position.x, weapon1.transform.position.y);
            Vector2 vector3 = new Vector2(weapon2.transform.position.x, weapon2.transform.position.y);
            GameObject go2 = Instantiate(bullet, vector2, Quaternion.identity);
            GameObject go3 = Instantiate(bullet, vector3, Quaternion.identity);
            Rigidbody2D r2d = go2.GetComponent<Rigidbody2D>();
            Rigidbody2D r3d = go3.GetComponent<Rigidbody2D>();
            r2d.velocity = new Vector2(0, 4f);
            r3d.velocity = new Vector2(0, 4f);
            SoundScript.instance.PlaySound(2);
            Destroy(r2d, 5f);
            Destroy(r3d, 5f);

        }
        if (stick_type == 2)
        {
            Vector2 vector1 = new Vector2(weapon1.transform.position.x, weapon1.transform.position.y);
            Vector2 vector2 = new Vector2(weapon2.transform.position.x, weapon2.transform.position.y);
            Vector2 vector3 = new Vector2(weapon3.transform.position.x, weapon3.transform.position.y);
            Vector2 vector4 = new Vector2(weapon4.transform.position.x, weapon4.transform.position.y);
            GameObject go1 = Instantiate(bullet, vector1, Quaternion.identity);
            GameObject go2 = Instantiate(bullet, vector2, Quaternion.identity);
            GameObject go3 = Instantiate(bullet, vector3, Quaternion.identity);
            GameObject go4 = Instantiate(bullet, vector4, Quaternion.identity);
            Rigidbody2D r1d = go1.GetComponent<Rigidbody2D>();
            Rigidbody2D r2d = go2.GetComponent<Rigidbody2D>();
            Rigidbody2D r3d = go3.GetComponent<Rigidbody2D>();
            Rigidbody2D r4d = go4.GetComponent<Rigidbody2D>();
            r1d.velocity = new Vector2(0, 4f);
            r2d.velocity = new Vector2(0, 4f);
            r3d.velocity = new Vector2(0, 4f);
            r4d.velocity = new Vector2(0, 4f);
            SoundScript.instance.PlaySound(2);
            Destroy(r1d, 5f);
            Destroy(r2d, 5f);
            Destroy(r3d, 5f);
            Destroy(r4d, 5f);

        }
    }
    void Through()
    {
        GameObject[] bricks, inv_bricks, bricks2, bricks3,bricks4, bricks5;
        bricks = GameObject.FindGameObjectsWithTag("tas");
        inv_bricks = GameObject.FindGameObjectsWithTag("inv_brick");
        bricks2 = GameObject.FindGameObjectsWithTag("3rd_brick");
        bricks3 = GameObject.FindGameObjectsWithTag("2rd_brick");
        bricks4 = GameObject.FindGameObjectsWithTag("1rd_brick");
        bricks5 = GameObject.FindGameObjectsWithTag("gold_brick");
        GameObject[] all = bricks.Concat(inv_bricks).ToArray();
        all = bricks.Concat(bricks2).ToArray();
        all = bricks.Concat(bricks3).ToArray();
        all = bricks.Concat(bricks4).ToArray();
        all = bricks.Concat(bricks5).ToArray();
        for (int i = 0; i < all.Length; i++)
        {
            Collider2D col2 = all[i].GetComponent<BoxCollider2D>();
            col2.isTrigger = true;
        }
    }
    public void CreateBall()
    {
              GameObject go = GameObject.FindGameObjectWithTag("ball");
              go.transform.position = new Vector2(0f, -2.32f);
              Rigidbody2D r2d = go.GetComponent<Rigidbody2D>();
              r2d.velocity = new Vector2(0f, 0f);
              firsttouch = false;
              stick_type = 0;
    }
    public void NextLevel() // Go back scene which selecting levels
    {
        if(SceneManager.GetActiveScene().buildIndex < 40) {
        LevelEditor.instance.BringLevelStart(PlayerPrefs.GetInt("CurrentLevel") + 1);
        levelComp.SetActive(false);
        CreateBall();
        ResetBall();
        ResetStick();
        GameOver.instance.image2.SetActive(true);
        Game.instance.ResetSpeed();
   
        Time.timeScale = 1;
        }
        else {
            button_nextlevel.enabled = false;
            if(PlayerPrefs.GetInt("Lang", 1) == 1)
            {
                button_nextlevel.GetComponentInChildren<Text>().text = "Şimdilik Bitti :(";
            }
            if (PlayerPrefs.GetInt("Lang", 2) == 1)
            {
                button_nextlevel.GetComponentInChildren<Text>().text = "Levels finish for now :(";
            }
            button_nextlevel.GetComponentInChildren<Text>().color = Color.black;
        }
    }
    public void Cikis() // Go back scene which selecting levels
    {
        SceneManager.LoadScene(0);
        Lang.instance.gameObject.SetActive(true);
        MenuScript.instance.OpenLevels();
        Game.instance.StopSpeedChange();
        Time.timeScale = 0;
    }
    public void ResetBall()
    {
        GameObject ball = GameObject.FindGameObjectWithTag("ball");
        ball.GetComponent<SpriteRenderer>().sprite = normal_ball;
        Game.instance.SetBallType(0);
    }
    public void ResetStick()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = normal_stick;
        gameObject.transform.localScale = new Vector3(0.3974734f, 0.8222171f, 0.8222171f);
        stick_size = 0;
        stick_type = 0;
    }
}


