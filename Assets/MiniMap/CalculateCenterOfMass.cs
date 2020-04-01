using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateCenterOfMass : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        updateCenterOfMass();
    }

    public void updateCenterOfMass()
    {
        Vector3 centerOfMassPos = Vector3.zero;
        List<Flocker> zombieList = this.transform.parent.GetComponent<FlockManager>().getZombieList();

        if (zombieList.Count == 0)
            return;

        foreach (Flocker zombie in zombieList)
        {
            centerOfMassPos += zombie.transform.position;
        }

        this.transform.position = centerOfMassPos / zombieList.Count;
    }
}
