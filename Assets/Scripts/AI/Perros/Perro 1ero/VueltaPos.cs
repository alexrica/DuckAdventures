using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class VueltaPos : StateMachineBehaviour
{
    private NavMeshAgent agent;
    private Transform target;
    private Transform pivot;
    float rotation = 0f;
    bool rotate = false;
    RaycastHit hit;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.gameObject.GetComponent<NavMeshAgent>();
        target = animator.gameObject.GetComponent<AgentEnemy>().target;
        pivot = animator.gameObject.GetComponent<AgentEnemy>().pivot;
        agent.destination = pivot.position;
        animator.SetBool("Losed", false);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!agent.pathPending && agent.remainingDistance < 0.2f)
        {
            if (rotate == false)
            {
                animator.transform.Rotate(0.0f, Time.deltaTime + 2f, 0.0f);
                rotation += Time.deltaTime + 2f;
                if (rotation >= 180)
                {
                    rotate = true;
                    animator.transform.LookAt(target.transform.position);
                }
            }
            else
            {
                animator.SetBool("Pivot", true);
            }
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