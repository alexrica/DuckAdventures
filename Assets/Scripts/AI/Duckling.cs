using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.AI;

public class Duckling : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform target;
    float distance;
    public float maxDistance;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(target.transform.position, transform.position);
        if(maxDistance < distance)
        {
            agent.destination = target.position;
        }
        else
        {
            agent.destination = transform.position;
        }
    }
}
