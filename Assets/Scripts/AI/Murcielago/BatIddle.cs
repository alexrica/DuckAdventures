using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatIddle : StateMachineBehaviour
{
    Transform target;
    public Vector3 direction;
    public bool ataque = false;
    float velocity;
    float random;
    float tiempo;
    bool realizarAtaque;
    float distancePlayerBat;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        target = animator.gameObject.GetComponent<AgentBat>().target;
        direction = animator.gameObject.GetComponent<AgentBat>().direction;
        velocity = animator.gameObject.GetComponent<AgentBat>().velocity;
        distancePlayerBat = Vector3.Distance(animator.transform.position, target.position);
        realizarAtaque = false;
        tiempo = 0;
        if (ataque == true)
        {
            animator.GetComponent<AgentBat>().DoWaitTimeCoroutine();
        }
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Iddle", false);
        BatMovimiento(animator, stateInfo, layerIndex);

        CheckAtack(animator, stateInfo, layerIndex);
    }

    public void BatMovimiento(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.transform.position.x != target.position.x)
        {
            if (animator.transform.position.x > target.position.x)
                direction.x = -10;
            else
                direction.x = 10;
        }
        else
        {
            direction.x = 0;
        }

        if(animator.transform.position.y < 1.6f)
        {
            direction.y += 1;
        }

        if (Vector3.Distance(animator.transform.position, target.position) >= distancePlayerBat)
        {
            direction.z += 20;
        }
        else
        {
            direction.z -= 20;
        }

        animator.transform.position = animator.transform.position + (direction * velocity * Time.deltaTime);
    }
    public void CheckAtack(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (ataque == false)
        {
            Cronometro();
            random = Random.Range(1, 301);
            if (random == 300 || realizarAtaque == true)
            {
                ataque = true;
                animator.SetTrigger("Atacar");
            }
        }
        else
        {
            ataque = animator.gameObject.GetComponent<AgentBat>().cooldowntrue;
        }
    }

    public void Cronometro()
    {
        if (tiempo >= 10)
        {
            realizarAtaque = true;
        }
        else
        {
            tiempo += Time.deltaTime;
        }
    }
    public void BatSalidaNivel(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        direction = new Vector3(0, 0, 0);
        animator.transform.position = animator.transform.position + (direction * velocity * Time.deltaTime);
    }
}
