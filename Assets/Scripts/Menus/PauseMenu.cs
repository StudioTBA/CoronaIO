using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Com.StudioTBD.CoronaIO.Menus
{
    public class PauseMenu : MonoBehaviour
    {
        #region Properties

        public GameObject pauseMenu;
        public GameObject HUD;
        public GameObject minimap;

        public static bool gameIsPaused = false;

        #endregion


        #region MonoBehaviour Callbacks

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

        #endregion


        #region Public Methods

        public void Resume()
        {
            pauseMenu.SetActive(false);

            if (HUD != null)
                HUD.SetActive(true);

            if (minimap != null)
                minimap.SetActive(true);

            Time.timeScale = 1.0f;
            gameIsPaused = false;
        }

        public void Pause()
        {
            pauseMenu.SetActive(true);

            if (HUD != null)
                HUD.SetActive(false);

            if (minimap != null)
                minimap.SetActive(false);

            Time.timeScale = 0.0f;
            gameIsPaused = true;
        }

        public void LoadMainMenu()
        {
            gameIsPaused = false;
            Time.timeScale = 1.0f;
            SceneManager.LoadScene(MenuManager.MAINMENU);
        }

        public void QuitGame()
        {
            MenuManager.QuitGame();
        }

        #endregion
    }
}
