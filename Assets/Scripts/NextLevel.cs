using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public GameManager gameManager;

    Scene escena;
    int nivelActual;

    private void Start()
    {
        escena = SceneManager.GetActiveScene();

        switch (escena.name)
        {
            case "1. Carretera (Día)":
                nivelActual = 2;
                break;
            case "1.2 Carretera (Noche)":
                nivelActual = 3;
                break;
            case "2. Nivel Calle":
                nivelActual = 4;
                break;
            case "3. Alcantarillas":
                nivelActual = 5;
                break;
            case "4. Cajas":
                nivelActual = 6;
                break;
            case "5. Parque":
                nivelActual = 7;
                break;
            case "6. Final":
                nivelActual = 8;
                break;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Player":
                Debug.Log(nivelActual);
                gameManager.GoToScene(nivelActual);
                break;
        }
    }
}
