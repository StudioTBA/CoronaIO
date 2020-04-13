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

        public static bool isSandbox = false;

        public GameObject[] Shelters { get; private set; }
        private GameObject[] spawns;
        public List<GameObject> Humans { get; private set; } = new List<GameObject>();

        #endregion

        // Map objects

        #region GameTags

        public static class Tags
        {
            public static string HumanTag = "Human";
            public static string EnemyTag = "Enemy";
            public static string SpawnTag = "Spawn";
            public static string ShelterTag = "Shelter";
        }

        #endregion


        #region MonoBehaviour Callbacks

        private void Start()
        {
            if (isSandbox)
            {
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
    }
}