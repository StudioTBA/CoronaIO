using System;
using UnityEngine;

namespace Com.StudioTBD.CoronaIO
{
    public class GameManager : MonoBehaviour
    {
        public GameObject[] Shelters { get; private set; }
        
        private void Start()
        {
            FindShelters();
        }

        private void FindShelters()
        {
            Shelters = GameObject.FindGameObjectsWithTag("Shelter");
        }
    }
}