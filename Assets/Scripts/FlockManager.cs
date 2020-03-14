using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockManager : MonoBehaviour
{
    public GameObject flockPrefab;
    public GameObject flockHolder;
    public float flockMoveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 randomPosInACube;
        Vector3 normalizedDirection;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            randomPosInACube = new Vector3(Random.Range(-10.0f, 10.0f), 0, Random.Range(-10.0f, 10.0f));
            GameObject Swarmling = (GameObject)Instantiate(flockPrefab, transform.position + randomPosInACube, Quaternion.identity);
            Swarmling.transform.parent = flockHolder.transform;
            Swarmling.GetComponent<Flocker>().target = this.gameObject;
        }

        normalizedDirection = (new Vector3(1.0f, 0, 0) * Input.GetAxis("Horizontal") + new Vector3(0, 0, 1.0f) * Input.GetAxis("Vertical")).normalized;

        transform.position += normalizedDirection * flockMoveSpeed * Time.deltaTime;

    }
}
