using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class BannerAdExample : MonoBehaviour
{
    // Для цілей цього прикладу ці кнопки використовуються для тестування функціональності:
    [SerializeField] Button _showBannerButton;
    [SerializeField] Button _hideBannerButton;

    [SerializeField] BannerPosition _bannerPosition = BannerPosition.BOTTOM_CENTER;

    [SerializeField] string _androidAdUnitId = "Banner_Android";
    [SerializeField] string _iOSAdUnitId = "Banner_iOS";
    string _adUnitId = null; // Це залишиться null для непідтримуваних платформ.

    void Start()
    {
        // Отримати ID рекламного юніту для поточної платформи:
#if UNITY_IOS
        _adUnitId = _iOSAdUnitId;
#elif UNITY_ANDROID
        _adUnitId = _androidAdUnitId;
#endif

        // Вимкнути кнопку до тих пір, поки реклама не буде готова до показу:
        _showBannerButton.interactable = false;
        _hideBannerButton.interactable = false;

        // Встановити позицію банера:
        Advertisement.Banner.SetPosition(_bannerPosition);

        // Викликати метод LoadBanner() для завантаження банера автоматично:
        LoadBanner();
    }

    // Реалізація методу для завантаження банера:
    public void LoadBanner()
    {
        if (PlayerPrefs.HasKey("RemoveAds") && PlayerPrefs.GetInt("RemoveAds") == 1)
        {
            Debug.Log("Банерна реклама вимкнена.");
            return;
        }

        // Налаштувати опції для повідомлення SDK про події завантаження:
        BannerLoadOptions options = new BannerLoadOptions
        {
            loadCallback = OnBannerLoaded,
            errorCallback = OnBannerError
        };

        // Завантажити рекламний юніт з банерним контентом:
        Advertisement.Banner.Load(_adUnitId, options);
    }

    // Реалізація коду для виконання при спрацьовуванні події loadCallback:
    void OnBannerLoaded()
    {
        Debug.Log("Банер завантажено");

        // Налаштувати кнопку Show Banner для виклику методу ShowBannerAd() при натисканні:
        _showBannerButton.onClick.AddListener(ShowBannerAd);
        // Налаштувати кнопку Hide Banner для виклику методу HideBannerAd() при натисканні:
        _hideBannerButton.onClick.AddListener(HideBannerAd);

        // Увімкнути обидві кнопки:
        _showBannerButton.interactable = true;
        _hideBannerButton.interactable = true;
    }

    // Реалізація коду для виконання при спрацьовуванні події errorCallback:
    void OnBannerError(string message)
    {
        Debug.Log($"Помилка банера: {message}");
        // Додатково можна виконати код, наприклад, спробувати завантажити іншу рекламу.
    }

    // Реалізація методу для виклику при натисканні кнопки Show Banner:
    void ShowBannerAd()
    {
        if (PlayerPrefs.HasKey("RemoveAds") && PlayerPrefs.GetInt("RemoveAds") == 1)
        {
            Debug.Log("Банерна реклама вимкнена.");
            return;
        }

        // Налаштувати опції для повідомлення SDK про події показу:
        BannerOptions options = new BannerOptions
        {
            clickCallback = OnBannerClicked,
            hideCallback = OnBannerHidden,
            showCallback = OnBannerShown
        };

        // Показати завантажений рекламний юніт банера:
        Advertisement.Banner.Show(_adUnitId, options);
    }

    // Реалізація методу для виклику при натисканні кнопки Hide Banner:
    void HideBannerAd()
    {
        // Сховати банер:
        Advertisement.Banner.Hide();
    }

    void OnBannerClicked() { }
    void OnBannerShown() { }
    void OnBannerHidden() { }

    void OnDestroy()
    {
        _showBannerButton.onClick.RemoveAllListeners();
        _hideBannerButton.onClick.RemoveAllListeners();
    }
}
