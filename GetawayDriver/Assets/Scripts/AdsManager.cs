using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdsManager : MonoBehaviour
{
    private string applicationID = "ca-app-pub-1261679683719068~8934912084";
    private string interstitialID = "ca-app-pub-1261679683719068/1926311290";
    private string testID = "ca-app-pub-3940256099942544/1033173712";

    public InterstitialAd interstitial;

    private void Awake()
    {
        MobileAds.Initialize(initStatus => { });
    }

    public void RequestInterstitial()
    {
        //change testID to interstitialID before PUBLISH
        InterstitialAd ad = new InterstitialAd(testID);

        interstitial = ad;

        // Called when an ad request has successfully loaded.
        interstitial.OnAdLoaded += HandleOnAdLoaded;
        // Called when an ad request failed to load.
        interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Called when an ad is shown.
        interstitial.OnAdOpening += HandleOnAdOpened;
        // Called when the ad is closed.
        interstitial.OnAdClosed += HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        interstitial.OnAdLeavingApplication += HandleOnAdLeavingApplication;

        AdRequest request = new AdRequest.Builder().Build();
        ad.LoadAd(request);
    }

    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
                            + args.Message);
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");

        GameManager.instance.ContinueGameOver();
    }

    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeavingApplication event received");
    }
}
