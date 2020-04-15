using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.StudioTBD.CoronaIO.Agent.Aggressors;
using Com.StudioTBD.CoronaIO.FMS;
using System;

public class DefencePoint : MonoBehaviour
{

    [SerializeField] private LayerMask layer;
    [SerializeField] private float NotifyRange = 50f;

   
    /// <summary>
    /// informs all nearby agents to come defend the defense point 
    /// when a enemy has crossed the defence point collider
    /// </summary>
    /// <param name="other"></param>

    private void OnTriggerStay(Collider other)
    {
        
        if (other.tag == "Zombie")
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, NotifyRange, layer);
            //Debug.Log("Defence point attacked");
            if (colliders.Length > 0)
            {
                HumanEvent @event = new HumanEvent(this.gameObject, HumanEvent.HumanEventType.PoliceAlert);
                //Debug.Log("police notified");
                foreach (Collider c in colliders)
                {
                    try
                    {
                        c.GetComponentInParent<PoliceAgent>().stateMachine.CurrentState.Consume(@event);
                    }
                    catch(NullReferenceException e)
                    {
                        continue;
                    }
                    


                }


            }
        }

        

       
    }
}



