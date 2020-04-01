using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHordeBlipCreator : MonoBehaviour
{
    FlockManager hordeManager;
    GameObject blipManager;
    GameObject blip;
    public GameObject blipPrefab;


    void Awake()
    {
        hordeManager = this.GetComponent<FlockManager>();
        blipManager = GameObject.Find("ZombieHordeBlips");
        blip = Instantiate(blipPrefab, blipManager.transform);
        blip.GetComponent<MiniMapIcon>().target = this.transform;
        blipManager.GetComponent<ZombieHordeBlipManager>().addBlip(blip);
    }

}
