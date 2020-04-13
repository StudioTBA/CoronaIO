using System;
using System.Collections;
using Boo.Lang;
using Com.StudioTBD.CoronaIO.Agent.Aggressors;
using Com.StudioTBD.CoronaIO.Agent.Human;
using Com.StudioTBD.CoronaIO.FMS;
using UnityEngine;

namespace Com.StudioTBD.CoronaIO
{
    public class GameManager : MonoBehaviour
    {
        #region Properties

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

        public static float HumanScale = 50f;

        #endregion

        #region MonoBehaviour Callbacks

        private void Start()
        {
            if (!Menus.MenuManager.isTest)
            {
                // Set values from Settings
                tileGenerator.SetFloorScale(Menus.MenuManager.mapScale * 10);
                // TODO: Replace by line to set minSizeToSplit
                // TODO: Replace by line to set maxNumOfHordes

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