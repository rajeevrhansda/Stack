using UnityEngine;
using UnityEngine.UI;
using Unity.RemoteConfig;
using System;
using UnityEngine;
using GoogleMobileAds.Api;

public class Manager : MonoBehaviour
{
    public struct userAttributes { }
    public struct appAttributes { }
    public GameObject admobBanner;
    public GameObject unityBanner;


    public bool admobSwitch = true;
    private void Awake()
    {
        admobBanner.SetActive(false);
        unityBanner.SetActive(false);
        ConfigManager.FetchCompleted += setSwitch;
        ConfigManager.FetchConfigs<userAttributes, appAttributes>(new userAttributes(), new appAttributes());
    }
    void setSwitch(ConfigResponse response)
    {
        admobSwitch = ConfigManager.appConfig.GetBool("admobswitch");
        if (admobSwitch)
        {
            admobBanner.SetActive(true);
            unityBanner.SetActive(false);
        }
        else
        {
            admobBanner.SetActive(false);
            unityBanner.SetActive(true);
        }
    }
    
    
    private void OnDestroy()
    {
        ConfigManager.FetchCompleted -= setSwitch;
    }
}
