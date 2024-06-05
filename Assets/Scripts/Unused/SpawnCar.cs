using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCar : MonoBehaviour
{
    public GameObject[] Coches;
    public GameObject[] spawnCar;

    //public bool gira = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Spawn()
    {

        while (true)
        {
            int azar2 = Random.Range(0, 2);
            /*
            if (azar2 >= 2)
            {
                gira = false;
            }
            */
            int azar = Random.Range(1, 16);

            if (Time.timeSinceLevelLoad > 6)
            {
                if (azar < 4 && azar > 0)
                {
                    Instantiate(Coches[0], spawnCar[azar2].transform.position, Quaternion.identity);
                   

                }
                else if (azar > 3 && azar < 7)
                {
                    Instantiate(Coches[1], spawnCar[azar2].transform.position, Quaternion.identity);
                }
                else if (azar > 6 && azar < 10)
                {
                    Instantiate(Coches[2], spawnCar[azar2].transform.position, Quaternion.identity);
                }

                else if (azar > 9)
                {
                    Instantiate(Coches[3], spawnCar[azar2].transform.position, Quaternion.identity);
                }

            }
            //gira = false;
            yield return new WaitForSeconds(Random.Range(1, 5));

        }





    }
}
