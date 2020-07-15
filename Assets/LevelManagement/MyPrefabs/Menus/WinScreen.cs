﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LvlManagement
{
    /**/
    public class WinScreen : Menu<WinScreen>
    {
        public void OnNextLevelPressed()
        {
            base.OnBackPressed();
            LevelLoader.LoadNextLevel();
        }
        public void OnRestartPressed()
        {
            base.OnBackPressed();
            LevelLoader.ReloadLevel();
        }
        public void OnMainMenuPressed()
        {
            LevelLoader.LoadMainMenuLevel();
            MainMenu.Open();
        }
    }
}
