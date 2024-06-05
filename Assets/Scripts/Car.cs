using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Car : MonoBehaviour
{
    [Tooltip("Only -1 or 1. This is *not* the speed.")]
    [SerializeField] private int sentido;
   private Vector3 dir;
   [SerializeField] private int velo;

    // Start is called before the first frame update
    void Start()
    {
        dir = new Vector3 (0, 0, sentido);
        //velo = Random.Range(2, 10);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(dir * velo * Time.deltaTime);

        if(transform.position.z < -55)
        {
            Destroy(this.gameObject);
        }
    }
}
