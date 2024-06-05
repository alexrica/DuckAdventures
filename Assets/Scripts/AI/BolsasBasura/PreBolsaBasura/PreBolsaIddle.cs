using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PreBolsaIddle : StateMachineBehaviour
{
    Transform spawnPos;
    GameObject Player;

    public bool enSpawnPos;
    float distance;
    int arrayPos;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.name == "PreBolsaBasura1")
        {
            arrayPos = 0;
        }
        else if (animator.name == "PreBolsaBasura2")
        {
            arrayPos = 1;
        }

        spawnPos = animator.gameObject.GetComponent<EnemyBasuraAgent>().spawnPos[arrayPos];

        enSpawnPos = false;

        if (Player == null)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.LookAt(Player.transform.position);
        distance = Vector3.Distance(animator.transform.position, Player.transform.position);

        if (enSpawnPos == false)
        {
            if (Vector3.Distance(animator.transform.position, spawnPos.position) < 0.5f)
            {
                enSpawnPos = true;
            }
            else
            {
                animator.transform.position = spawnPos.position;
            }
        }
        else
        {
            if (distance <= 10f)
            {
                animator.SetBool("Alerta", true);
            }
        }
    }
}