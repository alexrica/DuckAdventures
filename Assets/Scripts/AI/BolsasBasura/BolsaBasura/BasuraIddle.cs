using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasuraIddle : StateMachineBehaviour
{
    NavMeshAgent Agent;
    public Transform spawnPos;
    GameObject Player;

    public float amplitude;
    public float frequency;
    float tiempoDevorar;
    public bool enSpawnPos;
    int arrayPos;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.name == "BolsaBasura1")
        {
            arrayPos = 0;
        }
        else if (animator.name == "BolsaBasura2")
        {
            arrayPos = 1;
        }

        animator.SetBool("ReturnIddle", false);
        animator.SetBool("Escaped", false);
        animator.SetBool("Search", false);
        animator.SetBool("Follow", false);

        Agent = animator.gameObject.GetComponent<NavMeshAgent>();
        spawnPos = animator.gameObject.GetComponent<EnemyBasuraAgent>().spawnPos[arrayPos];
        tiempoDevorar = animator.gameObject.GetComponent<EnemyBasuraAgent>().tiempoDevorar;

        enSpawnPos = false;

        Agent.isStopped = false;

        if (Player == null)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        RaycastHit hit;
        animator.transform.LookAt(Player.transform.position);
        if (enSpawnPos == false)
        {
            if (!Agent.pathPending && Agent.remainingDistance < 0.5f)
            {
                enSpawnPos = true;
            }
            else
            {
                Agent.destination = spawnPos.position;
            }
        }
        else if(enSpawnPos == true)
        {

            Debug.DrawRay(animator.transform.position + Vector3.up * 0.3f, animator.transform.TransformDirection(Vector3.forward) * 8, Color.white);
            if (Physics.Raycast(animator.transform.position + Vector3.up * 0.3f, animator.transform.TransformDirection(Vector3.forward), out hit, 8))
            {
                animator.SetBool("Follow", true);
            }
        }
        //else
        //{
        //    if (Vector3.Distance(animator.transform.position, Player.transform.position) < radioAtaque)
        //    {
        //        if (Vector3.Distance(animator.transform.position, Player.transform.position) < radioAtaque)
        //        {
        //            if (timeRemaining > 0)
        //            {
        //                timeRemaining -= Time.deltaTime;
        //            }
        //            else
        //            {
        //                animator.SetBool("Bite", true);
        //            }
        //        }
        //        else
        //        {
        //            timeRemaining = tiempoDevorar;
        //        }
        //    }
        //    else
        //    {
        //        timeRemaining = tiempoDevorar;
        //    }
        //}
    }
}