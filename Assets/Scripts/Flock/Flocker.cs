using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flocker : MonoBehaviour
{
    public GameObject target;
    public float seekSpeed;
    public float repulsionFactor;
    public float cohesionFactor;
    public float repulsionQueryRadius;
    public float cohesionQueryRadius;
    public float alignmentFactor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Cohesion();
        Repulsion();
        Alignment();
        CollectiveMotion();
    }

    private void Cohesion()
    {
        Collider[] nearbyColliders = Physics.OverlapSphere(transform.position, cohesionQueryRadius);

        Vector3 avgPos = new Vector3(0.0f, 0.0f, 0.0f);
        foreach (Collider neighbor in nearbyColliders)
        {
           avgPos += neighbor.transform.position;
        }
        avgPos /= nearbyColliders.Length;
        Vector3 forceVec = (avgPos - transform.position) * cohesionFactor;
        //transform.position += forceVec.normalized * Time.deltaTime;
        GetComponent<Rigidbody>().velocity = forceVec.normalized;
        //GetComponent<Rigidbody>().AddForce(forceVec);
    }

    private void Repulsion()
    {
        Collider[] nearbyColliders = Physics.OverlapSphere(transform.position, repulsionQueryRadius);

        Vector3 avgPos = new Vector3(0.0f, 0.0f, 0.0f);
        foreach (Collider neighbor in nearbyColliders)
        {
            avgPos += neighbor.transform.position;
        }
        avgPos /= nearbyColliders.Length;
        Vector3 forceVec = -(avgPos - transform.position) * repulsionFactor;
        //transform.position += forceVec.normalized * Time.deltaTime;
        GetComponent<Rigidbody>().velocity = forceVec.normalized;
        //GetComponent<Rigidbody>().AddForce(forceVec);
    }

    private void Alignment()
    {
        Vector3 targetPosition = new Vector3(0.0f, 0.0f, 0.0f);
        
        if(target != null)
        {
            targetPosition = (target.transform.position-transform.position).normalized;
        }
        else
        {
            targetPosition = GetComponent<Rigidbody>().velocity.normalized;
        }

        Quaternion goalHeading = Quaternion.LookRotation(targetPosition, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, goalHeading, alignmentFactor);
    }

    private void CollectiveMotion()
    {
        if (target != null)
        {
            Vector3 direction = (target.transform.position - transform.position).normalized;
            //transform.position += direction * seekSpeed * Time.deltaTime;
            GetComponent<Rigidbody>().velocity = direction * seekSpeed;
            //GetComponent<Rigidbody>().AddForce(direction * seekSpeed);
        }
    }
}
