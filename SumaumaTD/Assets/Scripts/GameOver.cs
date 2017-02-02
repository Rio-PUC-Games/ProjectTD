﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class GameOver : MonoBehaviour
    {
        public int MenuSceneIndex = 0;

        public void Retry()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void Menu()
        {
            SceneManager.LoadScene(MenuSceneIndex);
        }
    }
}
