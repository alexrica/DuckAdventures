using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class NewMapacheCuentaAtras : StateMachineBehaviour
{
    private NavMeshAgent Agent;
    private Transform target;
    GameObject OjoDerecho;
    GameObject OjoIzquierdo;

    float tiempo = 0;
    float tiempoMaximo;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Agent = animator.gameObject.GetComponent<NavMeshAgent>();
        target = animator.gameObject.GetComponent<NewAgentMapache>().Target;
        tiempoMaximo = animator.gameObject.GetComponent<NewAgentMapache>().tiempoMaximo;
        OjoDerecho = animator.gameObject.GetComponent<NewAgentMapache>().ojoMapacheDer;
        OjoIzquierdo = animator.gameObject.GetComponent<NewAgentMapache>().ojoMapacheIzq;
        tiempo = 0;

        Agent.isStopped = true;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Vector3.Distance(target.position, animator.transform.position) > 6)
        {
            OjoDerecho.gameObject.SetActive(false);
            OjoIzquierdo.gameObject.SetActive(false);
            animator.SetTrigger("Perdido");
            tiempo = 0;
        }
        else if (Vector3.Distance(target.position, animator.transform.position) <= 6)
        {
            OjoDerecho.gameObject.SetActive(true);
            OjoIzquierdo.gameObject.SetActive(true);

            tiempo += Time.deltaTime;
            if (tiempo >= tiempoMaximo)
            {
                OjoDerecho.gameObject.SetActive(false);
                OjoIzquierdo.gameObject.SetActive(false);
                animator.SetTrigger("Agarrar");
            }
        }
    }
}