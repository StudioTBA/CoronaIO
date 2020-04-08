using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHordeBlipManager : MonoBehaviour
{
    FlockManager hordeManager;
    GameObject miniMap;
    GameObject blip;
    GameObject centerOfMass;
    public GameObject blipPrefab;


    void Awake()
    {
        hordeManager = this.GetComponent<FlockManager>();
        miniMap = GameObject.Find("MiniMap");
        blip = Instantiate(blipPrefab, miniMap.transform);
        centerOfMass = gameObject.transform.Find("CenterOfMass").gameObject;
    }

    private void Update()
    {
        centerOfMass.transform.position = hordeManager.getCenterOfMass();
        blip.GetComponent<MiniMapIcon>().target = centerOfMass.transform;
    }

}
