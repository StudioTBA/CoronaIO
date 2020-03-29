using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
	public GameObject testAgent;
    // Start is called before the first frame update
    void Start()
    {
		Instantiate(testAgent, new Vector3(5, 0.5f, -5), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
