using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PersuePlayer : StateMachineBehaviour
{
    private NavMeshAgent agent;
    private Transform target;
    RaycastHit hit;

    float time = 0f;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.gameObject.GetComponent<NavMeshAgent>();
        target = animator.gameObject.GetComponent<AgentEnemy>().target;
        agent.destination = target.position;
        agent.speed = 2;
        time = 0f;

        animator.SetBool("PlayerInRange", false);
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
                if (hit.collider.tag == "Player" && Vector3.Distance(animator.transform.position, target.transform.position) < 2)
                {
                    animator.SetBool("Attack", true);
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