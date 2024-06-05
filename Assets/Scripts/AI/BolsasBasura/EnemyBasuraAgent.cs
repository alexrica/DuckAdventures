using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasuraAgent : MonoBehaviour
{
    public Transform Target;
    public Transform[] spawnPos;
    public GameObject puntoComerPato;
    public Vector3 ultimaPosicionJugador;
    public int radioAtaque;
    public int DetectDistance;
    public int distanciaMaxPerse;
    public float tiempoDevorar;
    public float objetivoPerdidoTiempo;
    public float rotationSpeed;
}
