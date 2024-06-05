using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Attack : StateMachineBehaviour
{
    NavMeshAgent agent;
    private Transform target;
    float time = 0f;
    public float maxTimeUntilLose = 3f;

    RaycastHit hit;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.gameObject.GetComponent<NavMeshAgent>();
        target = animator.gameObject.GetComponent<AgentEnemy>().target;
        animator.SetBool("PlayerInRange", false);
        agent.destination = target.position;
        time = 0f;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.destination = target.position;

        Vector3 vectorJE = target.transform.position - animator.transform.position;
        float angle = Vector3.Angle(vectorJE, animator.transform.forward);
        if (angle <= 45f)
        {
            Ray ray = new Ray(animator.transform.position, vectorJE);
            Debug.DrawRay(ray.origin + Vector3.up * 0.3f, ray.direction * 5, Color.white);
            if (Physics.Raycast(ray.origin + Vector3.up * 0.3f, ray.direction, out hit, 5))
            {
                if (hit.collider.tag == "Player" && Vector3.Distance(animator.transform.position, target.transform.position) < 1)
                {
                    animator.SetBool("Bite", true);
                }
                else
                {
                    if (time >= 3f)
                    {
                        animator.SetBool("Losed", true);
                    }
                    else
                    {
                        time += Time.deltaTime;
                    }
                }
            }
        }
    }
}
