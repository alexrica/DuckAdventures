using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBasuraSearch : StateMachineBehaviour
{
    NavMeshAgent Agent;

    float rotationSpeed;
    float rotation = 0f;
    int detectDistance;
    bool rotation90;
    bool girando;
    bool searching;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Follow", false);

        Agent = animator.gameObject.GetComponent<NavMeshAgent>();
        detectDistance = animator.gameObject.GetComponent<EnemyBasuraAgent>().DetectDistance;
        rotationSpeed = animator.gameObject.GetComponent<EnemyBasuraAgent>().rotationSpeed;

        Agent.isStopped = true;
        rotation = 0f;
        rotation90 = false;
        girando = true;
        searching = true;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        RaycastHit hit;

        if (searching == true)
        {
            Debug.DrawRay(animator.transform.position + Vector3.up * 0.3f, animator.transform.TransformDirection(Vector3.forward) * detectDistance, Color.white);

            //Si encuentra al jugador antes de dar la vuelta completa
            if ((Physics.Raycast(animator.transform.position + Vector3.up * 0.3f, animator.transform.TransformDirection(Vector3.forward), out hit, detectDistance) && (hit.collider.tag == "Player")))
            {
                animator.SetBool("Search", false);
            }

            if (girando == true)
            {
                if (rotation90 == false)
                {
                    animator.transform.Rotate(0.0f, Time.deltaTime * rotationSpeed, 0.0f);
                    rotation += Time.deltaTime * rotationSpeed;

                    if (rotation >= 90)
                    {
                        rotation90 = true;
                    }
                }
                else
                {
                    animator.transform.Rotate(0.0f, -Time.deltaTime * rotationSpeed, 0.0f);
                    rotation -= Time.deltaTime * rotationSpeed;

                    if (rotation < -90)
                    {
                        girando = false;
                    }
                }
            }
            else
            {
                animator.transform.Rotate(0.0f, Time.deltaTime * rotationSpeed, 0.0f);
                rotation += Time.deltaTime * rotationSpeed;

                if (rotation >= 0)
                {
                    searching = false;
                }
            }
        }
        else
        {
            animator.SetBool("ReturnIddle", true);
        }
    }
}
