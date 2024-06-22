using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIInfo : MonoBehaviour
{
    public static UIInfo Instance;  
    [SerializeField]
    GameObject removeAdsBtn;

    private void Start()
    {
        Instance = this;
        UpdateRemoveAds();
    }
    public void UpdateRemoveAds()
    {
        bool removeAds = PlayerPrefs.GetInt("RemoveAds") == 1;
        removeAdsBtn.SetActive(!removeAds);
    }
}
