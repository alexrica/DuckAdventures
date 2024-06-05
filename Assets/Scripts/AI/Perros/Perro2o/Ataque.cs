using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Ataque : StateMachineBehaviour
{
    private NavMeshAgent agent;
    private Transform target;
    GameObject Player;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.gameObject.GetComponent<NavMeshAgent>();
        target = animator.gameObject.GetComponent<AgentEnemy>().target;
        agent.speed = 4;

        if (Player == null)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.destination = target.position;

        if(Vector3.Distance(animator.transform.position, target.position) > 0.5f)
        {
            agent.isStopped = true;
            Player.SendMessage("GrabbedPerro2", SendMessageOptions.RequireReceiver);
        }
    }
}
