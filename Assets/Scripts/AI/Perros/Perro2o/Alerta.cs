using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Alerta : StateMachineBehaviour
{
    private NavMeshAgent agent;
    private Transform target;
    RaycastHit hit;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.gameObject.GetComponent<NavMeshAgent>();
        target = animator.gameObject.GetComponent<AgentEnemy>().target;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.transform.LookAt(target);
        Ray ray = new Ray(animator.transform.position, animator.transform.forward);
        Debug.DrawRay(ray.origin + Vector3.up * 0.3f, ray.direction * 2, Color.white);
        if (Physics.Raycast(ray.origin + Vector3.up * 0.3f, ray.direction, out hit, 2) && (hit.collider.tag == "Player"))
        {
            animator.SetTrigger("Ataque");
        }
        if (Vector3.Distance(animator.transform.position, target.position) > 10f)
        {
            animator.SetTrigger("Perdido");
        }
    }
}
