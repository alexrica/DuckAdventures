using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class BigDuck : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform[] Goal;
    int WaypointNumber = 0;
    float dist;
    GameObject player;
    public GameManager gameManager;

    [Header("Assigned in runtime")]
    public bool crossing = false;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(Goal[WaypointNumber].position, transform.position);
        if (dist <= 1)
            ChangeWaypoint();
    }

    public void GoToWaypoint()
    {
        agent.destination = Goal[WaypointNumber].position;
    }

    void ChangeWaypoint()
    {
        switch (WaypointNumber)
        {
            case 0:
                WaypointNumber++;
                GoToWaypoint();
                break;

            case 1:
                gameManager.familyCrossing = true;
                WaypointNumber++;
                GoToWaypoint();
                break;

            case 2:
                WaypointNumber++;
                GoToWaypoint();
                break;

            case 3:
                gameManager.familyCrossing = false;
                WaypointNumber++;
                GoToWaypoint();
                break;

            case 4:
                if (player.GetComponent<PlayerController>().crossed == true)
                {
                    Debug.Log("cross time");
                    WaypointNumber++;
                    GoToWaypoint();
                }
                else
                {
                    return;
                }
                break;

            case 5:
                break;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Death":
                gameManager.DeadFamily();
                break;
        }
    }
}
