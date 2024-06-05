using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Saludar : StateMachineBehaviour
{

    private NavMeshAgent Agent;
    public float velocidadRot = 3f;
    public float distancePlayer;

    private Transform playerTransform;
    private Transform NpcTransform;

    // Start is called before the first frame update
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Agent = animator.gameObject.GetComponent<NavMeshAgent>();
        Agent.autoBraking = false;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        //Si el jugador no es nulo, se guarda su transform
        if(player != null )
        {
            playerTransform = player.transform;
        }
        else
        {

        }
        NpcTransform = animator.transform;
    }

    // Update is called once per frame
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if (playerTransform != null)
        {
            Vector3 direccionPlayer = playerTransform.position - NpcTransform.position;
            direccionPlayer.y = 0f;

            Quaternion rotacionHaciaJugador = Quaternion.LookRotation(direccionPlayer);
            NpcTransform.rotation = Quaternion.Slerp(NpcTransform.rotation, rotacionHaciaJugador, Time.deltaTime * velocidadRot);
        }
        //Si ve al jugador, se activa el trigger de Player con el Raycast
        RaycastHit hit;
        if (Physics.Raycast(animator.transform.position + Vector3.up * 0.1f, animator.transform.TransformDirection(Vector3.forward), out hit, distancePlayer))
        {
            Debug.DrawRay(animator.transform.position + Vector3.up * 0.1f, animator.transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
            
        }
        else
        {
            Debug.DrawRay(animator.transform.position + Vector3.up * 0.1f, animator.transform.TransformDirection(Vector3.forward) * distancePlayer, Color.yellow);
            animator.SetBool("Idle", true);
        }
    }
}



