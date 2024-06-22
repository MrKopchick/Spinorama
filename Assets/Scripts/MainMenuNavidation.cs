using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;

public class MainMenuNavigation : MonoBehaviour
{
    [SerializeField] private GameObject MainMenu;
    [SerializeField] private GameObject LevelSelection;
    [SerializeField] private GameObject Shop;
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject Options;
    [SerializeField] private GameObject DonateMenu;
    [SerializeField] private RectTransform levellSelectionPanelRect, MainMenuPanelRect, ShopPanelRect, OptionsPanelRect;
    [SerializeField] private float topPosY, middlePosY, bottomPosY;
    [SerializeField] private float tweenDuration;
    [SerializeField] private AudioClip ButtonSound;
    [SerializeField] private AudioClip MainButtonSound;

    private AudioSource audioSource;
    private VibrateController vibrateController;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        vibrateController = GameObject.Find("vibrate manager").GetComponent<VibrateController>();
    }

    private void PlaySound(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }

    private void Vibrate(bool isMedium)
    {
        if (vibrateController.isBoolTrue)
        {
            if (isMedium)
            {
                Taptic.Medium();
            }
            else
            {
                Taptic.Light();
            }
        }
        else
        {
            Debug.Log("Vibration disabled");
        }
    }

    public void Back()
    {
        Vibrate(false);
        ClosePanel(levellSelectionPanelRect);
        ClosePanel(ShopPanelRect);
        ClosePanel(OptionsPanelRect);
        PlaySound(ButtonSound);
        Player.SetActive(false);
    }

    public void StartToPlay()
    {
        Vibrate(true);
        PlaySound(MainButtonSound);
        LevelSelection.SetActive(true);
        OpenPanel(levellSelectionPanelRect);
    }

    private void OpenPanel(RectTransform rectObject)
    {
        rectObject.DOAnchorPosY(middlePosY, tweenDuration);
        MainMenuPanelRect.DOAnchorPosY(bottomPosY, tweenDuration);
    }

    private void ClosePanel(RectTransform rectObject)
    {
        rectObject.DOAnchorPosY(topPosY, tweenDuration);
        MainMenuPanelRect.DOAnchorPosY(0, tweenDuration);
    }

    public void shop()
    {
        ShopPanelRect.DOAnchorPosY(0, tweenDuration);
        MainMenuPanelRect.DOAnchorPosY(bottomPosY, tweenDuration);
        Vibrate(true);
        PlaySound(MainButtonSound);
        Player.SetActive(true);
        Shop.SetActive(true);
    }

    public void shopForLevel()
    {
        ShopPanelRect.DOAnchorPosY(0, tweenDuration);
        MainMenuPanelRect.DOAnchorPosY(bottomPosY, tweenDuration);
        Player.SetActive(true);
        Shop.SetActive(true);

    }

    public void CloseOptions()
    {
        Vibrate(false);
        PlaySound(ButtonSound);
        ClosePanel(OptionsPanelRect);
    }

    public void OpenOptions()
    {
        Vibrate(true);
        PlaySound(MainButtonSound);
        Options.SetActive(true);
        OptionsPanelRect.DOAnchorPosY(0, tweenDuration);
        MainMenuPanelRect.DOAnchorPosY(bottomPosY, tweenDuration);
    }
    public void OpenDonateMenu()
    {
        DonateMenu.SetActive(true);
    }
    public void CloseDonateMenu()
    {
        DonateMenu.SetActive(false);
    }
}
