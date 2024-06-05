using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Perro2oIddle : StateMachineBehaviour
{
    private NavMeshAgent agent;
    private Transform target;
    private Transform pivot;
    RaycastHit hit;

    public bool onPivot = false;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.gameObject.GetComponent<NavMeshAgent>();
        target = animator.gameObject.GetComponent<AgentEnemy>().target;
        pivot = animator.gameObject.GetComponent<AgentEnemy>().pivot;
        agent.isStopped = false;
        agent.speed = 2;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Vector3.Distance(animator.transform.position, pivot.position) > 0.5f && onPivot == false)
        {
            agent.destination = pivot.position;
        }
        else if (!agent.pathPending && agent.remainingDistance <= 0.5f)
        {
            onPivot = true;
        }

        if(onPivot == true)
        {
            agent.transform.LookAt(target.transform.position);
            Ray ray = new Ray(animator.transform.position, animator.transform.forward);
            Debug.DrawRay(ray.origin + Vector3.up * 0.3f, ray.direction * 8, Color.white);
            if (Physics.Raycast(ray.origin + Vector3.up * 0.3f, ray.direction, out hit, 8) && (hit.collider.tag == "Player"))
            {
                animator.SetTrigger("Alerta");
            }
        }
    }
}
