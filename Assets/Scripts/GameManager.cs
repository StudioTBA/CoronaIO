using System;
using System.Collections;
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
        public GameObject[] Shelters { get; private set; }
        private GameObject[] spawns;


        #endregion


        #region MonoBehaviour Callbacks


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
