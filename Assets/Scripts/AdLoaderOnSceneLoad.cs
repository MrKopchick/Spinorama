using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class AdLoaderOnSceneLoad : MonoBehaviour
{
    private RewardedAdsButton rewardedAdsButton;
    private BannerAdExample bannerAdExample;

    void Loop()
    {
        rewardedAdsButton = FindObjectOfType<RewardedAdsButton>();
        rewardedAdsButton.LoadAd();
    }
}
