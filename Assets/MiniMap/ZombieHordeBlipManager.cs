using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHordeBlipManager : MonoBehaviour
{

    public List<GameObject> hordeBlips;

    // Update is called once per frame
    void Update()
    {
        handleZombieHordeBlips();
    }

    private void handleZombieHordeBlips()
    {
        foreach (GameObject hordeBlip in hordeBlips)
        {
            if (hordeBlip.GetComponent<MiniMapIcon>().target == null)
            {
                hordeBlips.Remove(hordeBlip);
                Destroy(hordeBlip);
            }
        }
    }

    public void addBlip(GameObject zombieHordeBlip)
    {
        hordeBlips.Add(zombieHordeBlip);
    }
}
