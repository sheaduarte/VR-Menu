using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LvlManagement
{
    /**/
    public class LevelLoader : MonoBehaviour
    {
        private static int mainMenuIndex = 0;

        public void LoadLevel(string levelName)
        {
            SceneManager.LoadScene(levelName);
        }

        public static void LoadLevel(int levelIndex)
        {
            if (levelIndex >= 0 && levelIndex < SceneManager.sceneCountInBuildSettings)
            {
                if (levelIndex == mainMenuIndex)
                {
                    MainMenu.Open();
                }

                SceneManager.LoadScene(levelIndex);
            }
            else
            {
                Debug.LogWarning("invavlid scene specified");
            }
        }

        // private IEnumerator WinRoutine()
        // {
        //    TransitionFader.PlayTransition(_endTransitionPrefab);

        //    float fadeDelay  = (_endTransitionPrefab != null) ?
        //      _endTransitionPrefab.Delay + _endTransitionPrefab.FadeOnDuration : 0f;
        //   
        //   yield return new WaitForSeconds(fadeDelay);
        //   WinScreen.Open();
        //}

        public static void LoadNextLevel()
        {
            int nextSceneIndex = (SceneManager.GetActiveScene().buildIndex + 1) %
                SceneManager.sceneCountInBuildSettings;

            LoadLevel(nextSceneIndex);
        }
        // check for the end game condition on each frame


        public static void ReloadLevel()
        {
            LoadLevel(SceneManager.GetActiveScene().buildIndex);
        }

        public static void LoadMainMenuLevel()
        {
            LoadLevel(mainMenuIndex); 
        }
    }
}
