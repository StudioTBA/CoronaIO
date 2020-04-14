using System;
using UnityEngine;
using UnityEngine.AI;


namespace Com.StudioTBD.CoronaIO.Agent.Human
{
    /// <summary>
    /// This is an example class to show how to share data between the states.
    /// It can contain any kind of data.
    /// In this case since we are implementing basic arrive/flee,
    /// we need to have the position of the target.
    /// </summary>
    public class DataHolder
    {
        public GameObject Target;
        public NavMeshAgent NavMeshAgent;
        public GameObject PolicePrefab;
        public float BecomeAggressorProbability;
    }
}