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
    public GameObject comPrefab;

    void Start()
    {
        hordeManager = this.GetComponent<FlockManager>();
        centerOfMass = this.GetComponent<ZombieHordeCenterOfMass>();

        blipManager = GameObject.Find("ZombieHordeBlips");

        if (blipPrefab && blipManager)
            blip = Instantiate(blipPrefab, blipManager.transform);

        if (comPrefab && blip)
            centerOfMass.createCenterOfMassGO(comPrefab, blip);
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
