using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Quieto : StateMachineBehaviour
{

    // Componente NavMeshAgent para mover al objeto
    NavMeshAgent Agent;

    // Parámetros de comportamiento del "Mapache"
    public float distanciaDeVision = 10f;
    public float tiempoEspera = 5f;
    public GameObject ojos;

    // Tiempo transcurrido en el estado actual
    public float tiempoTranscurrido = 0f;

    // Método llamado cuando el estado del Animator entra
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Obtener la referencia al componente NavMeshAgent del objeto animado
        Agent = animator.GetComponent<NavMeshAgent>();

        // Obtener la referencia a los ojos del "Mapache" desde el componente MapacheAgente
        ojos = animator.gameObject.GetComponent<MapacheAgente>().ojosMapache;

        // Activar las luces de los ojos
        ActivarLuces(animator);

        // Reiniciar el tiempo transcurrido
        tiempoTranscurrido = 0f;

        // Detener el movimiento del NavMeshAgent
        Agent.isStopped = true;
    }

    // Método llamado en cada fotograma mientras el estado está activo
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Incrementar el tiempo transcurrido
        tiempoTranscurrido += Time.deltaTime;

        // Si ha pasado el tiempo de espera, permitir que el Mapache persiga al jugador
        if (tiempoTranscurrido >= tiempoEspera)
        {
            // Activar el movimiento del NavMeshAgent
            Agent.isStopped = false;

            // Transición al estado de persecución ("Persiguiendo")
            animator.SetBool("Persiguiendo", true);
        }
    }

    // Método para activar las luces de los ojos del "Mapache"
    void ActivarLuces(Animator animator)
    {
        ojos.gameObject.SetActive(true);
    }
}





