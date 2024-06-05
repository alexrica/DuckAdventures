using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Agent : MonoBehaviour
{
    public Transform[] PatrolPoints;
    public Transform Comedero;

    public float hambre;
    public float multiplicadorHambre;

    private float time = 0f;
    private float TimeHambre = 4f;

    public void Start()
    {

    }
    public void Update()
    {

        //Sistema para que vaya dando hambre al npc dependiendo del multiplicador
        time += Time.deltaTime;
        if (time >= TimeHambre)
        {
            time = 0f;
            hambre -= 2f * multiplicadorHambre;
        }
    }

}
