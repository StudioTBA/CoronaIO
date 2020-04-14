using System.Collections;
using System.Collections.Generic;
using Com.StudioTBD.CoronaIO;
using UnityEngine;
using Com.StudioTBD.CoronaIO;

public class Flocker : MonoBehaviour, System.IEquatable<Flocker>
{
    public GameObject target;
    public float seekSpeed;
    public float repulsionFactor;
    public float cohesionFactor;
    public float repulsionQueryRadius;
    public float cohesionQueryRadius;
    public float alignmentFactor;

    [Tooltip("Percentage of human health inflicted on collision")]
    public int damageToHuman;
    [Tooltip("Percentage of shelter health inflicted on collision")]
    public int damageShelter;

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
            if (neighbor.gameObject.GetComponent<Flocker>())
                avgPos += neighbor.transform.position;
        }

        avgPos /= nearbyColliders.Length;
        Vector3 forceVec = (avgPos - transform.position) * cohesionFactor;
        GetComponent<Rigidbody>().velocity = forceVec.normalized;
    }

    private void Repulsion()
    {
        Collider[] nearbyColliders = Physics.OverlapSphere(transform.position, repulsionQueryRadius);

        Vector3 avgPos = new Vector3(0.0f, 0.0f, 0.0f);
        foreach (Collider neighbor in nearbyColliders)
        {
            if (neighbor.gameObject.GetComponent<Flocker>())
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

        if (target != null)
        {
            targetPosition = (target.transform.position - transform.position);
            targetPosition = new Vector3(targetPosition.x, 0, targetPosition.z);
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

    //This function is specifically allowing zombies to be removed form horde lists
    public bool Equals(Flocker other)
    {
        return (transform.position == other.transform.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Infection
        //Debug.Log($"[Collision] {collision.gameObject.name}");
        // Debug.Log($"[Collider] {collision.collider.gameObject.name}");
        if (collision.collider.CompareTag(GameManager.Tags.HumanTag))
        {
            GameObject parent = collision.collider.transform.parent.gameObject;
            parent.GetComponentInChildren<HealthBar>();
            //Debug.Log($"Proper human {parent.name}", this);
            // GameObject human = collision.collider.GetComponent<HealthBar>();
            // HealthBar civilianHealth = collision.collider.GetComponentInChildren<HealthBar>();
            HealthBar civilianHealth = parent.GetComponentInChildren<HealthBar>();
            civilianHealth.TakeDamage(damageToHuman);

            if (civilianHealth.GetHealth() == 0)
            {
                //Debug.Log("Infected");
                
                target.GetComponent<FlockManager>().CreateZombie();
                Destroy(parent);
            }
        }
        if (collision.gameObject.GetComponentInParent<Shelter>())
        {
            HealthBar shelterHealth = collision.gameObject.GetComponentInParent<Shelter>().healthBar;
            shelterHealth.TakeDamage(damageShelter);

            print("hit");

            if (shelterHealth.GetHealth() == 0)
            {
                collision.gameObject.transform.parent.gameObject.GetComponent<Shelter>().RemoveFromNavmesh();
            }
        }
    }

    public void GotHit()
    {
        target.GetComponent<FlockManager>().DestroyZombie(this);
    }
}