using System;
using UnityEngine;
using Unity.RemoteConfig;
using GoogleMobileAds.Api;
using UnityEngine.Advertisements;

public class Interstitial : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    private UIManager _uimanager;
    public struct userAttributes { }
    public struct appAttributes { }
    public bool admobSwitch = true;

    private InterstitialAd interstitial;
    string _adUnitId = "Interstitial_Android";
    private void Awake()
    {
        ConfigManager.FetchCompleted += setSwitch;
        ConfigManager.FetchConfigs<userAttributes, appAttributes>(new userAttributes(), new appAttributes());
    }
    private void Start()
    {
        _uimanager = FindObjectOfType<UIManager>();
        
        {
            RequestInterstitial();
            LoadAd();
        }
            
    }
    void setSwitch(ConfigResponse response)
    {
        admobSwitch = ConfigManager.appConfig.GetBool("admobswitch");
        if (admobSwitch)
        {
            
        }
        else
        {
            
        }
    }
    public void RequestInterstitial()
    {
        
            string adUnitId = "ca-app-pub-8921506867398125/1197849911";
            this.interstitial = new InterstitialAd(adUnitId);
            AdRequest request = new AdRequest.Builder().Build();
            this.interstitial.LoadAd(request);

            this.interstitial.OnAdLoaded += HandleOnAdLoaded;
            this.interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
            this.interstitial.OnAdOpening += HandleOnAdOpened;
            this.interstitial.OnAdClosed += HandleOnAdClosed;
        
            
    }
    public void HandleOnAdLoaded(object sender, EventArgs args)
    {

    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        RequestInterstitial();
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {

    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        RequestInterstitial();
    }


    public void LoadAd()
    {
        Advertisement.Load(_adUnitId, this);
        
    }  
    public void OnUnityAdsAdLoaded(string adUnitId)
    {

    }

    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        LoadAd();
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        LoadAd();
    }

    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState) { }
    public void ShowInterstitial()
    {
        if(ConfigManager.appConfig.GetBool("admobswitch"))
        {
            int value = PlayerPrefs.GetInt("paid", 0);
            if (value == 1)
            {
                Debug.Log("VIP USER");
            }
            else
            {
                if (this.interstitial.IsLoaded())
                {
                    this.interstitial.Show();
                }
            }
                
        }
        else
        {
            int value = PlayerPrefs.GetInt("paid", 0);
            if (value == 1)
            {
                Debug.Log("VIP USER");
            }
            else
            {
                Advertisement.Show(_adUnitId, this);
            }
                
        }

    }


    private void OnDestroy()
    {
        ConfigManager.FetchCompleted -= setSwitch;
        interstitial.Destroy();

    }

}
