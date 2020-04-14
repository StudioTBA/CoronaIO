using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHordeBlipCreator : MonoBehaviour
{
    FlockManager hordeManager;
    GameObject blipManager;
    GameObject blip;
    public GameObject Blip { get { return blip; } }
    public GameObject blipPrefab;
    ZombieHordeCenterOfMass centerOfMass;

    void Start()
    {
        hordeManager = this.GetComponent<FlockManager>();
        centerOfMass = this.GetComponent<ZombieHordeCenterOfMass>();
        blipManager = GameObject.Find("ZombieHordeBlips");
        blip = Instantiate(blipPrefab, blipManager.transform);
        centerOfMass.createCenterOfMassGO(blip);
    }

    private void Update()
    {
        centerOfMass.updateCenterOfMassPos();
    }

    private void OnDestroy()
    {
        Destroy(blip);
    }

}
