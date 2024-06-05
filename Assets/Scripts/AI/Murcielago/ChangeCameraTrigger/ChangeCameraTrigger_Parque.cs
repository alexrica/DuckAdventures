using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraTrigger_Parque : MonoBehaviour
{
    public CameraFollow cameraScript;
    public GameObject murcielago;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Colision");
        switch (other.tag)
        {
            case "Player":
                Debug.Log("JugadorEncontrado");
                cameraScript.tipoCamaraParque = 2;
                BatSalida();
                break;
        }
    }

    void BatSalida()
    {
        murcielago.GetComponent<Animator>().SetBool("Salir", true);
    }
}