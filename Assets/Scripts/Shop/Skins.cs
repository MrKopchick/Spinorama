using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class Skins : MonoBehaviour
{
    public SpriteRenderer sr;
    public List<Sprite> skins = new List<Sprite>();
    public List<int> prices = new List<int>();
    public GameObject playerSkin;
    public bank coinBank;

    [SerializeField] private AudioClip ButtonSound;
    private AudioSource audioSource;
    public TMP_Text priceText;

    private bool[] purchasedSkins;
    private int selectedSkin;
    private int equippedSkin;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        coinBank = GameObject.Find("Bank").GetComponent<bank>();
        InitializeSkinData();
        UpdateSkinDisplay();
    }

    private void InitializeSkinData()
    {
        int skinCount = skins.Count;
        purchasedSkins = new bool[skinCount];

        if (PlayerPrefs.GetInt("FirstTimeAccessingSkins", 1) == 1)
        {
            PlayerPrefs.SetInt("SkinPurchased_0", 1);
            PlayerPrefs.SetInt("EquippedSkinIndex", 0);
            PlayerPrefs.SetInt("FirstTimeAccessingSkins", 0);
        }

        for (int i = 0; i < skinCount; i++)
        {
            purchasedSkins[i] = PlayerPrefs.GetInt($"SkinPurchased_{i}", 0) == 1;
        }

        equippedSkin = PlayerPrefs.GetInt("EquippedSkinIndex", 0);
        selectedSkin = equippedSkin;
    }

    public void NextOption()
    {
        PlaySound(ButtonSound);
        VibrateSelection();
        selectedSkin = (selectedSkin + 1) % skins.Count;
        UpdateSkinDisplay();
    }

    public void BackOption()
    {
        PlaySound(ButtonSound);
        VibrateSelection();
        selectedSkin = (selectedSkin - 1 + skins.Count) % skins.Count;
        UpdateSkinDisplay();
    }

    public void Select()
    {
        PlaySound(ButtonSound);

        if (purchasedSkins[selectedSkin])
        {
            EquipSkin();
        }
        else if (coinBank.GetCount() >= prices[selectedSkin])
        {
            VibratePurchase();
            coinBank.TakeCoin(prices[selectedSkin]);
            purchasedSkins[selectedSkin] = true;
            PlayerPrefs.SetInt($"SkinPurchased_{selectedSkin}", 1);
            EquipSkin();
        }
        else
        {
            VibrateFailure();
            Debug.Log("Not enough coins");
        }
    }

    private void EquipSkin()
    {
        equippedSkin = selectedSkin;
        PlayerPrefs.SetInt("EquippedSkinIndex", equippedSkin);
        sr.sprite = skins[selectedSkin];
        playerSkin.GetComponent<SpriteRenderer>().sprite = sr.sprite;
        UpdatePriceText();
    }

    private void UpdateSkinDisplay()
    {
        sr.sprite = skins[selectedSkin];
        UpdatePriceText();
    }

    private void UpdatePriceText()
    {
        priceText.text = purchasedSkins[selectedSkin] ? (selectedSkin == equippedSkin ? "selected" : "purchased") : $"{prices[selectedSkin]}$";
    }

    private void PlaySound(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }

    private void VibrateSelection()
    {
        var vibrateController = GameObject.Find("vibrate manager").GetComponent<VibrateController>();
        if (vibrateController.isBoolTrue)
        {
            Taptic.Selection();
        }
        else
        {
            Debug.Log("Vibration disabled");
        }
    }

    private void VibratePurchase()
    {
        var vibrateController = GameObject.Find("vibrate manager").GetComponent<VibrateController>();
        if (vibrateController.isBoolTrue)
        {
            Taptic.Medium();
        }
        else
        {
            Debug.Log("Vibration disabled");
        }
    }

    private void VibrateFailure()
    {
        var vibrateController = GameObject.Find("vibrate manager").GetComponent<VibrateController>();
        if (vibrateController.isBoolTrue)
        {
            Taptic.Failure();
        }
        else
        {
            Debug.Log("Vibration disabled");
        }
    }
}
