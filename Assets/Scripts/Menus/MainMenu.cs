using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Com.StudioTBD.CoronaIO.Menus
{
    public class MainMenu : MonoBehaviour
    {

        // Main game
        public void LoadGame()
        {
            MenuManager.LoadGame();
        }

        public void QuitGame()
        {
            MenuManager.QuitGame();
        }

        // Humans
        public void LoadHumansFindShelter()
        {
            SceneManager.LoadScene(MenuManager.HUMANS_FIND_SHELTER);
        }

        public void LoadHumansFlee()
        {
            SceneManager.LoadScene(MenuManager.HUMANS_FLEE);
        }

        public void LoadHumansWander()
        {
            SceneManager.LoadScene(MenuManager.HUMANS_WANDER);
        }


        // Police
        public void LoadPoliceDefend()
        {
            SceneManager.LoadScene(MenuManager.POLICE_DEFEND);
        }

        public void LoadPoliceWander()
        {
            SceneManager.LoadScene(MenuManager.POLICE_WANDER);
        }

        // Zombies
        public void LoadZombiesArrive()
        {
            SceneManager.LoadScene(MenuManager.ZOMBIES_ARRIVE);
        }

        public void LoadZombiesAttack()
        {
            SceneManager.LoadScene(MenuManager.ZOMBIES_ATTACK);
        }

        public void LoadZombiesFlee()
        {
            SceneManager.LoadScene(MenuManager.ZOMBIES_FLEE);
        }

        public void LoadZombiesWander()
        {
            SceneManager.LoadScene(MenuManager.ZOMBIES_WANDER);
        }
    }
}
