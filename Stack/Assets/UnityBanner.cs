using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class UnityBanner : MonoBehaviour
{
    [SerializeField] BannerPosition _bannerPosition = BannerPosition.BOTTOM_CENTER;
    
    string _adUnitId = "Banner_Android";

    void Start()
    {
        Advertisement.Banner.SetPosition(_bannerPosition);
        LoadBanner();
    }
    public void LoadBanner()
    {
        BannerLoadOptions options = new BannerLoadOptions
            {
                loadCallback = OnBannerLoaded,
                errorCallback = OnBannerError
            };
            Advertisement.Banner.Load(_adUnitId, options);
        
        
    }

    void OnBannerLoaded()
    {
        ShowBannerAd();
    }

    void OnBannerError(string message)
    {

    }
    void ShowBannerAd()
    {
        int value = PlayerPrefs.GetInt("paid", 0);
        if (value == 1)
        {
            Debug.Log("VIP USER");
        }
        else
        {

            BannerOptions options = new BannerOptions
            {
                clickCallback = OnBannerClicked,
                hideCallback = OnBannerHidden,
                showCallback = OnBannerShown
            };
            Advertisement.Banner.Show(_adUnitId, options);
        }
    }
    void OnBannerClicked() { }
    void OnBannerShown() { }
    void OnBannerHidden() { }

    void OnDestroy()
    {

    }
}
