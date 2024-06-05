using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaviOnePoint : MonoBehaviour
{
    public GameObject Navi;
    public GameObject[] Waypoints;
    public float speed;
    int numDestino = 0;

    float distanceToPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Navi.transform.position = Vector3.MoveTowards(Navi.transform.position, Waypoints[numDestino].transform.position, speed);
        distanceToPoint = Vector3.Distance(Navi.transform.position, Waypoints[numDestino].transform.position);

        if (distanceToPoint < 1)
        {
            if (numDestino < Waypoints.Length - 1)
            {
                numDestino += 1;
            }
        }

    }
}
