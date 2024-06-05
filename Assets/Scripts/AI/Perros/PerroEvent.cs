using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerroEvent : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject BocaPerro;
    public PlayerController PlayerController;

    bool grabbed = false;
    float time = 0f;
    float maxTime = 3f;

    public void GrabbedPerro1()
    {
        //Busca el gameObject
        BocaPerro = GameObject.Find("BocaPerro1");
        //Desactiva el script "PlayerController" del jugador
        GetComponent<PlayerController>().enabled = false;
        if (grabbed == false)
        {
            grabbed = true;
            //Se mueve al jugador a la posición del gameObject
            transform.position = BocaPerro.transform.position;
        }
    }
    
    public void GrabbedPerroLaberinto()
    {
        //Busca el gameObject
        BocaPerro = GameObject.Find("BocaPerroLaberinto");
        //Desactiva el script "PlayerController" del jugador
        GetComponent<PlayerController>().enabled = false;
        if (grabbed == false)
        {
            grabbed = true;
            //Se mueve al jugador a la posición del gameObject
            transform.position = BocaPerro.transform.position;
        }
    }

    public void GrabbedPerro2()
    {
        //Busca el gameObject
        BocaPerro = GameObject.Find("BocaPerro2");
        //Desactiva el script "PlayerController" del jugador
        GetComponent<PlayerController>().enabled = false;
        if (grabbed == false)
        {
            grabbed = true;
            //Se mueve al jugador a la posición del gameObject
            transform.position = BocaPerro.transform.position;
        }
    }

    private void Update()
    {
        if (grabbed == true)
        {
            transform.position = BocaPerro.transform.position;
            time += Time.deltaTime;
            if (time >= maxTime)
            {
                GameManager.Instance.Die();
            }
        }
    }
}
