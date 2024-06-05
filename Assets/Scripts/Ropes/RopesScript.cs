using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopesScript : MonoBehaviour
{
    void Awake()
    {
        GetComponent<ConfigurableJoint>().connectedBody = transform.parent.GetComponent<Rigidbody>();
    }
}
