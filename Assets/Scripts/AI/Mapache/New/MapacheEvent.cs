using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapacheEvent : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject AgarreMapache;
    public GameObject MapacheActual;
    public PlayerController PlayerController;
    public Canvas ReaccionMapaches;

    bool grabbed = false;
    bool agarrando = false;
    public int pulsacionesEscapar = 0;
    public int pulsarEscapar;
    public float timeRemaining;
    float time;


    public void GabbedMapache(int numeroMapache)
    {
        //Busca el gameObject
        if (numeroMapache == 1)
        {
            AgarreMapache = GameObject.Find("AgarreMapache");
        }
        else if (numeroMapache == 2)
        {
            AgarreMapache = GameObject.Find("AgarreMapache2");
        }
        //Desactiva el script "PlayerController" del jugador
        GetComponent<PlayerController>().enabled = false;
        time = timeRemaining;

        if (grabbed == false)
        {
            grabbed = true;
            //Se mueve al jugador a la posición del gameObject
            transform.position = AgarreMapache.transform.position;
        }
    }

    public void TragarJugador()
    {
        Debug.Log("Matar");
    }

    public void MatrixTime(GameObject Mapache)
    {
        Mapache.GetComponent<Animator>().SetBool("Agarrando", true);
        MapacheActual = Mapache;
        agarrando = true;
        //Jugador tendrá que pulsar la S para escapar del agarre del mapache
    }

    private void Update()
    {
        if (grabbed == true)
        {
            //Se mueve al jugador a la posición del gameObject
            transform.position = AgarreMapache.transform.position;
        }

        if (agarrando == true)
        {
            ReaccionMapaches.gameObject.SetActive(true);

            //Si el control del jugador esta desactivado y pulsa la tecla S
            if (Input.GetButtonDown("S"))
            {
                //Se suma 1 a la variable que cuenta cuantas veces se ha presionado la tecla "S"
                pulsacionesEscapar += 1;
                //Se hace un Debug con el numero de pulsaciones que lleva de momento
            }

            //Si el control del jugador esta desactivado y el tiempo aún no es 0
            if (time > 0)
            {
                //Restale al tiempo total el tiempo real que esta pasando
                time -= Time.deltaTime;
            }
            else if (time <= 0)
            {
                ReaccionMapaches.gameObject.SetActive(false);
                //MapacheActual.GetComponent<Animator>().SetBool("KillPlayer", true);
                Debug.Log("Kill Player");
            }

            //Si el jugador iguala las pulsaciones necesarias para escapar
            if (pulsacionesEscapar >= pulsarEscapar && timeRemaining > 0)
            {
                //Se reiniciara el contador de pulsaciones
                pulsacionesEscapar = 0;
                //Se desactiva el canvas que muestra la tecla de escape
                ReaccionMapaches.gameObject.SetActive(false);

                grabbed = false;
                MapacheActual.GetComponent<Animator>().SetBool("Agarrando", false);
                MapacheActual.GetComponent<Animator>().SetTrigger("JugadorEscapado");

                //Se le devuelve al jugador el script que controla su movimiento
                PlayerController.enabled = true;
                agarrando = false;

            }
        }
    }
}