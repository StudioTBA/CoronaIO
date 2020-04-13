using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.StudioTBD.CoronaIO.Menus
{
    public class MainMenu : MonoBehaviour
    {
        public void LoadGame()
        {
            MenuManager.LoadGame();
        }

        public void QuitGame()
        {
            MenuManager.QuitGame();
        }
    }
}
