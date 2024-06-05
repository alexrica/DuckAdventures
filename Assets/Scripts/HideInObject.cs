using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideInObject : MonoBehaviour
{
    Vector3 lastPos;
    bool hided = false;
    int contador = 0;
    int entrando = 0;
    int saliendo = 0;
    bool cooldown = false;
    public float tiempo = 0;

    public void HideBush(GameObject Bush)
    {
        if (hided == false && cooldown == false)
        {
            entrando += 1;
            GetComponent<PlayerController>().enabled = false;
            lastPos = this.transform.position;
            this.transform.position = Bush.transform.position;
            hided = true;
            cooldown = true;
        }
    }

    public void OutOfBush()
    {
        if (hided == true && entrando > saliendo)
        {
            saliendo += 1;
            this.transform.position = lastPos;
            hided = false;
            contador = 0;
            GetComponent<PlayerController>().enabled = true;
        }
    }
    private void Update()
    {
        if (hided == true && Input.GetButtonDown("R"))
        {
            contador += 1;

            if (contador >= 2)
            {
                OutOfBush();
            }
        }

        if (entrando > 0)
        {
            if(entrando == saliendo)
            {
                if (tiempo >= 1 && cooldown == true)
                {
                    cooldown = false;
                    tiempo = 0;
                }
                else if (tiempo < 1 && cooldown == true)
                {
                    tiempo += Time.deltaTime;
                }
            }
        }
    }
}
