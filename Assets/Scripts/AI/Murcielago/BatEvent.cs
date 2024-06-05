using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatEvent : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject BatBoca;
    public PlayerController PlayerController;

    bool grabbed = false;
    float time = 0f;
    float maxTime = 3f;

    public void BatGrabbed()
    {
        //Busca el gameObject
        if (BatBoca == null)
        {
            BatBoca = GameObject.Find("BocaMurcielago");
        }
        //Desactiva el script "PlayerController" del jugador
        GetComponent<PlayerController>().enabled = false;
        if (grabbed == false)
        {
            grabbed = true;
            //Se mueve al jugador a la posición del gameObject
            transform.position = BatBoca.transform.position;
        }
    }

    private void Update()
    {
        if (grabbed == true)
        {
            transform.position = BatBoca.transform.position;
            time += Time.deltaTime;
            if (time >= maxTime)
            {
                GameManager.Instance.Die();
            }
        }
    }
}
