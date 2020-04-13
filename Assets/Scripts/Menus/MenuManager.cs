using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Com.StudioTBD.CoronaIO.Menus
{
    public class MenuManager : MonoBehaviour
    {
        public int mapScale;
        public int minSizeToSplit;
        public int maxNumOfHordes;

        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }

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
    }
}
