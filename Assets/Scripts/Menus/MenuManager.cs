using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Com.StudioTBD.CoronaIO.Menus
{
    /// <summary>
    /// This class is meant to only be used by one object that should be alive throughout
    /// all scenes i.e. the entire run of the application
    /// </summary>
    public class MenuManager : MonoBehaviour
    {
        #region Properties

        // Settings
        public static int mapScale = 10;
        public static int minSizeToSplit = 5;
        public static int maxNumOfHordes = 10;

        // Scenes
        public static int LAUNCHER = 0;
        public static int MAINMENU = 1;
        public static int GAME = 2;

        #endregion


        #region MonoBehaviour Callbacks

        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
            SceneManager.LoadScene(MAINMENU);
        }

        #endregion


        #region Public Static Methods

        /// <summary>
        /// Load main game
        /// </summary>
        public static void LoadGame()
        {
            SceneManager.LoadScene(GAME);
        }

        public static void QuitGame()
        {
            if (Application.isEditor)
                UnityEditor.EditorApplication.isPlaying = false;
            else
                Application.Quit();
        }

        #endregion
    }
}
