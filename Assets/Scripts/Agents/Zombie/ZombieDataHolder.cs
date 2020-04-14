using System;
using UnityEngine;
using UnityEngine.AI;

namespace Com.StudioTBD.CoronaIO.Agent.Zombie
{
    public class ZombieDataHolder
    {
        public GameObject Target;
        public NavMeshAgent NavMeshAgent;
        public FlockManager FlockManager;
        public GameObject arriveParticleFXPrefab;
        public GameObject myArriveParticleFX;
        public Animator Animator;
    }
}
