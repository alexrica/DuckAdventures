using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class PatruPerro : StateMachineBehaviour
{
    private NavMeshAgent agent;
    private Transform[] points;
    private Transform puertaLaberinto;
    private Transform target;
    private int desPoint = 0;

    // Referencia al MonoBehaviour asociado al GameObject
    private MonoBehaviour monoBehaviourComponent;

    RaycastHit hit;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.gameObject.GetComponent<NavMeshAgent>();
        target = animator.gameObject.GetComponent<AgentEnemy>().target;
        points = animator.gameObject.GetComponent<AgentEnemy>().PatrolP;
        puertaLaberinto = animator.gameObject.GetComponent<AgentEnemy>().puertaLaberinto;
        monoBehaviourComponent = animator.gameObject.GetComponent<MonoBehaviour>();
        agent.speed = 1;
        animator.SetBool("Losed", false);

        GotoNextPoint();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GotoNextPoint();
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
                        //Contacto visual con el jugador
                        animator.SetBool("PlayerInRange", true);
                    }
                    else
                    {
                        //El jugador esta oculto tras un obstaculo
                    }
                }
            }
        }
    }

    void GotoNextPoint()
    {

        if (points.Length == 0)
            return;

        agent.destination = points[desPoint].position;

        desPoint = (desPoint + 1) % points.Length;
    }
}
