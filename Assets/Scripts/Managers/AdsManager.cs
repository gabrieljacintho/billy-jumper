using GoogleMobileAds.Api;
using UnityEngine;

public class AdsManager : MonoBehaviour
{
    public static AdsManager instance;

    private InterstitialAd interstitial;

    private readonly string adUnitId = "ca-app-pub-6939957165401361/9659942022";

    private bool playSong = false;
    private int skipAds = 3;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Start()
    {
#if UNITY_ANDROID || UNITY_IOS
        MobileAds.Initialize(initStatus => { });
        RequestInterstitial();
#endif
    }

    private void RequestInterstitial()
    {
        interstitial = new InterstitialAd(adUnitId);
        AdRequest request = new AdRequest.Builder().Build();
        interstitial.LoadAd(request);
    }

    public void ShowAd()
    {
        if (interstitial.IsLoaded())
        {
            if (skipAds == 0)
            {
                interstitial.Show();
                skipAds = 3;
            }
            else skipAds--;
        }
    }

    private void OnAdOpening()
    {
        if (SongsManager.instance.isPlaying)
        {
            SongsManager.instance.audioSource.Stop();
            playSong = true;
        }
        Time.timeScale = 0;
    }

    private void OnAdClosed()
    {
        if (playSong)
        {
            SongsManager.instance.audioSource.Play();
            playSong = false;
        }
        Time.timeScale = 1;
        interstitial.Destroy();
        RequestInterstitial();
    }
}
