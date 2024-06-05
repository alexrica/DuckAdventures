using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatSalida : StateMachineBehaviour
{
    float velocity;
    Vector3 direction;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        velocity = animator.gameObject.GetComponent<AgentBat>().velocity;
        direction = new Vector3(0, 40, 20);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.position = animator.transform.position + (direction * velocity * Time.deltaTime);
    }
}
