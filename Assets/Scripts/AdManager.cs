using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
public class AdManager : MonoBehaviour
{
    public static AdManager instance;
    private string store_id = "3092650";
    private string video_ad = "video";
    private string rewarded_video_ad = "rewardedVideo";
    // Start is called before the first frame update
    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    void Start()
    {
        Advertisement.Initialize(store_id,true);
       
    }

    // Update is called once per frame
    void Update()
    {


    }
    public void Video_Ads()
    {
        if (Advertisement.IsReady(video_ad))
        {
                Advertisement.Show(video_ad);
          
        }
    }
    public void Rewarded_VideoAds()
    {
        // when called this function show rewarded video and HandleShowResult
        if (Advertisement.IsReady(rewarded_video_ad))
        {
            var options = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show("rewardedVideo", options);
        }
        
    }
    private void HandleShowResult(ShowResult result)
    {
        
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("The ad was successfully shown.");        
                break;
            case ShowResult.Skipped:
                Debug.Log("The ad was skipped before reaching the end.");   
                break;
            case ShowResult.Failed:
                Debug.LogError("The ad failed to be shown.");
                break;
        }
       
    }
}
