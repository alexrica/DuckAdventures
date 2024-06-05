using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatoSpawner : MonoBehaviour
{
    [SerializeField] private GameObject pato;

    //public GameManager gameManager;

    void Start()
    {
    }

    public void Spawn()
    {
        Instantiate(pato, this.transform.position, Quaternion.identity);
    }
}
