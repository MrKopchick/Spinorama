using UnityEngine;
using TMPro;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;

    public TMP_Text text;

    private bool soundEnabled = true;

    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SoundManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = "SoundManager";
                    instance = obj.AddComponent<SoundManager>();
                    DontDestroyOnLoad(obj);
                }
            }
            return instance;
        }
    }

    public void ToggleSound()
    {
        soundEnabled = !soundEnabled;
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
        if (!soundEnabled)
        {
            text.text = "music off";
            AudioListener.pause = true;
        }
        // Увімкнення звуків
        else
        {
            text.text = "music on";
            AudioListener.pause = false;
        }
    }
}