using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class AlertaPreBolsa : StateMachineBehaviour
{
    GameObject Player;
    GameObject MainCamera;

    int radioAtaque;
    float tiempoDevorar;
    float timeRemaining;
    float distance;

    int bolsaNumber;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        radioAtaque = animator.gameObject.GetComponent<EnemyBasuraAgent>().radioAtaque;
        tiempoDevorar = animator.gameObject.GetComponent<EnemyBasuraAgent>().tiempoDevorar;

        timeRemaining = tiempoDevorar;

        if (Player == null)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
        }
        if (MainCamera == null)
        {
            MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        switch (animator.transform.name)
        {
            case "PreBolsaBasura1":
                bolsaNumber = 0;
                break;
            case "PreBolsaBasura2":
                bolsaNumber = 1;
                break;
            case "PreBolsaBasura3":
                bolsaNumber = 2;
                break;
        }

        distance = Vector3.Distance(animator.transform.position, Player.transform.position);

        if (distance <= radioAtaque)
        {
            if (timeRemaining > 0)
            {
                animator.transform.LookAt(Player.transform.position);
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                Player.SendMessage("GrabbedByPreBolsa", bolsaNumber);
                animator.SetBool("Bite", true);
            }
        }
        else if (distance >= 10f)
        {
            animator.transform.LookAt(Player.transform.position);
            timeRemaining = tiempoDevorar;
            animator.SetBool("Alerta", false);
        }
        else
        {
            animator.transform.LookAt(Player.transform.position);
            timeRemaining = tiempoDevorar;
        }
    }
}
