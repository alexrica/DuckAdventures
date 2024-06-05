using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BatAttack : StateMachineBehaviour
{
    Transform target;
    float distancePlayer;
    int faseAttack;
    public Vector3 getBack;
    Vector3 getBackOg;
    public Vector3 throwAttack;
    Vector3 throwAttackOg;
    public Vector3 backToStartingPos;
    Vector3 backToStartingPosOg;
    float velocity;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        target = animator.gameObject.GetComponent<AgentBat>().target;
        velocity = animator.gameObject.GetComponent<AgentBat>().velocity;
        distancePlayer = (target.position.z - animator.transform.position.z);
        faseAttack = 1;
        getBackOg = getBack;
        throwAttackOg = throwAttack;
        backToStartingPosOg = backToStartingPos;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        switch (faseAttack)
        {
            case 1:
                PreparaAttack(animator, stateInfo, layerIndex);
                break;
            case 2:
                LanzarseAttack(animator, stateInfo, layerIndex);
                break;
            case 3:
                GetBackToPose(animator, stateInfo, layerIndex);
                break;
            case 4:
                EndingAttack(animator, stateInfo, layerIndex);
                break;
        }
    }

    public void PreparaAttack(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.position = animator.transform.position + (getBack * velocity * Time.deltaTime);
        if (animator.transform.position.y >= 6)
        {
            getBack = new Vector3(getBack.x, getBack.y, getBack.z);
        }
        if (Vector3.Distance(animator.transform.position, target.position) >= 10)
        {
            faseAttack += 1;
        }
    }

    public void LanzarseAttack(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.position = animator.transform.position + (throwAttack * velocity * Time.deltaTime);
        if (animator.transform.position.y <= 2)
        {
            throwAttack = new Vector3(throwAttack.x, 2, throwAttack.z);
        }
        if (Vector3.Distance(animator.transform.position, target.position) >= 20)
        {
            faseAttack += 1;
        }
    }

    public void GetBackToPose(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.position = animator.transform.position + (backToStartingPos * velocity * Time.deltaTime);
        if (animator.transform.position.y >= 4)
        {
            backToStartingPos = new Vector3(backToStartingPos.x, 0, backToStartingPos.z);
        }
        if ((Vector3.Distance(animator.transform.position, target.position) >= distancePlayer) && (target.position.z > animator.transform.position.z))
        {
            animator.transform.position = new Vector3(animator.transform.position.x, animator.transform.position.y, target.position.z - distancePlayer);
            backToStartingPos = new Vector3(backToStartingPos.x, 0, 40);
            faseAttack += 1;
        }
    }

    public void EndingAttack(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        getBack = getBackOg;
        throwAttack = throwAttackOg;
        backToStartingPos = backToStartingPosOg;
        animator.SetBool("Iddle", true);
    }
}