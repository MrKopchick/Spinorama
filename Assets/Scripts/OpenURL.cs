using UnityEngine;

public class OpenURL : MonoBehaviour
{
    // URL-адреса для Instagram и Telegram
    public string instagramURL = "https://www.instagram.com/pixel_dreams010";
    public string telegramURL = "https://t.me/pixeldreams1";
    public string xURL = "https://x.com/Pixel_Dreamss";
    private bank bank;

    void Start()
    { 
        bank = FindObjectOfType<bank>();
    }
    // Метод для открытия Instagram
    public void OpenInstagram()
    {
        Application.OpenURL(instagramURL);
        if(PlayerPrefs.GetInt("OpenInstagram", 0) == 0 )
        {
            PlayerPrefs.SetInt("OpenInstagram", 1);
            bank.AddCoin(30);
        }
        else
        {
            Debug.Log("InstagramHasAlreadyOpen");
        }
        
    }

    // Метод для открытия Telegram
    public void OpenTelegram()
    {
        Application.OpenURL(telegramURL);
        if (PlayerPrefs.GetInt("OpenTelegram", 0) == 0)
        {
            PlayerPrefs.SetInt("OpenTelegram", 1);
            bank.AddCoin(30);
        }
        else
        {
            Debug.Log("TelegramHasAlreadyOpen");
        }
    }
    public void OpenX()
    {
        if (PlayerPrefs.GetInt("OpenX", 0) == 0)
        {
            PlayerPrefs.SetInt("OpenX", 1);
            bank.AddCoin(30);
        }
        else
        {
            Debug.Log("XHasAlreadyOpen");
        }
        Application.OpenURL(xURL);
    }
}