using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NewVueltaAGUarida : StateMachineBehaviour
{
    NavMeshAgent Agent;
    private Transform target;
    Transform guarida;

    float time = 0;
    bool enGuarida = false;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Agent = animator.gameObject.GetComponent<NavMeshAgent>();
        target = animator.gameObject.GetComponent<NewAgentMapache>().Target;
        guarida = animator.gameObject.GetComponent<NewAgentMapache>().Guarida;
        time = 0;
        enGuarida = false;

        Agent.destination = guarida.position;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Agent.destination = guarida.position;

        if (!Agent.pathPending && Agent.remainingDistance < 0.5f)
        {
            enGuarida = true;
        }

        if (enGuarida == true)
        {
            if (time >= 5)
            {
                animator.SetTrigger("Patrol");
            }
            else
            {
                time += Time.deltaTime;
            }
        }
    }
}
