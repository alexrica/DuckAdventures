using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Usar : StateMachineBehaviour
{

    private NavMeshAgent Agent;

    private float time;
    public float limitTime;

    // Start is called before the first frame update
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Agent = animator.gameObject.GetComponent<NavMeshAgent>();
        Agent.autoBraking = false;
        Agent.isStopped = true;
        time = 0f;
    }

    // Update is called once per frame
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Si pasa el tiempo, se activa el trigger de Hoguera
        time += Time.deltaTime;
        if (time >= limitTime)
        {
            time = 0f;
            Agent.isStopped = false;
            animator.SetBool("Idle", true);

        }
    }
}

