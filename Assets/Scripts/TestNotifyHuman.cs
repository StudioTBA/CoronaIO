using System.Collections;
using System.Collections.Generic;
using Com.StudioTBD.CoronaIO.Agent.Human;
using Com.StudioTBD.CoronaIO.FMS;
using UnityEngine;

public class TestNotifyHuman : MonoBehaviour
{
    public HumanAgent targetHuman;

    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(NotifyHuman), 5f);
        // InvokeRepeating(nameof(NotifyHuman), 5f, 2f);
    }

    private void NotifyHuman()
    {
        Debug.Log("Notifying Human", this);
        targetHuman.Consume(new HumanEvent(this.gameObject, HumanEvent.HumanEventType.SpottedZombie));
    }
}