using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SceneSwitcher : MonoBehaviour
{
    // Ім'я сцени, на яку потрібно переключитися
    public string sceneName = "Menu";

    // Ім'я об'єкта, який має компонент MainMenuNavigation
    public string mainMenuNavigationObjectName = "AudioManager";

    // Метод, який викликається при натисканні кнопки
    public void SwitchScene()
    {
        // Підписуємось на подію завантаження сцени
        SceneManager.sceneLoaded += OnSceneLoaded;
        // Починаємо завантаження сцени
        SceneManager.LoadScene(sceneName);
    }

    // Метод, який викликається після завантаження сцени
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == sceneName)
        {
            // Знаходимо об'єкт з MainMenuNavigation та викликаємо OpenOptions
            GameObject mainMenuNavigationObject = GameObject.Find(mainMenuNavigationObjectName);
            if (mainMenuNavigationObject != null)
            {
                MainMenuNavigation mainMenuNavigation = mainMenuNavigationObject.GetComponent<MainMenuNavigation>();
                if (mainMenuNavigation != null)
                {
                    Debug.Log("Викликається OpenOptions");
                    mainMenuNavigation.shopForLevel();
                }
                else
                {
                    Debug.Log("Компонент MainMenuNavigation не знайдено");
                }
            }
            else
            {
                Debug.Log("Об'єкт з ім'ям " + mainMenuNavigationObjectName + " не знайдено");
            }

            // Відписуємось від події після завершення дій
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
}
