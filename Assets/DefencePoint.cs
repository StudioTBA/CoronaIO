using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.StudioTBD.CoronaIO.Agent.Aggressors;
using Com.StudioTBD.CoronaIO.FMS;
public class DefencePoint : MonoBehaviour
{

    [SerializeField] private LayerMask layer;
    [SerializeField] private float NotifyRange = 50f;

   
    /// <summary>
    /// informs the all nearby agents to come defent the defenc e point when a enemy
    /// has crossed the defence point collider
    /// </summary>
    /// <param name="other"></param>

    private void OnTriggerStay(Collider other)
    {

        if (other.tag == "Zombie")
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, NotifyRange, layer);

            if (colliders.Length > 0)
            {
                HumanEvent @event = new HumanEvent(this.gameObject, HumanEvent.HumanEventType.PoliceAlert);

                foreach (Collider c in colliders)
                {

                    c.GetComponentInParent<PoliceAgent>().stateMachine.CurrentState.Consume(@event);


                }


            }
        }

        

       
    }
}



