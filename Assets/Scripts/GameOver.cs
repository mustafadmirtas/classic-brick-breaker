using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;
using System;

public class GameOver : MonoBehaviour
{
    public static GameOver instance;
    public GameObject gameOver,ball,adManagers,image1,image2,soundManager;
    public Button tryagain_button,cont_ads_button,quit_button;
    public Text text,text2;
    public int health;
    GP_AdManager gp;
    Game game;
    SoundScript sS;
    Play play;

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


        tryagain_button.onClick.AddListener(TryAgain);
        cont_ads_button.onClick.AddListener(Cont_Ads);
        quit_button.onClick.AddListener(QuitGame);
        game = new Game();       
        sS = soundManager.GetComponent<SoundScript>();
        play = new Play();
        health = 2;

        if(health == 0)
        {
            gameOver.SetActive(true);
        }
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
                    Game.instance.StopSpeedChange();
                }
                else if(health <= 1)
                {
                    
                    Time.timeScale = 0;
                    sS.PlaySound(0);
                    health -= 1;
                    cont_ads_button.enabled = true;
                    Game.instance.StopSpeedChange();
                    cont_ads_button.GetComponentInChildren<Text>().text = Lang.instance.text[14].text;
                    if (PlayerPrefs.GetInt("GameOverCount", 0)== 3)
                    {     
                        GP_AdManager.instance.Display_InsterstitialAD();
                        PlayerPrefs.SetInt("GameOverCount", 0);
                        
                    }
                    else {
                        PlayerPrefs.SetInt("GameOverCount", PlayerPrefs.GetInt("GameOverCount", 0) + 1);
                    }
                    gameOver.SetActive(true);
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
        gameOver.SetActive(false);
        Time.timeScale = 1;
        health = 2;
        Play.instance.ResetBall();
        Play.instance.ResetStick();
        Game.instance.ResetSpeed();
        image2.SetActive(true);
        play.CreateBall();
    }
    void QuitGame()
    {
        SceneManager.LoadScene(0);
        gameOver.SetActive(false);
        Destroy(Game.instance.gameObject);
        health = 2;
        Play.instance.ResetBall();
        Play.instance.ResetStick();
        Game.instance.ResetSpeed();
        MenuScript.instance.OpenLevels();
        Game.instance.StopSpeedChange();
        image2.SetActive(true);
        Lang.instance.gameObject.SetActive(true);
       
    }
    void Cont_Ads()
    {

            if(GP_AdManager.instance.Display_Reward_Video() == false)
            {
               cont_ads_button.GetComponentInChildren<Text>().text = "Ads Not Found";
               cont_ads_button.enabled = false;
               GP_AdManager.instance.RequestReward();
            }
            else
            { 
               if(health > 0)
                {
                    gameOver.SetActive(false);
                    Time.timeScale = 1;
                    play.CreateBall();
                    Game.instance.ResetSpeed();
                }
            }     
    }  
}
