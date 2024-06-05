using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NewPatrolMapache : StateMachineBehaviour
{
    // Componente NavMeshAgent para mover al objeto
    private NavMeshAgent Agent;
    private Transform Target;

    // Puntos de patrulla y variables relacionadas
    private Transform[] points;

    // Método llamado cuando el estado del Animator entra
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Obtener la referencia al componente NavMeshAgent del objeto animado
        Agent = animator.gameObject.GetComponent<NavMeshAgent>();
        Target = animator.gameObject.GetComponent<NewAgentMapache>().Target;

        // Obtener los puntos de patrulla desde el componente MapacheAgente
        points = animator.gameObject.GetComponent<NewAgentMapache>().PatrolPoints;

        // Permitir el movimiento del NavMeshAgent y desactivar el frenado automático
        Agent.isStopped = false;
        Agent.autoBraking = false;
        Agent.speed = 3.5f;

        // Ir al próximo punto de patrulla
        GotoNextPoint();
    }

    // Método llamado en cada fotograma mientras el estado está activo
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Elegir el próximo punto de destino cuando el agente se acerca al actual
        if (!Agent.pathPending && Agent.remainingDistance < 0.5f)
            GotoNextPoint();

        if (Vector3.Distance(Target.position, animator.transform.position) <= 6)
        {
            animator.SetTrigger("Detectado");
        }
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
