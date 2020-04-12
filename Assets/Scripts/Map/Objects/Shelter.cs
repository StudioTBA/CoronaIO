using System;
using UnityEngine;
using Com.StudioTBD.CoronaIO.Agent;

namespace Com.StudioTBD.CoronaIO
{ 
    public class Shelter : Agent.Agent
    {
        public MeshRenderer floorMeshRenderer;

        public void RemoveFromNavmesh()
        {
            Destroy(this.gameObject);
        }

        
    }
}