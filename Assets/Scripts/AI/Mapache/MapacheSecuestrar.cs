using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapacheSecuestrar : MonoBehaviour
{
    // Referencias a otros objetos y variables públicas accesibles desde el editor de Unity
    public GameManager gameManager;
    public GameObject Mapache;
    public GameObject Agarre;
    public PlayerController PlayerController;
    public bool TiempoLento = false;

    // Almacena la última posición del jugador antes de ser capturado
    Vector3 lastPos;

    // Contadores y variables relacionadas con la captura y escape
    public int pulsacionesEscapar = 0;
    public int pulsarEscapar;
    float timeRemaining;

    // Variables de control de estado
    bool noPlayerController = false;
    //bool escapado = false;

    // Método llamado cuando el jugador es capturado
    public void Cogido()
    {
        // Buscar el objeto con la etiqueta "Agarre"
        Agarre = GameObject.FindGameObjectWithTag("Agarre");

        // Deshabilitar el controlador de jugador
        GetComponent<PlayerController>().enabled = false;

        // Indicar que el jugador no tiene control
        noPlayerController = true;

        // Guardar la posición actual del jugador
        lastPos = transform.position;

        // Posicionar al jugador en el lugar del "Agarre"
        transform.position = Agarre.transform.position;

        // Establecer un tiempo límite para intentar escapar
        timeRemaining = 5;
    }

    // Método llamado en cada fotograma
    private void Update()
    {
        // Verificar si el tiempo está en modo lento
        if (TiempoLento == true)
        {
            // Manejar acciones cuando el jugador no tiene control
            if (noPlayerController == true && Input.GetButtonDown("S"))
            {
                // Incrementar el contador de pulsaciones para escapar
                pulsacionesEscapar += 1;

                // Mostrar el contador en la consola
                Debug.Log(pulsacionesEscapar);
            }

            // Reducir el tiempo restante para escapar si hay control de jugador
            if (noPlayerController == true && timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            // Si se agota el tiempo, llamar al método "Die" del GameManager
            else if (noPlayerController == true && timeRemaining <= 0)
            {
                gameManager.Die();
            }

            // Verificar si se alcanzó el número necesario de pulsaciones para escapar
            if (pulsacionesEscapar >= pulsarEscapar)
            {
                // Restablecer variables y permitir que el jugador recupere el control
                pulsacionesEscapar = 0;
                PlayerController.enabled = true;
                noPlayerController = false;

                // Posicionar al jugador en la última posición antes de ser capturado
                transform.position = lastPos;

                // Indicar que el jugador ha escapado y desactivar una animación en el objeto "Mapache"
                //escapado = true;
                Mapache.GetComponent<Animator>().SetBool("Guarida", false);
            }
        }
    }

    // Método para activar el modo de tiempo lento desde fuera del script (por ejemplo, desde otro script)
    public void ActivarEspacio()
    {
        TiempoLento = true;
    }
}
