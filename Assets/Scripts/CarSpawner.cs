using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    [SerializeField] private GameObject car;

    public GameManager gameManager;

    [Tooltip("In Seconds")]
    [SerializeField] private int minCarSpawnTime;
    [Tooltip("In Seconds")]
    [SerializeField] private int maxCarSpawnTime;

    void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            if (gameManager.familyCrossing == false)
            {
                Instantiate(car, this.transform.position, Quaternion.identity);
            }

            yield return new WaitForSeconds(Random.Range(minCarSpawnTime, maxCarSpawnTime));
        }
    }
}
