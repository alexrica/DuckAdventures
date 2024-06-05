using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class EnemyBasuraFollow : StateMachineBehaviour
{
    NavMeshAgent Agent;
    Transform Target;
    Transform spawnPos;
    int attackDistance;
    int distanciaMaxPerse = 0;
    float objetivoPerdidoTiempo;
    float time;
    bool activo;
    int arrayPos;
    bool volverSpawn = false;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Follow", false);
        animator.SetBool("Bite", false);
        animator.SetBool("Escaped", false);

        if (animator.name == "BolsaBasura1")
        {
            arrayPos = 0;
        }
        else if (animator.name == "BolsaBasura2")
        {
            arrayPos = 1;
        }

        Agent = animator.gameObject.GetComponent<NavMeshAgent>();
        Target = animator.gameObject.GetComponent<EnemyBasuraAgent>().Target;
        spawnPos = animator.gameObject.GetComponent<EnemyBasuraAgent>().spawnPos[arrayPos];
        attackDistance = animator.gameObject.GetComponent<EnemyBasuraAgent>().radioAtaque;
        distanciaMaxPerse = animator.gameObject.GetComponent<EnemyBasuraAgent>().distanciaMaxPerse;
        objetivoPerdidoTiempo = animator.gameObject.GetComponent<EnemyBasuraAgent>().objetivoPerdidoTiempo;
        Agent.destination = Target.position;
        time = 0f;
        Agent.isStopped = true;
        activo = false;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Debug.Log(Vector3.Distance(spawnPos.position, Target.position));
        animator.gameObject.GetComponent<EnemyBasuraAgent>().ultimaPosicionJugador = Target.position;

        if (activo == false)
        {
            time += Time.deltaTime;
        }

        if (time >= objetivoPerdidoTiempo)
        {
            activo = true;
            Agent.isStopped = false;
            time = 0f;
        }

        if (activo == true)
        {
            Agent.destination = Target.position;
            RaycastHit hit;

            //Si el Raycast esta lanzado y se aleja del lugar inicial donde estaba sin detectar al jugador
            if (Vector3.Distance(spawnPos.position, animator.transform.position) > distanciaMaxPerse && volverSpawn == false)
            {
                if (Vector3.Distance(spawnPos.position, Target.position) <= distanciaMaxPerse)
                {
                    Agent.destination = Target.position;
                }
                else
                {
                    activo = false;
                    volverSpawn = true;
                }
            }
            if (volverSpawn == true)
            {
                Agent.destination = spawnPos.position;

                if (!Agent.pathPending && Agent.remainingDistance < 0.5f)
                {
                    volverSpawn = false;
                    animator.SetBool("Search", true);
                }
            }
            //Si el Raycast esta lanzado, toca al jugador y esta a la distancia necesaria para atacar
            if (Physics.Raycast(animator.transform.position + Vector3.up * 0.3f, animator.transform.TransformDirection(Vector3.forward), out hit, attackDistance) && (hit.collider.tag == "Player"))
            {
                animator.SetBool("Bite", true);
                activo = false;
            }
            else
            {
                Debug.DrawRay(animator.transform.position + Vector3.up * 0.3f, animator.transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            }
        }
    }
}
