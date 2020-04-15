using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestAreaChang : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Tile")
        {
            col.gameObject.GetComponentInChildren<NavMeshModifierVolume>().area=3;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Tile")
        {
            col.gameObject.GetComponentInChildren<NavMeshModifierVolume>().area = 0;
        }
    }
}
