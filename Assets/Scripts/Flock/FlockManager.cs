﻿using System.Collections.Generic;
using Com.StudioTBD.CoronaIO.Menus;
using UnityEngine;

public class FlockManager : MonoBehaviour
{
    public GameObject flockPrefab;
    public GameObject flockHolder;
    public float flockMoveSpeed;
    [SerializeField] [Min(0)] int Initial_Horde_Size;
    [SerializeField] private bool TestMode;

    public int minHordeSizeToSplit;

    private List<Flocker> zombieList = new List<Flocker>();

    public bool active = true;

    public bool always_flee;
    public bool attack_if_able;
    public bool stop;

    // Start is called before the first frame update
    void Start()
    {
        minHordeSizeToSplit = MenuManager.minSizeToSplit;

        InvokeRepeating("UntrapZombies", 5, 5);
        float value = MenuManager.mapScale;
        if (Initial_Horde_Size > 0)
            transform.position = new Vector3(value * 40, transform.position.y, value * 40);
        while (Initial_Horde_Size > 0)
        {
            CreateZombie();
            Initial_Horde_Size--;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            Vector3 direction = Vector3.zero;

            if (TestMode)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    CreateZombie();
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
            }

            //Keys to control individual horde behavior
            //probably need a visual cue to indicate what their strategy is
            if (Input.GetKeyDown(KeyCode.Z))
            {
                always_flee = !always_flee;
                attack_if_able = false;
            }
            else if (Input.GetKeyDown(KeyCode.X))
            {
                attack_if_able = !attack_if_able;
                always_flee = false;
            }
            else if (Input.GetKeyDown(KeyCode.C))
            {
                stop = !stop;
            }

            transform.position += direction.normalized * flockMoveSpeed * Time.deltaTime;
            //GetComponent<NavMeshAgent>().SetDestination((direction + transform.position)*flockMoveSpeed);
        }
        if (zombieList.Count == 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void CreateZombie()
    {
        Vector3 randomPosInACube;

        float sizeFactor = transform.localScale.x;

        randomPosInACube = new Vector3(Random.Range(-sizeFactor * 2, sizeFactor * 2), sizeFactor / 1.2f, Random.Range(-sizeFactor * 2, sizeFactor * 2));
        GameObject Swarmling = (GameObject)Instantiate(flockPrefab, transform.position + randomPosInACube,
            Quaternion.identity);

        Swarmling.transform.localScale = transform.localScale;

        Swarmling.transform.parent = flockHolder.transform;

        AttachZombie(Swarmling.GetComponent<Flocker>());
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

            newHorde.flockHolder = flockHolder;
            newHorde.transform.position = transform.position;

            int amount = zombieList.Count / 2;

            while (zombieList.Count > amount)
            {
                zombieList[0].GetComponentInChildren<SkinnedMeshRenderer>().materials[1].SetFloat("_Outline", 0f);
                newHorde.AttachZombie(zombieList[0]);
                RemoveZombie(zombieList[0]);
            }

            newHorde.active = false;

            newHorde.CopyState(this);

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

    public void CopyState(FlockManager other)
    {
        always_flee = other.always_flee;
        attack_if_able = other.attack_if_able;
        stop = other.stop;
        transform.localScale = other.transform.localScale;
        minHordeSizeToSplit = other.minHordeSizeToSplit;
    }

    private void UntrapZombies()
    {
        float dist;

        float maxDist = float.MinValue;
        float minDist = float.MaxValue;

        Flocker furthest = null;

        foreach (Flocker zombie in zombieList)
        {
            dist = (transform.position - zombie.transform.position).magnitude;

            if (dist > maxDist)
            {
                furthest = zombie;
                maxDist = dist;
            }
            if (dist < minDist)
                minDist = dist;
        }

        if (maxDist / minDist > 50.0f)
        {
            Vector3 newPos = (transform.position - furthest.transform.position) / 2 + furthest.transform.position;

            newPos = new Vector3(newPos.x, transform.localScale.x, newPos.z);

            furthest.transform.position = newPos;
        }

    }

    public void DestroyZombie(Flocker other)
    {
        zombieList.Remove(other);
        Destroy(other.gameObject);
    }
}