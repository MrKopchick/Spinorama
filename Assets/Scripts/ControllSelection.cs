using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ControlSelection : MonoBehaviour
{
    public Button swipeControlButton;
    public Button tapsControlButton;
    public GameObject toggleControlButton;
    public GameObject TutorialGuide;
    public GameObject TutorialPanel;
    public GameObject Controllers;


    private const string ButtonStateKey = "ButtonState";

    private void Start()
    {

        if (PlayerPrefs.GetInt(ButtonStateKey, 0) == 1)
        {
            // Якщо кнопка вже була натиснута, деактивуємо об'єкт
            if (TutorialGuide != null)
            {
                TutorialGuide.SetActive(false);
                TutorialPanel.SetActive(false);
            }
            
        }

        if (swipeControlButton != null)
        {
            swipeControlButton.onClick.AddListener(SetSwipeControl);
            tapsControlButton.onClick.AddListener(SetTapsControl);
        }
        
        if (toggleControlButton != null)
        {
            UpdateToggleButtonLabel();
        }
    }

    private void SetSwipeControl()
    {
        PlayerPrefs.SetInt("SwipeControl", 1);
        PlayerPrefs.SetInt("TapsControl", 0);
        PlayerPrefs.SetInt(ButtonStateKey, 1);
        PlayerPrefs.Save();
        if(TutorialGuide != null)
        {
            TutorialGuide.SetActive(false);
            TutorialPanel.SetActive(false);
        }
        
        Debug.Log("Swipe control activeted");
        // Оновлення стану toggleControlButton
        if (toggleControlButton != null)
        {
            UpdateToggleButtonLabel();
        }
    }

    private void SetTapsControl()
    {
        PlayerPrefs.SetInt("SwipeControl", 0);
        PlayerPrefs.SetInt("TapsControl", 1);
        PlayerPrefs.SetInt(ButtonStateKey, 1);
        PlayerPrefs.Save();
        PlayerPrefs.Save();
        if (TutorialGuide != null)
        {
            Controllers.SetActive(true);
            TutorialGuide.SetActive(false);
            TutorialPanel.SetActive(false);
        }
        

        Debug.Log("Taps control activeted");

        // Оновлення стану toggleControlButton
        if (toggleControlButton != null)
        {
            UpdateToggleButtonLabel();
        }
    }

    public void ToggleControl()
    {
        int swipeControl = PlayerPrefs.GetInt("SwipeControl", 1);
        if (swipeControl == 1)
        {
            SetTapsControl();
        }
        else
        {
            SetSwipeControl();
        }
    }

    private void UpdateToggleButtonLabel()
    {
        int swipeControl = PlayerPrefs.GetInt("SwipeControl", 1);
        TMP_Text buttonText = toggleControlButton.GetComponentInChildren<TMP_Text>();

        if (buttonText != null)
        {
            if (swipeControl == 1)
            {
                buttonText.text = "Control: Swipe";
            }
            else
            {
                buttonText.text = "Control: Taps";
            }
        }
    }
}
