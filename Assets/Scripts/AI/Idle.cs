using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.HID;

public class Idle : StateMachineBehaviour
{

    private NavMeshAgent Agent;
    private Agent agente;
    private Transform[] home;

    private float time;
    private int random;

    public float distancePlayer;
    public float limitTime;

    // Start is called before the first frame update
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Idle", false);
        time = 0f;
        Agent = animator.gameObject.GetComponent<NavMeshAgent>();
        Agent.autoBraking = false;
        Agent.isStopped = true;
        home = animator.gameObject.GetComponent<Agent>().PatrolPoints;
        agente = animator.gameObject.GetComponent<Agent>();

        //Cuando el npc no esta en casa y vuelve a este estado es necesario poner en la primera posicion del array la casa del npc
        if (GameManager.Instance.isHome == false)
        {
            Agent.isStopped = false;
            Agent.destination = home[0].position;
        }
    }

    // Update is called once per frame
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (agente.hambre <= 0)
        {
            Agent.isStopped = false;
            animator.SetTrigger("Hambre");
        }
        

        //Si ve al jugador, se activa el trigger de Player con el Raycast
        RaycastHit hit; 
        if (Physics.Raycast(animator.transform.position + Vector3.up * 0.1f, animator.transform.TransformDirection(Vector3.forward), out hit, distancePlayer))
        {
            Debug.DrawRay(animator.transform.position + Vector3.up * 0.1f, animator.transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
            if (hit.collider.tag == "Player")
            {
                animator.SetTrigger("Player");
            }
        }
        else
        {
            Debug.DrawRay(animator.transform.position + Vector3.up * 0.1f, animator.transform.TransformDirection(Vector3.forward) * distancePlayer, Color.yellow);
        }
        
        //Si pasa el tiempo, se activa el trigger de Hoguera
        time += Time.deltaTime;         
        if (time >= limitTime)           
        {
            random = Random.Range(0, 10);
            //Debug.Log("" + random);           //Comprobador de numero
            if (random == 3)
            {
                Agent.isStopped = false;
                animator.SetTrigger("Hoguera");                
            }
            time = 0f;
        }
    }
}
