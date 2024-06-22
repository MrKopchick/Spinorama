using UnityEngine;

public class LevelCheck : MonoBehaviour
{

    private Transform gemoroy;

    private void Awake()
    {
        gemoroy = GameObject.Find("gemoroy").transform;
        for (int i = 0; i < gemoroy.childCount; i++)
        {
            GameObject Button = gemoroy.GetChild(i).gameObject;
            Button.SetActive(false);
        }

        int counter = 0;
        int _isActive = 1;

        while (_isActive == 1)
        {
            SetLevelButtonActive(counter + 1, true);
            counter++;
            _isActive = PlayerPrefs.GetInt("level" + counter.ToString());
        }
    }

    private void SetLevelButtonActive(int levelNum, bool state)
    {
        gemoroy.GetChild(levelNum - 1).gameObject.SetActive(state);
    }
}