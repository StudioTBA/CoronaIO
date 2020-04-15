using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.StudioTBD.CoronaIO
{
    public class CivilianGenerator : MonoBehaviour
    {
        #region Properties

        // Prefabs
        public GameObject humanPrefab;
        public GameObject policePrefab;

        public List<GameObject> Humans { get; private set; } = new List<GameObject>();

        // Probabilities
        [Tooltip("Probability of creating a civilian in a particular spawn. " +
            "For example, if set to 20, the probability of having a character " +
            "spawn in this spot is 20%")]
        public float spawningProbability;

        [Tooltip("Probability of a police agent spawning instead of a human")]
        public float policeProbability;

        private GameManager gameManager;
        private GameObject I;

        #endregion


        #region MonoBehaviour Callbacks

        private void Awake()
        {
            gameManager = GetComponent<GameManager>();
        }

        #endregion


        #region Public Methods

        public void GenerateCivilians()
        {
            foreach (GameObject spawn in gameManager.GetSpawns())
            {
                float probability = UnityEngine.Random.Range(0.0f, 100.0f);

                // If random number is within the spawning probability
                if (probability < spawningProbability)
                {
                    probability = UnityEngine.Random.Range(0.0f, 100.0f);

                    // If random number is within the probability of spawning police
                    // Else spawn a human
                    if (probability < policeProbability)
                        I = Instantiate(policePrefab, spawn.transform);
                    else
                        I = Instantiate(humanPrefab, spawn.transform);

                    Humans.Add(I);

                    
                }
            }
        }

        #endregion
    }
}
