using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcPatrol : StateMachineBehaviour
{
    private NavMeshAgent Agent;
    private Transform[] points;
    private int destPoint = 0;
    int HogueraValor = 8;


    bool Hoguera()
    {


        NavMeshHit hit;
        if (NavMesh.SamplePosition(Agent.transform.position, out hit, 0.5f, NavMesh.AllAreas))   //Segun la posicion del npc, detecta en el radio de 0.5 si se encuentra encima del are de la hoguera usando el mask.
        {
            if (hit.mask == HogueraValor) //Usamos la variable int HogueraValor para usar la layer de la hoguera que al estar en la 3a posicion, seria el numero 8 siendo 2^3
            {
                return true;
            }
        }

        return false;
    }

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Agent = animator.gameObject.GetComponent<NavMeshAgent>();
        points = animator.gameObject.GetComponent<Agent>().PatrolPoints;
        Agent.isStopped = false;
        Agent.autoBraking = false;
        
        GotoNextPoint();
    }
   
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    
        //Si el npc toca la hoguera, Le paramos y va a Usar
        if (Hoguera()) 
        {
            GameManager.Instance.isHome = false;
            animator.SetTrigger("Usar");
        }
            
           // Choose the next destination point when the agent gets close to the current one.
            if (!Agent.pathPending && Agent.remainingDistance < 0.5f)

                GotoNextPoint();
    }
    void GotoNextPoint()
    {
        // Returns if no points have been set up
        if (points.Length == 0)
            return;
        
        // Set the agent to go to the currently selected destination.
        Agent.destination = points[destPoint].position;
        
        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % points.Length;
    }

}