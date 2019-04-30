using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class GP_AdManager : MonoBehaviour
{
    public static GP_AdManager instance;
    private BannerView bannerAD;

    public void Start()
    {       
        string appId = "ca-app-pub-3940256099942544~3347511713le";
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(appId);
        RequestBanner();
    }

    private void RequestBanner()
    {
    
        string adUnitId = "ca-app-pub-3940256099942544/6300978111";
        AdSize adSize = new AdSize(280, 40);
        // Create a 280x40 banner at the top of the screen.
        bannerAD = new BannerView(adUnitId, adSize, AdPosition.Top);
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
}

