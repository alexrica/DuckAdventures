using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraFollow : MonoBehaviour
{
    Transform target;
    public float smoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;
    GameObject player;
    float smooth = 2.0F;
    float eulerAngXIddlePos;
    float eulerAngYIddlePos;
    float eulerAngZIddlePos;
    int activeScene;

    [Header("Camaras niveles")]
    public Vector3 lvl1;
    public Vector3 lvl2;
    public Vector3 lvl3;
    public Vector3 lvl4;
    public Vector3 lvl5;
    public Vector3 lvl6;

    [Header("Variables camara niveles")]

    [Header("Topes Nivel 1 Noche")]
    public float lvl1NocheAxisMaxX;

    [Header("Posicion 2 Camara Nivel 2")]
    public int tipoCamara = 1;
    public float offSetCamara2;
    Vector3 posicion2Lvl2;
    GameObject enemigoAgarre;
    bool camaraComprobada = false;

    [Header("Topes Nivel 2")]
    public float lvl2AxisMinX;

    [Header("Topes Nivel 4")]
    public float lvl4AxisMinX;
    public float lvl4AxisMaxX;
    public float lvl4AxisZ;

    [Header("Topes Nivel 6")]
    public float lvl6AxisMinZ;

    [Header("Posicion 2 Camara Nivel 5")]
    public int tipoCamaraParque = 1;
    public float offSetCamara2Parque;
    public Vector3 posicion2Lvl5;

    Scene escena;
    string nivel1 = "1. Carretera (Día)";
    string nivel1noche = "1.2 Carretera (Noche)";
    string nivel2 = "2. Nivel Calle";
    string nivel3 = "3. Alcantarillas";
    string nivel4 = "4. Cajas";
    string nivel5 = "5. Parque";
    string nivel6 = "6. Final";

    private void Awake()
    {
        FollowPlayer();
    }
    void Update()
    {
        escena = SceneManager.GetActiveScene();

        if (target == null)
            return;

        if (escena.name == nivel1)
        {
            Vector3 desiredPosition = target.position + lvl1;
            Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);

            transform.position = smoothedPosition;
            activeScene = 1;
        }
        else if (escena.name == nivel1noche)
        {
            Vector3 desiredPosition = target.position + lvl1;
            Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);

            if (smoothedPosition.x > lvl1NocheAxisMaxX)
            {
                smoothedPosition.x = lvl1NocheAxisMaxX;
            }

            transform.position = smoothedPosition;
            activeScene = 2;
        }
        else if (escena.name == nivel2)
        {
            activeScene = 3;
            if (tipoCamara == 1)
            {
                if (camaraComprobada == false)
                {
                    eulerAngXIddlePos = transform.localEulerAngles.x;
                    eulerAngYIddlePos = transform.localEulerAngles.y;
                    eulerAngZIddlePos = transform.localEulerAngles.z;
                    camaraComprobada = true;
                }

                Quaternion targetRotation = Quaternion.Euler(eulerAngXIddlePos, eulerAngYIddlePos, eulerAngZIddlePos);
                Vector3 desiredPosition = target.position + lvl2;
                Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);

                if (smoothedPosition.x < lvl2AxisMinX)
                {
                    smoothedPosition.x = lvl2AxisMinX;
                }

                transform.position = smoothedPosition;
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * smooth);
            }
            else if (tipoCamara == 2)
            {
                posicion2Lvl2 = (player.transform.position - enemigoAgarre.transform.position).normalized * offSetCamara2 + player.transform.position;

                Vector3 desiredPosition = posicion2Lvl2;
                Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);

                transform.LookAt(enemigoAgarre.transform);

                transform.position = smoothedPosition;
            }
        }
        else if (escena.name == nivel3)
        {
            activeScene = 4;
            Vector3 desiredPosition = target.position + lvl3;
            Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);

            transform.position = smoothedPosition;
        }
        else if (escena.name == nivel4)
        {
            activeScene = 5;
            Vector3 desiredPosition = target.position + lvl4;
            Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);

            if (smoothedPosition.z < lvl4AxisZ)
            {
                smoothedPosition.z = lvl4AxisZ;
            }
            if (smoothedPosition.x < lvl4AxisMinX)
            {
                smoothedPosition.x = lvl4AxisMinX;
            }
            if (smoothedPosition.x > lvl4AxisMaxX)
            {
                smoothedPosition.x = lvl4AxisMaxX;
            }

            transform.position = smoothedPosition;
        }
        else if (escena.name == nivel5)
        {
            activeScene = 6;
            if (tipoCamaraParque == 1)
            {
                Vector3 desiredPosition = target.position + lvl5;
                Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);

                transform.position = smoothedPosition;
            }
            else
            {
                Vector3 desiredPosition = target.position + posicion2Lvl5;
                Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, 2);

                transform.position = smoothedPosition;
            }
        }
        else if (escena.name == nivel6)
        {
            activeScene = 7;
            Vector3 desiredPosition = target.position + lvl6;
            Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);

            if (smoothedPosition.z < lvl6AxisMinZ)
            {
                smoothedPosition.z = lvl6AxisMinZ;
            }

            transform.position = smoothedPosition;
        }
    }

    public int CheckScene()
    {
        return activeScene;
    }

    public void FollowPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        target = player.GetComponent<Transform>();
    }
    public void FollowFamily()
    {
        GameObject bigDuck = GameObject.FindGameObjectWithTag("CameraWaypoint");
        target = bigDuck.GetComponent<Transform>();
    }
    public void CamaracambioPreBolsa(int Pos, GameObject enemigo)
    {
        tipoCamara = Pos;
        enemigoAgarre = enemigo;
    }
}