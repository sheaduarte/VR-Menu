using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EyeTest
{
    /**/
    public class MainMenu : MonoBehaviour
    {
        public void OnEyeTestPressed()
        {
            LevelLoader.LoadLevel(1);
        }
    }
}
