using System;
using System.Collections;
using Boo.Lang;
using Com.StudioTBD.CoronaIO.Agent.Aggressors;
using Com.StudioTBD.CoronaIO.Agent.Human;
using UnityEngine;

namespace Com.StudioTBD.CoronaIO
{
    public class GameManager : MonoBehaviour
    {
        #region Properties

        // Generators
        public TileGenerator tileGenerator;
        public CivilianGenerator civilianGenerator;

        // Map objects
        private static string _humanTag = "HumanTag";
        public GameObject[] Shelters { get; private set; }
        private GameObject[] spawns;


        #endregion


        #region MonoBehaviour Callbacks


        public List<GameObject> Humans { get; private set; } = new List<GameObject>();

        private void Start()
        {
            // Create map
            StartCoroutine("GenerateTiles");

            // Find objects
            StartCoroutine("FindObjects");

            // Create civilians
            StartCoroutine("GenerateCivilians");
        }


        #endregion


        #region Finders


        private void FindSpawns()
        {
            spawns = GameObject.FindGameObjectsWithTag("Spawn");
        }

        private void FindShelters()
        {
            Shelters = GameObject.FindGameObjectsWithTag("Shelter");
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
