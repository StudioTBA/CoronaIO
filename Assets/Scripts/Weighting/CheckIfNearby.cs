using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class CheckIfNearby : MonoBehaviour
{

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Tile")
        {
            col.gameObject.GetComponentInChildren<NavMeshModifierVolume>().area = 4;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Tile")
        {
                col.gameObject.GetComponentInChildren<NavMeshModifierVolume>().area = 0;
        }
    }
}