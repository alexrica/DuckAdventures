using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntradaLaberinto : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameManager.Instance.PerroLaberintico(1);
        Destroy(this.gameObject);
    }
}
