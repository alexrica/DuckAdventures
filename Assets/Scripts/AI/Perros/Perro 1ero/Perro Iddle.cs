using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.AI;

public class PerroIddle : StateMachineBehaviour
{
    private NavMeshAgent agent;
    private Transform target;
    Vector3 lastSeenPos;
    RaycastHit hit;

    int random;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.gameObject.GetComponent<NavMeshAgent>();
        target = animator.gameObject.GetComponent<AgentEnemy>().target;
        agent.speed = 2;

        animator.SetBool("Iddle 2", false);
        animator.SetBool("Pivot", false);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Vector3.Distance(animator.transform.position, target.position) < 10)
        {
            Vector3 vectorJE = target.transform.position - animator.transform.position;
            float angle = Vector3.Angle(vectorJE, animator.transform.forward);
            if (angle <= 45f)
            {
                Ray ray = new Ray(animator.transform.position, vectorJE);
                Debug.DrawRay(ray.origin + Vector3.up * 0.3f, ray.direction * 8, Color.white);
                if (Physics.Raycast(ray.origin + Vector3.up * 0.3f, ray.direction, out hit, 8))
                {
                    if (hit.collider.tag == "Player")
                    {
                        //Contacto visual con el jugador
                        lastSeenPos = target.transform.position;
                        GameManager.Instance.PlayerDetected(lastSeenPos);
                    }
                    else
                    {
                        //El jugador esta oculto tras un obstaculo
                    }
                }
            }
        }

        random = Random.Range(1, 1001);
        if(random == 1001)
        {
            animator.SetBool("Iddle 2", true);
        }
    }
}
