using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VibrateController : MonoBehaviour
{
    public Button myButton;
    public TMP_Text statusText;
    public bool isBoolTrue = true;

    void Start()
    {
        // Load saved state
        isBoolTrue = PlayerPrefs.GetInt("BoolState", 1) == 1;
        UpdateStatusText();

        if (myButton != null)
        {
            myButton.onClick.AddListener(ToggleVibration);
        }
    }

    void ToggleVibration()
    {
        isBoolTrue = !isBoolTrue;

        // Save state
        PlayerPrefs.SetInt("BoolState", isBoolTrue ? 1 : 0);
        PlayerPrefs.Save();

        // Update status text
        UpdateStatusText();

        if (isBoolTrue)
        {
            Taptic.Light();
        }
        else
        {
            Debug.Log("Vibration off");
        }

        Debug.Log("Vibration State: " + isBoolTrue);
    }

    void UpdateStatusText()
    {
        if (statusText != null)
        {
            statusText.text = "VIBRATION " + (isBoolTrue ? "ON" : "OFF");
        }
    }
}
