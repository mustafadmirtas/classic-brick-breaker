using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;
using System;

public class GameOver : MonoBehaviour
{
    public GameObject gameOver,ball,adManagers,image1,image2,soundManager;
    public Button tryagain_button,cont_ads_button,quit_button;
    public Text text,text2;
    int health;
    GP_AdManager gp;
    Game game;
    SoundScript sS;
    Play play;
    private InterstitialAd interstitialAD;
    private RewardBasedVideoAd rewardVideoAD;
    // Start is called before the first frame update
    void Start()
    {
      //  DontDestroyOnLoad(gameObject);
        string appId = "ca-app-pub-3940256099942544~3347511713le";
        MobileAds.Initialize(appId);
        tryagain_button.onClick.AddListener(TryAgain);
        cont_ads_button.onClick.AddListener(Cont_Ads);
        quit_button.onClick.AddListener(QuitGame);
        game = new Game();       
        sS = soundManager.GetComponent<SoundScript>();
        play = new Play();
        health = 2;
        InterstitialRequest();
        RewardedAdsRequest();
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
                    Display_InsterstitialAD();
                }
                else if(health == 1)
                {
                    gameOver.SetActive(true);
                    Time.timeScale = 0;
                    text.text = text.text +"  " + text2.text;
                    sS.PlaySound(0);
                    if(PlayerPrefs.GetInt("GameOverCount", 0)== 3)
                    {
                        Display_Reward_Video();
                    }
                    else {
                        PlayerPrefs.SetInt("GameOverCount", PlayerPrefs.GetInt("GameOverCount", 0) + 1);
                    }
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
       
            gameOver.SetActive(false);
            Time.timeScale = 1;
            play.CreateBall();
    }
    private void InterstitialRequest()
    {

        string ins_ID = "ca-app-pub-3940256099942544/1033173712";

        // Create a 320x50 banner at the top of the screen.
        interstitialAD = new InterstitialAd(ins_ID);

        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        interstitialAD.LoadAd(request);
    }
    private void RewardedAdsRequest()
    {

        string video_ID = "ca-app-pub-3940256099942544/5224354917";
        // Create a 320x50 banner at the top of the screen.
        rewardVideoAD = RewardBasedVideoAd.Instance;

        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        rewardVideoAD.LoadAd(request, video_ID);
    }

    public void Display_InsterstitialAD()
    {
        if (interstitialAD.IsLoaded())
        {
            interstitialAD.Show();
        }
    }
    public void Display_Reward_Video()
    {
        if (rewardVideoAD.IsLoaded())
        {
            rewardVideoAD.Show();
        }
    }


    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
    }

    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeavingApplication event received");
    }

    //

    public void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoLoaded event received");
    }

    public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardBasedVideoFailedToLoad event received with message: "
                             + args.Message);
    }

    public void HandleRewardBasedVideoOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoOpened event received");
    }

    public void HandleRewardBasedVideoStarted(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoStarted event received");
    }

    public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoClosed event received");
    }

    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        MonoBehaviour.print(
            "HandleRewardBasedVideoRewarded event received for "
                        + amount.ToString() + " " + type);
    }

    public void HandleRewardBasedVideoLeftApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoLeftApplication event received");
    }
}
