using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Com.StudioTBD.CoronaIO.Menus
{
    public class PauseMenu : MonoBehaviour
    {
        public GameObject pauseMenu;
        public GameObject HUD;
        public GameObject minimap;

        public static bool gameIsPaused = false;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (gameIsPaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }

        public void Resume()
        {
            pauseMenu.SetActive(false);

            HUD.SetActive(true);
            minimap.SetActive(true);

            Time.timeScale = 1.0f;
            gameIsPaused = false;
        }

        public void Pause()
        {
            pauseMenu.SetActive(true);

            HUD.SetActive(false);
            minimap.SetActive(false);

            Time.timeScale = 0.0f;
            gameIsPaused = true;
        }

        public void LoadMainMenu()
        {
            SceneManager.LoadScene(0);
        }

        public void QuitGame()
        {
            if (Application.isEditor)
                UnityEditor.EditorApplication.isPlaying = false;
            else
                Application.Quit();
        }
    }
}
