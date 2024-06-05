using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaForQuack : MonoBehaviour
{
    public GameManager gameManager;
    public void DeletePlayerController()
    {
        GetComponent<PlayerController>().enabled = false;
        //cameraScript.CamaracambioPreBolsa(2, enemigoActual);
        gameManager.CallEndingDucks();
    }
}
