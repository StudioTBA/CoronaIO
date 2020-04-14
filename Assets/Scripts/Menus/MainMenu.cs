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
            MenuManager.isTest = false;
            MenuManager.LoadGame();
        }

        public void QuitGame()
        {
            MenuManager.QuitGame();
        }

        // Humans
        public void LoadHumansFindShelter()
        {
            MenuManager.isTest = true;
            SceneManager.LoadScene(MenuManager.HUMANS_FIND_SHELTER);
        }

        public void LoadHumansFlee()
        {
            MenuManager.isTest = true;
            SceneManager.LoadScene(MenuManager.HUMANS_FLEE);
        }

        public void LoadHumansWander()
        {
            MenuManager.isTest = true;
            SceneManager.LoadScene(MenuManager.HUMANS_WANDER);
        }


        // Police
        public void LoadPoliceDefend()
        {
            MenuManager.isTest = true;
            SceneManager.LoadScene(MenuManager.POLICE_DEFEND);
        }

        public void LoadPoliceWander()
        {
            MenuManager.isTest = true;
            SceneManager.LoadScene(MenuManager.POLICE_WANDER);
        }

        // Zombies
        public void LoadZombiesArrive()
        {
            MenuManager.isTest = true;
            SceneManager.LoadScene(MenuManager.ZOMBIES_ARRIVE);
        }

        public void LoadZombiesAttack()
        {
            MenuManager.isTest = true;
            SceneManager.LoadScene(MenuManager.ZOMBIES_ATTACK);
        }

        public void LoadZombiesFlee()
        {
            MenuManager.isTest = true;
            SceneManager.LoadScene(MenuManager.ZOMBIES_FLEE);
        }

        public void LoadZombiesWander()
        {
            MenuManager.isTest = true;
            SceneManager.LoadScene(MenuManager.ZOMBIES_WANDER);
        }
    }
}
