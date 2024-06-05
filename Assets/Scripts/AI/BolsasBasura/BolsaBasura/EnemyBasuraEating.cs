using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBasuraEating : StateMachineBehaviour
{
    NavMeshAgent Agent;
    GameObject Player;

    int bolsaNumber;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Bite", false);
        animator.SetBool("Follow", false);

        Agent = animator.gameObject.GetComponent<NavMeshAgent>();
        Agent.isStopped = true;

        if (Player == null)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
        }

        switch (animator.transform.name)
        {
            case "BolsaBasura1":
                bolsaNumber = 0;
                break;
            case "BolsaBasura2":
                bolsaNumber = 1;
                break;
            case "BolsaBasura3":
                bolsaNumber = 2;
                break;
        }

        Player.SendMessage("Grabbed", bolsaNumber);
    }
}