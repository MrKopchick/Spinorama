using UnityEngine;
using UnityEngine.Advertisements;

public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] string _androidGameId = "5630511";
    [SerializeField] string _iOSGameId = "5630510";
    [SerializeField] bool _testMode = true;
    private string _gameId;

    void Awake()
    {
        InitializeAds();
    }

    public void InitializeAds()
    {
#if UNITY_IOS
        _gameId = _iOSGameId;
#elif UNITY_ANDROID
        _gameId = _androidGameId;
#elif UNITY_EDITOR
        _gameId = _androidGameId; //Only for testing the functionality in the Editor
#endif

        // Перевірте стан removeAds перед ініціалізацією
        if (!PlayerPrefs.HasKey("RemoveAds") || PlayerPrefs.GetInt("RemoveAds") == 0)
        {
            if (!Advertisement.isInitialized && Advertisement.isSupported)
            {
                Advertisement.Initialize(_gameId, _testMode, this);
            }
        }
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }
}
