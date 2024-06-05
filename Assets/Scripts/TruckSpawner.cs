using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckSpawner : MonoBehaviour
{
    [SerializeField] private GameObject truck;

    public GameManager gameManager;

    public void SpawnTruck()
    {
        Instantiate(truck, this.transform.position, Quaternion.identity);
    }
}
