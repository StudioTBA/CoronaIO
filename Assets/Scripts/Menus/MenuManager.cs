using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Com.StudioTBD.CoronaIO.Menus
{
    public class MenuManager : MonoBehaviour
    {
        #region Properties

        public int mapScale;
        public int minSizeToSplit;
        public int maxNumOfHordes;

        #endregion


        #region MonoBehaviour Callbacks

        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }

        #endregion


        #region Public Methods

        /// <summary>
        /// Load main game
        /// </summary>
        public void LoadGame()
        {
            Settings settings = Resources.FindObjectsOfTypeAll<Settings>()[0] as Settings;

            mapScale = settings.GetMapScale();
            minSizeToSplit = settings.GetMinSizeToSplit();
            maxNumOfHordes = settings.GetMaxNumOfHordes();

            SceneManager.LoadScene(1);
        }

        #endregion


        #region Getters/Setters

        public int GetMapScale()
        {
            return mapScale;
        }

        public int GetMinSizeToSplit()
        {
            return minSizeToSplit;
        }

        public int GetMaxNumOfHordes()
        {
            return maxNumOfHordes;
        }

        #endregion
    }
}
