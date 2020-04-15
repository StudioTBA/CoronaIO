using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentBlipCreator : MonoBehaviour
{
    GameObject blipManager;
    public GameObject blipPrefab;
    GameObject blip;

    void Awake()
    {
        blipManager = GameObject.Find("AgentBlips");

        if (blipPrefab && blipManager)
        {
            blip = Instantiate(blipPrefab, blipManager.transform);
            blip.GetComponent<MiniMapIcon>().target = this.transform;
        }

    }

    private void OnDestroy()
    {
        Destroy(blip);
    }
}
