using System;
using Boo.Lang;
using UnityEngine;
using Com.StudioTBD.CoronaIO.Agent;
using Com.StudioTBD.CoronaIO.Agent.Human;
using Com.StudioTBD.CoronaIO.FMS;

namespace Com.StudioTBD.CoronaIO
{
    public class Shelter : Agent.Agent
    {
        public MeshRenderer floorMeshRenderer;
        public float ShelterRadius;

        public void RemoveFromNavmesh()
        {
            GetHumansAround();
            Destroy(this.gameObject);
        }


        public void GetHumansAround()
        {
            List<HumanAgent> humanAgents = new List<HumanAgent>();
            var colliders = Physics.OverlapSphere(transform.position, ShelterRadius, LayerMask.GetMask("Human"));
            foreach (var collider in colliders)
            {
                var human = collider.GetComponent<HumanAgent>();
                if (human != null)
                {
                    humanAgents.Add(human);
                }
            }

            NotifyShelterDestroyed(humanAgents);
        }

        public void NotifyShelterDestroyed(List<HumanAgent> humans)
        {
            foreach (var human in humans)
            {
                human.Consume(new HumanEvent(this.gameObject, HumanEvent.HumanEventType.DestroyedShelter));
            }
        }
    }
}