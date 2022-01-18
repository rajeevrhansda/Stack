using UnityEngine;
using UnityEngine.Advertisements;

public class sdk : MonoBehaviour, IUnityAdsInitializationListener
{
    bool _testMode = false;
    bool _enablePerPlacementMode = true;
    private string _gameId = "4390849";

    void Awake()
    {
        InitializeAds();
    }

    public void InitializeAds()
    {
        Advertisement.Initialize(_gameId, _testMode, _enablePerPlacementMode, this);
    }

    public void OnInitializationComplete()
    {
        
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        
    }
}