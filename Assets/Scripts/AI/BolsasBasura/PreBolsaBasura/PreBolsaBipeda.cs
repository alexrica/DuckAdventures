using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreBolsaBipeda : StateMachineBehaviour
{
    private Vector3 scaleChange;
    GameObject Player;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        scaleChange = new Vector3(-0.05f, -0.05f, -0.05f);
        if (Player == null)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.LookAt(Player.transform.position);
        if (animator.transform.localScale.y >= 5f)
        {
            animator.transform.localScale += scaleChange;
        }
        else
        {
            GameManager.Instance.BasuraBipeda(animator.name);
        }
    }
}
