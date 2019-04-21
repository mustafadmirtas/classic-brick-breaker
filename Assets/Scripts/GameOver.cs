using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour
{
    public GameObject gameOver,ball,adManagers,image1,image2,soundManager;
    public Button tryagain_button,cont_ads_button,quit_button;
    public Text text,text2;
    int health;

    Game game;
    SoundScript sS;
    AdManager ad;
    Play play;
    // Start is called before the first frame update
    void Start()
    {
        tryagain_button.onClick.AddListener(TryAgain);
        cont_ads_button.onClick.AddListener(Cont_Ads);
        quit_button.onClick.AddListener(QuitGame);
        game = new Game();
        sS = soundManager.GetComponent<SoundScript>();
        ad = new AdManager();
        play = new Play();
        health = 2;
        

    }

    // Update is called once per frame
    void Update()
    {
       
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // if balls enter this collison if it isn't last ball destroy it. 
        // if it is last ball look health if player has health change ball position to start point.
        // if hasn't health finish game write point to the screen
        if (collision.gameObject.CompareTag("ball"))
        {
            
            GameObject[] balls;
            balls = GameObject.FindGameObjectsWithTag("ball");
            if(balls.Length == 1)
            {
                if(health > 1)
                {
                    health = health - 1;
                    play.CreateBall();
                    image2.SetActive(false);
                    sS.PlaySound(0);    
                }
                else if(health == 1)
                {
                    gameOver.SetActive(true);
                    Time.timeScale = 0;
                    text.text = text.text +"  " + text2.text;
                    sS.PlaySound(0);
                }
                
            }
            else
            {
                Destroy(collision.gameObject);
            }

        }
        
    }
    void TryAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void QuitGame()
    {
        SceneManager.LoadScene(0);
    }
    void Cont_Ads()
    {
        // if player use this button player will watch ads and game will gives 1 health
            ad.Rewarded_VideoAds();
            gameOver.SetActive(false);
            Time.timeScale = 1;
            play.CreateBall();
    }
}
