using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
    public static int currlevel;
    public static int UnlockedLevels;

    private AudioSource audioSource;
    [SerializeField] private AudioClip clickSound;

    public void OnClickLevel(int levelNum)
    {
        StartCoroutine(clickOnLevel(levelNum));

    }

    public void OnClickBack()
    {
        VibrateController vibrateController;
        vibrateController = GameObject.Find("vibrate manager").GetComponent<VibrateController>();
        if (vibrateController.isBoolTrue == true)
        {
            Taptic.Medium();
        }
        else
        {
            Debug.Log("мінус вібрація");
        }

        this.gameObject.SetActive(false);
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        UnlockedLevels = PlayerPrefs.GetInt("UnlockedLevels", 0);
    }

    public void LevelsDestroy()
    {
        VibrateController vibrateController;
        vibrateController = GameObject.Find("vibrate manager").GetComponent<VibrateController>();
        if (vibrateController.isBoolTrue == true)
        {
            Taptic.Medium();
        }
        else
        {
            Debug.Log("мінус вібрація");
        }
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(0);
    }

    IEnumerator clickOnLevel(int levelNum)
    {
        audioSource.clip = clickSound;
        audioSource.Play();
        VibrateController vibrateController;
        vibrateController = GameObject.Find("vibrate manager").GetComponent<VibrateController>();
        if (vibrateController.isBoolTrue == true)
        {
            Taptic.Medium();
        }
        else
        {
            Debug.Log("мінус вібрація");
        }
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene(levelNum);
    }
}