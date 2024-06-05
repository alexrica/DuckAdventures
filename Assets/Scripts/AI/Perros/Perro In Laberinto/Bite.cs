using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bite : StateMachineBehaviour
{
    NavMeshAgent Agent;
    GameObject Player;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Agent = animator.gameObject.GetComponent<NavMeshAgent>();
        Agent.isStopped = true;
        animator.SetBool("Bite", false);

        if (Player == null)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
        }

        Player.SendMessage("GrabbedPerroLaberinto", SendMessageOptions.RequireReceiver);
    }
}
