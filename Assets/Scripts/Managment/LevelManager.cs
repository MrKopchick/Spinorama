using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UIElements;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject[] levels;
    [SerializeField] private GameObject[] buttons;
    [SerializeField] private GameObject finishPanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private GameObject levelSelectionPanel;
    [SerializeField] private GameObject RemoveAdsPanel;
    [SerializeField] private GameObject chelHaroshPanel;
    private MainMenuNavigation mainMenu;
    private AudioSource audioSource;
    [SerializeField] private AudioClip clickSound;

    [SerializeField] private RectTransform RemoveAdsPanelRect, NicePanelRect, FinishPanelRect;
    [SerializeField] private float topPosY, middlePosY, bottomPosY;

    private RotateObject rotateObj;

    private void Start()
    {
        mainMenu = GameObject.FindAnyObjectByType<MainMenuNavigation>();
        audioSource = GetComponent<AudioSource>();
        rotateObj = GameObject.Find("RotateLevel").GetComponent<RotateObject>();
    }

    private void PlayClickSound()
    {
        audioSource.clip = clickSound;
        audioSource.Play();
        VibrateController vibrateController;
        vibrateController = GameObject.Find("vibrate manager").GetComponent<VibrateController>();
        if (vibrateController.isBoolTrue == true)
        {
            Taptic.Light();
        }
        else
        {
            Debug.Log("мінус вібрація");
        }
    }

    public void OnRemoveAdsClick()
    {
        PlayClickSound();
        if (PlayerPrefs.HasKey("RemoveAds") && PlayerPrefs.GetInt("RemoveAds") == 1)
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            int nextSceneIndex = (currentSceneIndex + 1) % SceneManager.sceneCountInBuildSettings;
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            RemoveAdsPanel.SetActive(true);
            FinishPanelRect.DOAnchorPosY(topPosY, 0.4f);
            RemoveAdsPanelRect.DOAnchorPosY(middlePosY, 0.4f);
        }
        
    }
    public void RemovedAdsClick()
    {
        chelHaroshPanel.SetActive(true);
        RemoveAdsPanelRect.DOAnchorPosY(topPosY, 0.4f);
        NicePanelRect.DOAnchorPosY(middlePosY, 0.4f);
    }

    public void OnNextSceneButtonClick()
    {
        PlayClickSound();
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = (currentSceneIndex + 1) % SceneManager.sceneCountInBuildSettings;
        SceneManager.LoadScene(nextSceneIndex);
    }

    public void ResetButton()
    {
        Taptic.Light();
        PlayClickSound();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoMenu()
    {
        Taptic.Light();
        PlayClickSound();
        SceneManager.LoadScene(0);
    }

    public void PauseButton()
    {
        Taptic.Light();
        PlayClickSound();
        pausePanel.SetActive(true);
        Time.timeScale = 0;
        rotateObj.IsPause = true;
        
    }

    public void GoShop()
    {
        StartCoroutine(LoadSceneAndActivateSettings());
    }
    public void ClosePanel()
    {
        Taptic.Light();
        PlayClickSound();
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        rotateObj.IsPause = false;
    }

    public void OpenShop()
    {
        Taptic.Light();
        PlayClickSound();
        shopPanel.SetActive(true);
        levelSelectionPanel.SetActive(false);
    }

    public void LeaveShop()
    {
        Taptic.Light();
        PlayClickSound();
        shopPanel.SetActive(false);
        levelSelectionPanel.SetActive(true);
    }

    private IEnumerator LoadSceneAndActivateSettings()
    {
        // Завантаження сцени асинхронно
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(0);
        // Запобігає автоматичному переходу на нову сцену після завантаження
        asyncLoad.allowSceneActivation = false;

        // Чекаємо, поки сцена буде завантажена
        while (!asyncLoad.isDone)
        {
            // Перевірка, чи сцена завантажена
            if (asyncLoad.progress >= 0.9f)
            {
                // Дозволяємо переключення сцени
                asyncLoad.allowSceneActivation = true;

                // Чекаємо один кадр, щоб сцена повністю завантажилась
                yield return null;

                // Затримка для ініціалізації сцени
                yield return new WaitForSeconds(0.1f);

                // Знаходимо об'єкт з MainMenuNavigation та викликаємо OpenOptions
                GameObject mainMenuNavigationObject = GameObject.Find("AudioManager");
                if (mainMenuNavigationObject != null)
                {
                    MainMenuNavigation mainMenuNavigation = mainMenuNavigationObject.GetComponent<MainMenuNavigation>();
                    if (mainMenuNavigation != null)
                    {
                        Debug.Log("Викликається OpenOptions");
                        mainMenuNavigation.OpenOptions();
                    }
                    else
                    {
                        Debug.Log("Компонент MainMenuNavigation не знайдено");
                    }
                }
                else
                {
                    Debug.Log("Об'єкт з ім'ям " + "AudioManager" + " не знайдено");
                }
            }
            yield return null;
        }
    }
}