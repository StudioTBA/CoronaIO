using System;
using System.Collections;
using System.Collections.Generic;
using Com.StudioTBD.CoronaIO.Agent;
using Com.StudioTBD.CoronaIO.FMS;
using UnityEngine;

public class HouseDoor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Make sure it is an agent
        if (!other.CompareTag("Agent")) return;
        other.GetComponent<Agent>().Consume(new HumanEvent(this.gameObject, HumanEvent.HumanEventType.EnteredShelter));
    }
}