using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveTo : MonoBehaviour
{
    public GameObject player;
    public GameObject Navi;
    public GameObject[] Waypoints;
    public float speed;
    int numDestino;

    float distanceToPoint;
    public bool arrived = false;

    float distancePlayerNavi;

    public bool goingToPlayer;
    bool waitingCorroutine;

    // Start is called before the first frame update
    void Start()
    {
        numDestino = 0;
        arrived = false;
        goingToPlayer = false;
        waitingCorroutine = false;
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPoint = Vector3.Distance(Navi.transform.position, Waypoints[numDestino].transform.position);
        distancePlayerNavi = Vector3.Distance(Navi.transform.position, player.transform.position);

        if (distanceToPoint < 1)
        {
            arrived = true;
        }

        if (arrived == true)
        {
            if (goingToPlayer == true)
            {
                MoveTowardsPlayer();
                if (waitingCorroutine == false && distancePlayerNavi < 4)
                {
                    StartCoroutine(WaitForSeconds(3));
                }
            }
            else
            {
                WaitForPlayerToArrive();
            }
        }
        else
        {
            MoveTowardsWaypoint();
        }
    }

    void WaitForPlayerToArrive()
    {
        if (distancePlayerNavi <= 4 && arrived == true)
        {
            if (distanceToPoint <= 4)
            {
                if (numDestino < Waypoints.Length)
                {
                    numDestino += 1;
                }
                arrived = false;
            }
            else if (waitingCorroutine == false && distanceToPoint > 4)
            {
                StartCoroutine(WaitOnlyTime(5));
            }
        }
        else if (distancePlayerNavi > 4 && arrived == true)
        {
            if (waitingCorroutine == false)
            {
                StartCoroutine(WaitForSeconds(5));
            }
        }
    }

    void MoveTowardsWaypoint()
    {
        Navi.transform.position = Vector3.MoveTowards(Navi.transform.position, Waypoints[numDestino].transform.position, speed);
    }

    void MoveTowardsPlayer()
    {
        Navi.transform.position = Vector3.MoveTowards(Navi.transform.position, player.transform.position + new Vector3(0, 1, 0), speed);
    }

    IEnumerator WaitForSeconds(float time)
    {
        waitingCorroutine = true;
        yield return new WaitForSeconds(time);
        goingToPlayer = !goingToPlayer;
        waitingCorroutine = false;
    }

    IEnumerator WaitOnlyTime (float time)
    {
        waitingCorroutine = true;
        yield return new WaitForSeconds(time);
        arrived = false;
        waitingCorroutine = false;
    }
}
