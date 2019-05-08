using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;
using UnityEngine.SceneManagement;

public class GP_AdManager : MonoBehaviour
{
    public static GP_AdManager instance;
    private BannerView bannerAD;
    private InterstitialAd interstitialAD;
    private RewardBasedVideoAd rewardVideoAD;
    public void Start()
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
        string appId = "";
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(appId);
        RequestBanner();
        rewardVideoAD = RewardBasedVideoAd.Instance;      
        // Called when an ad request has successfully loaded.
        rewardVideoAD.OnAdLoaded += HandleRewardBasedVideoLoaded;
        // Called when an ad request failed to load.
        rewardVideoAD.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
        // Called when an ad is shown.
        rewardVideoAD.OnAdOpening += HandleRewardBasedVideoOpened;
        // Called when the ad starts to play.
        rewardVideoAD.OnAdStarted += HandleRewardBasedVideoStarted;
        // Called when the user should be rewarded for watching a video.
        rewardVideoAD.OnAdRewarded += HandleRewardBasedVideoRewarded;
        // Called when the ad is closed.
        rewardVideoAD.OnAdClosed += HandleRewardBasedVideoClosed;
        // Called when the ad click caused the user to leave the application.
        rewardVideoAD.OnAdLeavingApplication += HandleRewardBasedVideoLeftApplication;
        Display_Banner();
        RequestInt();
        RequestReward();
    }

    private void RequestBanner()
    {
    
        string adUnitId = "";
        
        // Create a 280x40 banner at the top of the screen.
        bannerAD = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);
        AdRequest request = new AdRequest.Builder().Build();
        // Load the banner with the request.
        bannerAD.LoadAd(request);
    }
   
    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        Display_Banner();
    }
    public void Display_Banner()
    {
        bannerAD.Show();
    }
    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        RequestBanner();
    }
    private void InterstitialRequest()
    {

        string ins_ID = "";

        // Create a 320x50 banner at the top of the screen.
        interstitialAD = new InterstitialAd(ins_ID);

        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        interstitialAD.LoadAd(request);
    }
    private void RewardedAdsRequest()
    {

        string video_ID = "";
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
        else {
            print("NotLoaded");
            
        }
        
       
    }
    public bool Display_Reward_Video()
    {
        bool a;
        if (rewardVideoAD.IsLoaded())
        {
            a = true;
            rewardVideoAD.Show();
        }
        else {
            a = false;
            print("NotLoadedRWD");
        }
        return a;
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
        GameOver.instance.health = 1;
        GameOver.instance.gameOver.SetActive(false);
        Time.timeScale = 1;
        Play.instance.CreateBall();
    }

    public void HandleRewardBasedVideoLeftApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoLeftApplication event received");
    }
    public void RequestInt()
    {
        InterstitialRequest();
    }
    public void RequestReward()
    {
        RewardedAdsRequest();
    }
}

