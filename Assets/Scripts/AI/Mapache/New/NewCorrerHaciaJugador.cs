using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class NewCorrerHaciaJugador : StateMachineBehaviour
{
    private NavMeshAgent Agent;
    private Transform target;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Agent = animator.gameObject.GetComponent<NavMeshAgent>();
        target = animator.gameObject.GetComponent<NewAgentMapache>().Target;

        Agent.isStopped = false;
        Agent.destination = target.position;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Agent.isStopped = false;
        Agent.destination = target.position;
        if (Vector3.Distance(target.position, animator.transform.position) <= 2)
        {
            animator.SetTrigger("Atacar");
        }
    }
}
