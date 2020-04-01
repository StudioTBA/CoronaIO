using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHordeBlipCreator : MonoBehaviour
{
    FlockManager hordeManager;
    GameObject blipManager;
    GameObject blip;
    GameObject centerOfMass;
    public GameObject blipPrefab;


    void Awake()
    {
        hordeManager = this.GetComponent<FlockManager>();
        blipManager = GameObject.Find("ZombieHordeBlips");
        blip = Instantiate(blipPrefab, blipManager.transform);
        centerOfMass = gameObject.transform.Find("CenterOfMass").gameObject;
        blip.GetComponent<MiniMapIcon>().target = centerOfMass.transform;
        blipManager.GetComponent<ZombieHordeBlipManager>().addBlip(blip);
    }

}
