using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackPlayer : StateMachineBehaviour
{
    NavMeshAgent Agent;
    GameObject Player;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        Agent = animator.gameObject.GetComponent<NavMeshAgent>();
        Agent.isStopped = true;

        if (Player == null)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
        }

        Player.SendMessage("GrabbedPerro1", SendMessageOptions.RequireReceiver);
    }
}
