using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Com.StudioTBD.CoronaIO.MainMenu
{
    public class MainMenu : MonoBehaviour
    {
        /// <summary>
        /// Load main game
        /// </summary>
        public void LoadGame()
        {
            SceneManager.LoadScene(1);
        }
    }
}
