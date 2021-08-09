using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour, IUnityAdsListener
{
#if UNITY_ANDROID
    private static readonly string storeID = "3920411";
#elif UNITY_IOS
    private static readonly string storeID = "3920410";
#endif

    private static readonly string videoID = "video";
    private static readonly string rewardedID = "rewardedVideo";
    private static readonly string bannerID = "MediationBanner";

    private Action adSuccess;
    private Action adSkipped;
    private Action adFailed;

#if UNITY_EDITOR
    private static bool testMode = false;
#else
    private static bool testMode = false;
#endif

    public static AdManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            Advertisement.AddListener(this);
            Advertisement.Initialize(storeID, testMode);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ShowBanner();
    }

    public static void ShowStandardAd()
    {
        if (Advertisement.IsReady(videoID))
            Advertisement.Show(videoID);
    }

    public static void ShowBanner()
    {
        instance.StartCoroutine(ShowBannerWhenReady());
    }

    public static void HideBanner()
    {
        Advertisement.Banner.Hide();
    }

    public static void ShowRewardedAd(Action success, Action skipped, Action failed)
    {
        instance.adSuccess = success;
        instance.adSkipped = skipped;
        instance.adFailed = failed;

        if (Advertisement.IsReady(rewardedID))
            Advertisement.Show(rewardedID);
    }

    private static IEnumerator ShowBannerWhenReady()
    {
        while (!Advertisement.IsReady(bannerID))
            yield return new WaitForSeconds(0.5f);

        Advertisement.Banner.SetPosition(BannerPosition.TOP_CENTER);
        Advertisement.Banner.Show(bannerID);
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (placementId == rewardedID)
        {
            switch (showResult)
            {
                case ShowResult.Finished:
                    adSuccess();
                    break;
                case ShowResult.Skipped:
                    adSkipped();
                    break;
                case ShowResult.Failed:
                    adFailed();
                    break;
            }
        }
    }

    public void OnUnityAdsDidError(string message) { }
    public void OnUnityAdsDidStart(string placementId) { }
    public void OnUnityAdsReady(string placementId) { }
}