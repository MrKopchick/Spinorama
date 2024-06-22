using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PointObject : MonoBehaviour
{
    public TMP_Text RewardText;
    public GameObject player;
    public GameObject Panel;
    public GameObject RotateObj;
    public float rotationSpeed = 30.0f;
    public int coinsForLevel;
    public ParticleSystem explosion;
    public TMP_Text text;
    public GameObject claimButton;

    private AudioSource audioSource;
    [SerializeField] private AudioClip BonusSound;
    private int currentLevelIndex;
    public bool IsRewarded = false;

    private Coins coinsCounter;
    private VibrateController vibrateController;
    private bank coinBank;
    private int currentReward;

    void Start()
    {
        // Cache references
        coinsCounter = FindObjectOfType<Coins>();
        audioSource = GetComponent<AudioSource>();
        vibrateController = GameObject.Find("vibrate manager").GetComponent<VibrateController>();
        coinBank = GameObject.Find("Bank").GetComponent<bank>();

        currentReward = coinsForLevel;
        RewardText.text = currentReward.ToString();
        currentLevelIndex = SceneManager.GetActiveScene().buildIndex;

        // Check if level is already completed
        if (PlayerPrefs.GetInt($"Sigma_{currentLevelIndex}_Completed", 0) == 1)
        {
            Destroy(GameObject.Find("Coins"));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != player) return;

        HandleVibration();
        PlayBonusSound();
        explosion.Play();
        player.SetActive(false);
        LevelPromosion.CompleteLevel(currentLevelIndex);
        int tapsControl = PlayerPrefs.GetInt("TapsControl", 0);
        if (tapsControl == 1)
        {
            GameObject.Find("TapsControl").SetActive(false);
        }

        if (PlayerPrefs.GetInt($"Sigma_{currentLevelIndex}_Completed", 0) != 1)
        {
            PlayerPrefs.SetInt($"Sigma_{currentLevelIndex}_Completed", 1);
            if (coinsCounter != null)
            {
                currentReward = coinsCounter.coinsCountForLevel;
                RewardText.text = currentReward.ToString();
            }
        }
        else
        {
            text.text = "Claimed";
            RewardText.text = string.Empty;
            claimButton.SetActive(false);
        }

        StartCoroutine(CoroutineTime());
    }

    private void HandleVibration()
    {
        if (vibrateController.isBoolTrue)
        {
            Taptic.Heavy();
        }
    }

    private void PlayBonusSound()
    {
        audioSource.clip = BonusSound;
        audioSource.Play();
    }

    public void ClaimX2(int money)
    {
        int addedCoins = coinsCounter.coinsCountForLevel; // The amount added in AddCoin()
        coinBank.AddCoin(addedCoins);

        currentReward += addedCoins;
        RewardText.text = currentReward.ToString();
        claimButton.SetActive(false);
    }

    private IEnumerator CoroutineTime()
    {
        yield return new WaitForSeconds(0.1f);
        Panel.SetActive(true);
        Time.timeScale = 0;
    }
}