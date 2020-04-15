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
        #region Static Properties

        // Settings
        public static int mapScale = 10;
        public static int minSizeToSplit = 5;
        public static int maxNumOfHordes = 10;

        // Scenes
        public static int LAUNCHER = 0;
        public static int MAINMENU = 1;
        public static int GAME = 2;
        public static int HUMANS_BECOME_POLICE = 3;
        public static int HUMANS_FIND_SHELTER = 4;
        public static int HUMANS_WANDER = 5;
        public static int POLICE_ATTACK = 6;
        public static int POLICE_WANDER = 7;
        public static int ZOMBIES_ARRIVE = 8;
        public static int ZOMBIES_ATTACK = 9;
        public static int ZOMBIES_FLEE = 10;
        public static int ZOMBIES_WANDER = 11;

        // Other
        [Tooltip("Indicates if the scene loaded is a demo or not")]
        public static bool isTest;

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
