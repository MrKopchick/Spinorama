using UnityEngine;
using UnityEngine.UI;

public class ScrollPositionSaver : MonoBehaviour
{
    public ScrollRect scrollRect;

    private void OnDisable()
    {
        SaveScrollPosition();
    }

    private void OnEnable()
    {
        LoadScrollPosition();
    }

    private void SaveScrollPosition()
    {
        PlayerPrefs.SetFloat("ScrollPosition", scrollRect.verticalNormalizedPosition);
        PlayerPrefs.Save();
    }

    private void LoadScrollPosition()
    {
        float savedScrollPosition = PlayerPrefs.GetFloat("ScrollPosition", 0f);
        scrollRect.verticalNormalizedPosition = savedScrollPosition;
    }
}
