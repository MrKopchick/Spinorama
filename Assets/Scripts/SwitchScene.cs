using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SceneSwitcher : MonoBehaviour
{
    // ��'� �����, �� ��� ������� �������������
    public string sceneName = "Menu";

    // ��'� ��'����, ���� �� ��������� MainMenuNavigation
    public string mainMenuNavigationObjectName = "AudioManager";

    // �����, ���� ����������� ��� ��������� ������
    public void SwitchScene()
    {
        // ϳ��������� �� ���� ������������ �����
        SceneManager.sceneLoaded += OnSceneLoaded;
        // �������� ������������ �����
        SceneManager.LoadScene(sceneName);
    }

    // �����, ���� ����������� ���� ������������ �����
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == sceneName)
        {
            // ��������� ��'��� � MainMenuNavigation �� ��������� OpenOptions
            GameObject mainMenuNavigationObject = GameObject.Find(mainMenuNavigationObjectName);
            if (mainMenuNavigationObject != null)
            {
                MainMenuNavigation mainMenuNavigation = mainMenuNavigationObject.GetComponent<MainMenuNavigation>();
                if (mainMenuNavigation != null)
                {
                    Debug.Log("����������� OpenOptions");
                    mainMenuNavigation.shopForLevel();
                }
                else
                {
                    Debug.Log("��������� MainMenuNavigation �� ��������");
                }
            }
            else
            {
                Debug.Log("��'��� � ��'�� " + mainMenuNavigationObjectName + " �� ��������");
            }

            // ³��������� �� ��䳿 ���� ���������� ��
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
}
