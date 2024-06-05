using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BolsaBasuraEvent : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject MainCamera;
    public CameraFollow cameraScript;
    public GameObject enemigoActual;
    public GameObject[] bolsaBasura;
    public GameObject[] PrebolsaBasura;
    public GameObject[] BocaBolsaBasura;
    public GameObject[] BocaPrebolsaBolsaBasura;
    public PlayerController PlayerController;
    public Canvas ReaccionBolsaBasura;

    public int pulsacionesEscapar = 0;
    public int pulsarEscapar;
    int arrayPos;
    float timeRemaining;

    bool noPlayerController = false;
    bool escapado = false;

    bool preBolsa = true;

    private void Start()
    {
        cameraScript = MainCamera.GetComponent<CameraFollow>();
    }
    public void Grabbed(int bolsaPos)
    {
        arrayPos = bolsaPos;
        preBolsa = false;
        enemigoActual = PrebolsaBasura[bolsaPos];

        //Busca el gameObject con el tag "BocaBolsaBasura"
        BocaBolsaBasura[arrayPos] = GameObject.FindGameObjectWithTag("BocaBolsaBasura");
        //Desactiva el script "PlayerController" del jugador
        GetComponent<PlayerController>().enabled = false;
        //Pasa a true el booleano que indica que el control del jugador ha sido desactivado
        noPlayerController = true;
        //Se mueve al jugador a la posición del gameObject con el tag "BocaBolsaBasura"
        transform.position = BocaBolsaBasura[arrayPos].transform.position;

        cameraScript.CamaracambioPreBolsa(2, enemigoActual);

        //Se activa el Canvas que mostrará la tecla que habrá que presionar para escapar
        ReaccionBolsaBasura.gameObject.SetActive(true);
        //Si no ha escapado antes
        if (escapado == false)
        {
            //El tiempo que tendrá será 5
            timeRemaining = 5;
        }
        //Si ha escapado antes
        else
        {
            //El tiempo ya establecido antes (5 si es la segunda vez, 3 si es la tercera...) Será reducido en 2
            timeRemaining = timeRemaining - 2;
            escapado = false;
        }
    }

    public void GrabbedByPreBolsa(int bolsaPos)
    {
        arrayPos = bolsaPos;
        preBolsa = true;
        enemigoActual = PrebolsaBasura[bolsaPos];

        PrebolsaBasura[arrayPos].GetComponent<Animator>().SetBool("Bite", false);
        //Busca el gameObject con el tag "BocaBolsaBasura"
        BocaPrebolsaBolsaBasura[arrayPos] = GameObject.Find("PuntoComerPato1");
        //Desactiva el script "PlayerController" del jugador
        GetComponent<PlayerController>().enabled = false;
        //Pasa a true el booleano que indica que el control del jugador ha sido desactivado
        noPlayerController = true;
        //Se mueve al jugador a la posición del gameObject con el tag "BocaBolsaBasura"
        transform.position = BocaPrebolsaBolsaBasura[arrayPos].transform.position;

        cameraScript.CamaracambioPreBolsa(2, enemigoActual);

        //Se activa el Canvas que mostrará la tecla que habrá que presionar para escapar
        ReaccionBolsaBasura.gameObject.SetActive(true);
        //Si no ha escapado antes
        if (escapado == false)
        {
            //El tiempo que tendrá será 5
            timeRemaining = 5;
        }
        //Si ha escapado antes
        else
        {
            //El tiempo ya establecido antes (5 si es la segunda vez, 3 si es la tercera...) Será reducido en 2
            timeRemaining = timeRemaining - 2;
            escapado = false;
        }
    }

    private void Update()
    {
        //Si el control del jugador esta desactivado y pulsa la tecla S
        if (noPlayerController == true && Input.GetButtonDown("S"))
        {
            //Se suma 1 a la variable que cuenta cuantas veces se ha presionado la tecla "S"
            pulsacionesEscapar += 1;
            //Se hace un Debug con el numero de pulsaciones que lleva de momento
        }

        //Si el control del jugador esta desactivado y el tiempo aún no es 0
        if (noPlayerController == true && timeRemaining > 0)
        {
            transform.position = BocaPrebolsaBolsaBasura[arrayPos].transform.position;
            //Restale al tiempo total el tiempo real que esta pasando
            timeRemaining -= Time.deltaTime;
        }
        else if (noPlayerController == true && timeRemaining <= 0)
        {
            transform.position = BocaPrebolsaBolsaBasura[arrayPos].transform.position;
            if (preBolsa == true)
            {
                ReaccionBolsaBasura.gameObject.SetActive(false);
                PrebolsaBasura[arrayPos].GetComponent<Animator>().SetBool("KillPlayer", true);
                //cameraScript.CamaracambioPreBolsa(1, enemigoActual);
            }
            else
            {
                ReaccionBolsaBasura.gameObject.SetActive(false);
                bolsaBasura[arrayPos].GetComponent<Animator>().SetBool("KillPlayer", true);
            }
            //Matar al pato
        }

        //Si el jugador iguala las pulsaciones necesarias para escapar
        if (pulsacionesEscapar >= pulsarEscapar && timeRemaining > 0)
        {
            //Se reiniciara el contador de pulsaciones
            pulsacionesEscapar = 0;
            //Se desactiva el canvas que muestra la tecla de escape
            ReaccionBolsaBasura.gameObject.SetActive(false);

            if (preBolsa == true)
            {
                PrebolsaBasura[arrayPos].GetComponent<Animator>().SetBool("Escaped", true);
                cameraScript.CamaracambioPreBolsa(1, enemigoActual);
                transform.position = BocaPrebolsaBolsaBasura[arrayPos].transform.position;
            }
            else
            {
                bolsaBasura[arrayPos].GetComponent<Animator>().SetBool("Escaped", true);
                cameraScript.CamaracambioPreBolsa(1, enemigoActual);
                transform.position = BocaBolsaBasura[arrayPos].transform.position;
            }
            //Se le devuelve al jugador el script que controla su movimiento
            PlayerController.enabled = true;
            //Se indica mediante el booleano que el scrit esta activo
            noPlayerController = false;
            //El booleano que indica que ha escapado se pondrá en true, esto permitirá que si vuelve a ser atrapado el tiempo se reduzca
            escapado = true;
        }
    }
}
