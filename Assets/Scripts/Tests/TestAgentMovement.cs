using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestAgentMovement : MonoBehaviour
{
    NavMeshAgent agent;
	Vector3 distToGoal;
    // Start is called before the first frame update
    void Start()
    {
		agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
		agent.speed = 10;
		agent.destination = new Vector3(70f, 0, 23f);
		Random.InitState(System.DateTime.Now.Millisecond);

	}

    // Update is called once per frame
    void Update()
	{
		distToGoal = agent.destination - transform.position;
		if (distToGoal.magnitude < 5)
		{
			agent.destination = new Vector3(Random.Range(-100, 100), 0, Random.Range(-100, 100));
		}

	}
}
