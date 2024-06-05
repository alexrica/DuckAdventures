using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PerroLaberintico : StateMachineBehaviour
{
    float time = 0f;
    public float tiempoEspera;
    Transform puertaPerro;
    private NavMeshAgent agent;

    Renderer ren;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        puertaPerro = animator.gameObject.GetComponent<AgentEnemy>().puertaLaberinto;
        agent = animator.gameObject.GetComponent<NavMeshAgent>();
        animator.SetBool("Laberinto", false);
        time = 0f;

        ren = animator.gameObject.GetComponent<Renderer>();
        ren.material.color = Color.yellow;
        agent.speed = 3f;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(time < tiempoEspera)
        {
            time += Time.deltaTime;
        }

        else if(time >= tiempoEspera)
        {
            agent.destination = puertaPerro.position;
            if (!agent.pathPending && agent.remainingDistance < 0.2f)
            {
                GameManager.Instance.PerroLaberintico(2);
            }
        }
    }
}
