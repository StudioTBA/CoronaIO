using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHordeCenterOfMass : MonoBehaviour
{
    GameObject com;
    public Transform ComTransform { get; private set; }

    public void createCenterOfMassGO(GameObject blip)
    {
        GameObject temp = new GameObject("Center Of Mass");
        com = Instantiate(temp, this.transform);
        Destroy(temp);

        blip.GetComponent<MiniMapIcon>().target = com.transform;
    }

    public void updateCenterOfMassPos()
    {
        Vector3 centerOfMass = Vector3.zero;
        List<Flocker> zombiesInHorde = this.GetComponent<FlockManager>().getZombieList();

        foreach (Flocker zombie in zombiesInHorde)
        {
            centerOfMass += zombie.transform.position;
        }

        centerOfMass /= zombiesInHorde.Count;

        com.transform.position = centerOfMass;
        ComTransform = com.transform;
    }
}
