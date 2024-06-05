using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;


public class PatrullaMapache : StateMachineBehaviour
{
    // Componente NavMeshAgent para mover al objeto
    private NavMeshAgent Agent;

    // Puntos de patrulla y variables relacionadas
    private Transform[] points;
    private Transform Target;

    // Método llamado cuando el estado del Animator entra
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Obtener la referencia al componente NavMeshAgent del objeto animado
        Agent = animator.gameObject.GetComponent<NavMeshAgent>();

        // Obtener los puntos de patrulla desde el componente MapacheAgente
        points = animator.gameObject.GetComponent<MapacheAgente>().PatrolPoints;

        // Permitir el movimiento del NavMeshAgent y desactivar el frenado automático
        Agent.isStopped = false;
        Agent.autoBraking = false;

        // Ir al próximo punto de patrulla
        GotoNextPoint();
    }

    // Método llamado en cada fotograma mientras el estado está activo
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Rayo para detectar al jugador en la dirección del Mapache
        RaycastHit hitbox;
        Debug.DrawRay(animator.transform.position, animator.transform.TransformDirection(Vector3.forward));

        // Lanzar un rayo en la dirección del Mapache
        if (Physics.Raycast(animator.transform.position + Vector3.up * 0.3f, animator.transform.TransformDirection(Vector3.forward), out hitbox, 5))
        {
            // Si el rayo golpea al jugador, cambiar el destino del Mapache hacia el jugador
            if (hitbox.collider.gameObject.tag == "Player")
            {
                Debug.Log("Encontrado");
                Target = hitbox.collider.transform;
                Agent.destination = Target.position;

                // Cambiar a un estado de espera ("Quieto")
                animator.SetBool("Quieto", true);
            }
        }

        // Elegir el próximo punto de destino cuando el agente se acerca al actual
        if (!Agent.pathPending && Agent.remainingDistance < 0.5f)
            GotoNextPoint();
    }

    // Método para ir al próximo punto de patrulla
    void GotoNextPoint()
    {
        // Retornar si no se han configurado puntos de patrulla
        if (points.Length == 0)
            return;

        // Establecer el destino del NavMeshAgent como el punto de patrulla actual
        Agent.destination = points[Random.Range(0, points.Length)].position;
    }
}
