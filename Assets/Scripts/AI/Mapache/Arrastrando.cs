using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Arrastrando : StateMachineBehaviour
{

    private NavMeshAgent agent; // Referencia al componente NavMeshAgent del enemigo
    private Transform Target; // Transform del objetivo del enemigo
    private float reach = 5; // Distancia m�xima a la que el enemigo puede ver o detectar al objetivo
    
    public float Dist = 2.0f; // Distancia a la que el enemigo considera que ha llegado al objetivo
    private Vector3 lastKnownPlayerPosition; // La �ltima posici�n conocida del jugador

    // OnStateEnter se llama cuando comienza una transici�n y la m�quina de estados comienza a evaluar este estado
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Obtener las referencias a los componentes NavMeshAgent y el objetivo del enemigo
        agent = animator.gameObject.GetComponent<NavMeshAgent>();
        Target = animator.gameObject.GetComponent<MapacheAgente>().Target;
    }

    // OnStateUpdate se llama en cada frame de actualizaci�n entre OnStateEnter y las devoluciones de llamada OnStateExit
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Establecer la posici�n de destino del NavMeshAgent como la posici�n del objetivo
        agent.destination = Target.position;

        RaycastHit hit;

        // Realizar un raycast desde la posici�n del personaje/enemigo hacia adelante
        if (Physics.Raycast(animator.transform.position + Vector3.up, animator.transform.TransformDirection(Vector3.forward), out hit, reach))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                // Guardar la �ltima posici�n conocida del jugador
                lastKnownPlayerPosition = hit.point;
            }
        }

        if (!agent.pathPending && agent.remainingDistance < Dist)
        {
            
            //agent.isStopped = true;

            animator.SetBool("Guarida", true);
        }
        
    }
}
