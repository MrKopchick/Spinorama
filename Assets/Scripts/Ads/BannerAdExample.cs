using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class BannerAdExample : MonoBehaviour
{
    // ��� ����� ����� �������� �� ������ ���������������� ��� ���������� ���������������:
    [SerializeField] Button _showBannerButton;
    [SerializeField] Button _hideBannerButton;

    [SerializeField] BannerPosition _bannerPosition = BannerPosition.BOTTOM_CENTER;

    [SerializeField] string _androidAdUnitId = "Banner_Android";
    [SerializeField] string _iOSAdUnitId = "Banner_iOS";
    string _adUnitId = null; // �� ���������� null ��� �������������� ��������.

    void Start()
    {
        // �������� ID ���������� ���� ��� ������� ���������:
#if UNITY_IOS
        _adUnitId = _iOSAdUnitId;
#elif UNITY_ANDROID
        _adUnitId = _androidAdUnitId;
#endif

        // �������� ������ �� ��� ��, ���� ������� �� ���� ������ �� ������:
        _showBannerButton.interactable = false;
        _hideBannerButton.interactable = false;

        // ���������� ������� ������:
        Advertisement.Banner.SetPosition(_bannerPosition);

        // ��������� ����� LoadBanner() ��� ������������ ������ �����������:
        LoadBanner();
    }

    // ��������� ������ ��� ������������ ������:
    public void LoadBanner()
    {
        if (PlayerPrefs.HasKey("RemoveAds") && PlayerPrefs.GetInt("RemoveAds") == 1)
        {
            Debug.Log("������� ������� ��������.");
            return;
        }

        // ����������� ����� ��� ����������� SDK ��� ��䳿 ������������:
        BannerLoadOptions options = new BannerLoadOptions
        {
            loadCallback = OnBannerLoaded,
            errorCallback = OnBannerError
        };

        // ����������� ��������� ��� � �������� ���������:
        Advertisement.Banner.Load(_adUnitId, options);
    }

    // ��������� ���� ��� ��������� ��� ������������� ��䳿 loadCallback:
    void OnBannerLoaded()
    {
        Debug.Log("����� �����������");

        // ����������� ������ Show Banner ��� ������� ������ ShowBannerAd() ��� ���������:
        _showBannerButton.onClick.AddListener(ShowBannerAd);
        // ����������� ������ Hide Banner ��� ������� ������ HideBannerAd() ��� ���������:
        _hideBannerButton.onClick.AddListener(HideBannerAd);

        // �������� ����� ������:
        _showBannerButton.interactable = true;
        _hideBannerButton.interactable = true;
    }

    // ��������� ���� ��� ��������� ��� ������������� ��䳿 errorCallback:
    void OnBannerError(string message)
    {
        Debug.Log($"������� ������: {message}");
        // ��������� ����� �������� ���, ���������, ���������� ����������� ���� �������.
    }

    // ��������� ������ ��� ������� ��� ��������� ������ Show Banner:
    void ShowBannerAd()
    {
        if (PlayerPrefs.HasKey("RemoveAds") && PlayerPrefs.GetInt("RemoveAds") == 1)
        {
            Debug.Log("������� ������� ��������.");
            return;
        }

        // ����������� ����� ��� ����������� SDK ��� ��䳿 ������:
        BannerOptions options = new BannerOptions
        {
            clickCallback = OnBannerClicked,
            hideCallback = OnBannerHidden,
            showCallback = OnBannerShown
        };

        // �������� ������������ ��������� ��� ������:
        Advertisement.Banner.Show(_adUnitId, options);
    }

    // ��������� ������ ��� ������� ��� ��������� ������ Hide Banner:
    void HideBannerAd()
    {
        // ������� �����:
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
