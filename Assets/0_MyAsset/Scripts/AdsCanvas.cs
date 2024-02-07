using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsCanvas : MonoBehaviour
{
    static AdsCanvas instance;
    public static AdsCanvas i
    {
        get
        {
            if (instance == null) instance = FindObjectOfType<AdsCanvas>() as AdsCanvas;
            return instance;
        }
    }
    [SerializeField] GameObject interstitialContainer;
    [SerializeField] bool showInterstitialOnStart = false;

    [Space(20)]
    [SerializeField] GameObject bannerContainer;
    [SerializeField] bool showBannerOnStart = false;

    //ーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーー
    void Start()
    {
        interstitialContainer.SetActive(showInterstitialOnStart);
        bannerContainer.SetActive(showBannerOnStart);
    }
    public void SetInterstitialActive(bool cond)
    {
        interstitialContainer.SetActive(cond);
    }

    public void SetBannerActive(bool cond)
    {
        bannerContainer.SetActive(cond);
    }
}
