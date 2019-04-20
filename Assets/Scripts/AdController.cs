using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
public class AdController : MonoBehaviour
{
    public static AdController instance;
    public string bannerPlacement = "banner";
    public bool testMode = true;
    private string gameID = "3092650";
    // Start is called before the first frame update
    private void Awake()
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


    void Start()
    {
        Advertisement.Initialize(gameID, testMode);
        StartCoroutine(ShowBannerWhenReady());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator ShowBannerWhenReady()
    {
        // take banner while ready and show
        while (!Advertisement.IsReady("banner"))
        {
            yield return new WaitForSeconds(0.5f);
        }
        Advertisement.Banner.SetPosition(BannerPosition.TOP_CENTER);
        Advertisement.Banner.Show(bannerPlacement);
    }
}
