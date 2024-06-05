using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.AI;

public class PlayerSearch : StateMachineBehaviour
{
    private NavMeshAgent agent;
    private Transform target;
    RaycastHit hit;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.gameObject.GetComponent<NavMeshAgent>();
        target = animator.gameObject.GetComponent<AgentEnemy>().target;
        agent.destination = GameManager.Instance.lastSeenPos;
        animator.SetBool("PlayerDetect", false);
        animator.SetBool("Pivot", false);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            animator.SetBool("Losed", true);
        }

        if (Vector3.Distance(animator.transform.position, target.position) < 10)
        {
            Vector3 vectorJE = target.transform.position - animator.transform.position;
            float angle = Vector3.Angle(vectorJE, animator.transform.forward);
            if (angle <= 45f)
            {
                Ray ray = new Ray(animator.transform.position, vectorJE);
                Debug.DrawRay(ray.origin + Vector3.up * 0.3f, ray.direction * 5, Color.white);
                if (Physics.Raycast(ray.origin + Vector3.up * 0.3f, ray.direction, out hit, 5))
                {
                    if (hit.collider.tag == "Player")
                    {
                        animator.SetBool("PlayerInRange", true);
                    }
                }
            }
        }
    }
}
