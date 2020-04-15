using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CheckIfInfested : MonoBehaviour
{

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Tile")
        {
            col.gameObject.GetComponentInChildren<NavMeshModifierVolume>().area = 3;
        }
    }

    /*private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Tile")
        {
            col.gameObject.GetComponentInChildren<NavMeshModifierVolume>().area = 0;
        }
    }*/
}
