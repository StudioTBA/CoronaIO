using System.Collections;
using System.Collections.Generic;
using Com.StudioTBD.CoronaIO.Agent.Zombie;
using Com.StudioTBD.CoronaIO.Agent.Zombie.States;
using UnityEngine;

public class FlockManager : MonoBehaviour
{
    public GameObject flockPrefab;
    public GameObject flockHolder;
    public float flockMoveSpeed;

    public int minHordeSizeToSplit;

    private List<Flocker> zombieList = new List<Flocker>();

    public bool active = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            Vector3 randomPosInACube;
            Vector3 direction = Vector3.zero;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                randomPosInACube = new Vector3(Random.Range(-100.0f, 100.0f), 25.0f, Random.Range(-100.0f, 100.0f));
                GameObject Swarmling = (GameObject)Instantiate(flockPrefab, transform.position + randomPosInACube,
                    Quaternion.identity);

                Swarmling.transform.parent = flockHolder.transform;

                AttachZombie(Swarmling.GetComponent<Flocker>());
            }

            if (Input.GetKey(KeyCode.I))
            {
                direction += new Vector3(0, 0, 1.0f);
            }
            if (Input.GetKey(KeyCode.J))
            {
                direction += new Vector3(-1.0f, 0, 0);
            }
            if (Input.GetKey(KeyCode.K))
            {
                direction += new Vector3(0, 0, -1.0f);
            }
            if (Input.GetKey(KeyCode.L))
            {
                direction += new Vector3(1.0f, 0, 0);
            }


            transform.position += direction.normalized * flockMoveSpeed * Time.deltaTime;
        }
    }

    public void AttachZombie(Flocker zombie)
    {
        zombie.target = this.gameObject;
        zombieList.Add(zombie);
    }

    public void RemoveZombie(Flocker zombie)
    {
        zombieList.Remove(zombie);
    }

    public FlockManager SplitHorde(FlockManager newHorde)
    {
        if (zombieList.Count >= minHordeSizeToSplit)
        {
            newHorde.transform.parent = transform.parent;

            newHorde.gameObject.GetComponent<ZombieAgent>().stateMachine.ResetToDefaultState();

            newHorde.flockHolder = flockHolder;
            newHorde.transform.position = transform.position;

            int amount = zombieList.Count / 2;

            while (zombieList.Count > amount)
            {
                newHorde.AttachZombie(zombieList[0]);
                RemoveZombie(zombieList[0]);
            }

            active = false;

            return newHorde;
        }

        return null;
    }

    public void AbsorbHorde(FlockManager otherHorde)
    {
        //Change target of all zombies in other horde

        //Add them to appropriate list

        foreach (Flocker zombie in otherHorde.zombieList)
        {
            AttachZombie(zombie);
        }

        //Destroy other horde

        Destroy(otherHorde.gameObject);
    }
    public int HordeSize()
    {
        return zombieList.Count;
    }

    public List<Flocker> getZombieList()
    {
        return zombieList;
    }
}