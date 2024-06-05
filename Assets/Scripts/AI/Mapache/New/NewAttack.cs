using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class NewAttack : StateMachineBehaviour
{
    NavMeshAgent Agent;
    GameObject Player;
    Transform guarida;
    bool enGuarida = false;
    public bool matrixTime = false;
    float distanciaGuarida;

    float actualSpeed;
    float actualTime;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Agent = animator.gameObject.GetComponent<NavMeshAgent>();
        guarida = animator.gameObject.GetComponent<NewAgentMapache>().Guarida;
        Agent.isStopped = true;

        if (Player == null)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
        }

        if (Agent.name == "Mapache")
        {
            Player.SendMessage("GabbedMapache", 1);
        }
        else if (Agent.name == "Mapache 2")
        {
            Player.SendMessage("GabbedMapache", 2);
        }
        Agent.isStopped = false;
        enGuarida = false;
        matrixTime = false;
        actualSpeed = Agent.speed;

        distanciaGuarida = Vector3.Distance(guarida.position, animator.transform.position);
        distanciaGuarida = (distanciaGuarida / 2) + 4;
        Agent.destination = guarida.position;
        actualTime = 0;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Agent.destination = guarida.position;
        if (!Agent.pathPending && Agent.remainingDistance <= distanciaGuarida && enGuarida == false)
        {
            if (matrixTime == false)
            {
                Debug.Log("Velocidad Lenta");
                Agent.speed = actualSpeed / 2;
                Player.SendMessage("MatrixTime", animator.gameObject);
                matrixTime = true;
            }
            else if (matrixTime == true)
            {
                if (animator.GetBool("Agarrando") == true && actualTime < 5)
                {
                    actualTime += Time.deltaTime;
                }
                else
                {
                    Agent.speed = actualSpeed;
                    actualTime = 0;
                    matrixTime = false;
                    animator.SetTrigger("JugadorEscapado");
                }
            }
        }

        if (!Agent.pathPending && Agent.remainingDistance < 0.5f && enGuarida == false && animator.GetBool("Agarrando") == true)
        {
            enGuarida = true;
            Player.SendMessage("TragarJugador", SendMessageOptions.RequireReceiver);
        }
    }
}
