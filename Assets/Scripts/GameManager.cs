using System;
using System.Collections;
//using Boo.Lang;
using Com.StudioTBD.CoronaIO.Agent.Aggressors;
using Com.StudioTBD.CoronaIO.Agent.Human;
using UnityEngine;
using Com.StudioTBD.CoronaIO.Menus;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

namespace Com.StudioTBD.CoronaIO
{
    public class GameManager : MonoBehaviour
    {
        #region Properties

        [SerializeField] private GameObject floackHolder;

        // Generators
        public TileGenerator tileGenerator;
        public CivilianGenerator civilianGenerator;

        public static bool isSandbox = true;

        public GameObject[] Shelters { get; private set; }
        public List<GameObject> Humans { get; private set; } = new List<GameObject>();
        private GameObject[] spawns;


        #endregion


        #region GameTags

        public static class Tags
        {
            public static string HumanTag = "Human";
            public static string EnemyTag = "Zombie";
            public static string SpawnTag = "Spawn";
            public static string ShelterTag = "Shelter";
        }

        public static class Layers
        {
            public static string Enemy = "enemy";
            public static string Defences = "Defences";
            public static string Human = "Human";
        }

        #endregion

        #region Scales

        public static float HumanScale = 5f;

        #endregion

        #region MonoBehaviour Callbacks

        private void Start()
        {
            if (!Menus.MenuManager.isTest)
            {
                // Set values from Settings
                tileGenerator.SetFloorScale(Menus.MenuManager.mapScale * 10);

                // Create map
                StartCoroutine("GenerateTiles");

                // Find objects
                StartCoroutine("FindObjects");

                // Create civilians
                StartCoroutine("GenerateCivilians");
            }
            else
            {
                // Find objects
                StartCoroutine("FindObjects");


            }
        }

        public void Update()
        {
            if(Time.realtimeSinceStartup > 10)
            {
                if (floackHolder.GetComponentsInChildren<Flocker>().Length <= 0)
                    SceneManager.LoadScene(MenuManager.GAME_OVER);
                else if (civilianGenerator.Humans.Count == countNumberOfNull(civilianGenerator.Humans))
                    SceneManager.LoadScene(MenuManager.YOU_WIN);
            }
            

        }

        private int countNumberOfNull(List<GameObject> humans)
        {
            int count = 0;
            foreach (var h in humans)
            {
                if (h == null)
                    count++;
            }
            return count;
        }

        #endregion


        #region Finders

        private void FindSpawns()
        {
            spawns = GameObject.FindGameObjectsWithTag(Tags.SpawnTag);
        }

        private void FindShelters()
        {
            Shelters = GameObject.FindGameObjectsWithTag(Tags.ShelterTag);
        }

        private void FindAllHumans()
        {
            foreach (var humanAgent in FindObjectsOfType<HumanAgent>())
            {
                Humans.Add(humanAgent.gameObject);
            }

            foreach (var humanAgent in FindObjectsOfType<PoliceAgent>())
            {
                Humans.Add(humanAgent.gameObject);
            }
        }

        #endregion


        #region Coroutines

        IEnumerator FindObjects()
        {
            if (Time.realtimeSinceStartup < 1)
                yield return new WaitForSeconds(3f);

            FindSpawns();
            FindShelters();
            FindAllHumans();

            yield return null;
        }

        IEnumerator GenerateTiles()
        {
            tileGenerator.GenerateTiles();
            yield return null;
        }

        IEnumerator GenerateCivilians()
        {
            civilianGenerator.GenerateCivilians();
            yield return null;
        }

        #endregion


        #region Getters/Setters

        public GameObject[] GetSpawns()
        {
            return spawns;
        }

        #endregion
    }
}