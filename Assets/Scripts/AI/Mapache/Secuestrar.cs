using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Secuestrar : StateMachineBehaviour
{

    // Referencia al objeto del jugador y al componente NavMeshAgent
    GameObject Player;
    public NavMeshAgent Agent;
    

    // Transform que representa la posición de la guarida
    private Transform guarida;

    // Método llamado cuando el estado del Animator entra
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        // Obtener la referencia al componente NavMeshAgent del objeto animado
        Agent = animator.gameObject.GetComponent<NavMeshAgent>();

        // Obtener la posición de la guarida desde el componente MapacheAgente
        guarida = animator.gameObject.GetComponent<MapacheAgente>().Guarida;

        // Buscar al jugador por su etiqueta si aún no se ha hecho
        if (Player == null)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
        }

        // Enviar un mensaje al jugador indicando que ha sido "Cogido"
        
        Player.SendMessage("Cogido", SendMessageOptions.DontRequireReceiver);

        // Establecer la posición de destino del NavMeshAgent como la posición de la guarida
        Agent.destination = guarida.position;
    }

}

