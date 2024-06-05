using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Hambre : StateMachineBehaviour
{

    private NavMeshAgent Agent;
    private Transform comedero;
    private Agent agente;

    int Valor = 16;

    bool Comedero()
    {


        NavMeshHit hit;
        if (NavMesh.SamplePosition(Agent.transform.position, out hit, 0.5f, NavMesh.AllAreas))   //Segun la posicion del npc, detecta en el radio de 0.5 si se encuentra encima del are de la hoguera usando el mask.
        {
            if (hit.mask == Valor) //Usamos la variable int Valor para usar la layer del lugar que al estar en la 4a posicion, seria el numero 16 siendo 2^4
            {
                return true;
            }
        }

        return false;
    }


    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
    {
        Agent = animator.gameObject.GetComponent<NavMeshAgent>();
        agente = animator.gameObject.GetComponent<Agent>();

        Agent.autoBraking = false;
        Agent.isStopped = false;
        comedero = animator.gameObject.GetComponent<Agent>().Comedero;
        Agent.destination = comedero.position;
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Comedero())
        {
            GameManager.Instance.isHome = false;
            agente.hambre = 104;
            animator.SetTrigger("Usar");
        }
    }
}
