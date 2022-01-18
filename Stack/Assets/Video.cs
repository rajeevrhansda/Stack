using System;
using UnityEngine;
using Unity.RemoteConfig;
using GoogleMobileAds.Api;
using UnityEngine.Advertisements;
using UnityEngine.UI;
public class Video : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    private UIManager _uimanager;
    private GameManager _gamemanager;

    [SerializeField] Button showVideo;
    [SerializeField] private Text videoText;
    public struct userAttributes { }
    public struct appAttributes { }
    public bool admobSwitch = true;

    private RewardedAd rewardedAd;
    string adUnitId = "ca-app-pub-8921506867398125/4945523231";
    string _adUnitId = "Rewarded_Android";
    private void Awake()
    {
        showVideo.interactable = false;
        videoText.text = "please wait ad not loaded";
        ConfigManager.FetchCompleted += setSwitch;
        ConfigManager.FetchConfigs<userAttributes, appAttributes>(new userAttributes(), new appAttributes());
    }
    private void Start()
    {
        _uimanager = FindObjectOfType<UIManager>();
        _gamemanager = FindObjectOfType<GameManager>();
        RequestRewardedAd();
        LoadAd();

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
    private void RequestRewardedAd()
    {
        this.rewardedAd = new RewardedAd(adUnitId);
        AdRequest request = new AdRequest.Builder().Build();
        this.rewardedAd.LoadAd(request);
        this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        this.rewardedAd.OnAdOpening += HandleRewardedAdOpening;
        this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;
    }

    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        showVideo.interactable = true;
        videoText.text = "Watch an ad to continue";        
    }
    public void HandleRewardedAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        videoText.text = "please wait ad not loaded";
        showVideo.interactable = false;

        RequestRewardedAd();
    }
    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        showVideo.interactable = false;
        videoText.text = "please wait ad not loaded";


    }
    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        videoText.text = "please wait ad not loaded";
        showVideo.interactable = false;

        RequestRewardedAd();
    }
    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        videoText.text = "please wait ad not loaded";
        showVideo.interactable = false;

        RequestRewardedAd();
    }
    public void HandleUserEarnedReward(object sender, Reward args)
    {
        _gamemanager.SwitchAllowedFalse();
        _uimanager.GameOverMenuFalse();
        _gamemanager.SwitchGAmeOver();
        RequestRewardedAd();
        showVideo.interactable = false;

    }
    public void LoadAd()
    {
        Advertisement.Load(_adUnitId, this);
    }
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        showVideo.interactable = true;
        videoText.text = "Watch an ad to continue";
    }
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            _gamemanager.SwitchAllowedFalse();
            _uimanager.GameOverMenuFalse();
            _gamemanager.SwitchGAmeOver();
            LoadAd();
            showVideo.interactable = false;

        }
    }
    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        videoText.text = "please wait ad not loaded";
        showVideo.interactable = false;

        LoadAd();
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        videoText.text = "please wait ad not loaded";
        showVideo.interactable = false;

        LoadAd();
    }

    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }
    public void ShowRewardedAd()
    {
        if (ConfigManager.appConfig.GetBool("admobswitch"))
        {
            if (this.rewardedAd.IsLoaded())
            {
                this.rewardedAd.Show();
            }
        }
        else
        {
            Advertisement.Show(_adUnitId, this);
        }
    }
    private void OnDestroy()
    {
        ConfigManager.FetchCompleted -= setSwitch;
    }
}
