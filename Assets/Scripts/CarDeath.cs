using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDeath : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Death")
        {
            Destroy(other.gameObject);
        }
    }
}
