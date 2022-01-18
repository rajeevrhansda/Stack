using System;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdmobBanner : MonoBehaviour
{
    UIManager _uimanager;
    private BannerView bannerView;
    public void Start()
    {
        _uimanager = FindObjectOfType<UIManager>();
        MobileAds.Initialize(initStatus => { });
        this.RequestBanner();
    }
    private void RequestBanner()
    { 
        
            string adUnitId = "ca-app-pub-8921506867398125/7640324558";

            this.bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);
            AdRequest request = new AdRequest.Builder().Build();
        int value = PlayerPrefs.GetInt("paid", 0);
        if (value == 1)
        {
            Debug.Log("VIP USER");
        }
        else
        {

            this.bannerView.LoadAd(request);
        }

            this.bannerView.OnAdLoaded += this.HandleOnAdLoaded;
            this.bannerView.OnAdFailedToLoad += this.HandleOnAdFailedToLoad;
            this.bannerView.OnAdOpening += this.HandleOnAdOpened;
        
        
        

    }

    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
    }
    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print("HandleFailedToReceiveAd event received with message: " + args.LoadAdError);
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
    private void OnDestroy()
    {
        bannerView.Destroy();
    }
}