using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace EyeTest
{
    /**/
    public class LevelLoader : MonoBehaviour
    {
        private static int mainMenuIndex = 1;

        public static void LoadLevel(string levelName)
        {
            if (Application.CanStreamedLevelBeLoaded(levelName))
            {
                SceneManager.LoadScene(levelName);
            }
            else
            {
                Debug.LogWarning("GameManager LoadLevel() Error: invalid scene specified!");
            }
        }
        public static void ReloadLevel()
        {
            LoadLevel(SceneManager.GetActiveScene().name);
        }

        public static void LoadLevel(int levelIndex)
        {
            // if the index is valid...
            if (levelIndex >= 0 && levelIndex < SceneManager.sceneCountInBuildSettings)
            {
                // open the MainMenu if the index is the mainMenuIndex
                if (levelIndex == LevelLoader.mainMenuIndex)
                {
                    MainMenu.Open();
                }

                // load the scene by index
                SceneManager.LoadScene(levelIndex);
            }
            else
            {
                Debug.LogWarning("LEVELLOADER LoadLevel Error: invalid scene specified!");
            }
        }
        public static void LoadNextLevel()
        {
            int nextSceneIndex = (SceneManager.GetActiveScene().buildIndex + 1)
                % SceneManager.sceneCountInBuildSettings;
            nextSceneIndex = Mathf.Clamp(nextSceneIndex, mainMenuIndex, nextSceneIndex);
            LoadLevel(nextSceneIndex);

        }

        public static void LoadMainMenuLevel()
        {
            LoadLevel(mainMenuIndex);
        }

    }
}
