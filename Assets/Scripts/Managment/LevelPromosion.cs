using UnityEngine;

public class LevelPromosion : MonoBehaviour
{    
    public static void CompleteLevel(int level)
    {
        PlayerPrefs.SetInt("level" + level.ToString(), 1);
    }
}
