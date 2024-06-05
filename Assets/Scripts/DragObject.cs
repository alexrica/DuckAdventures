using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    bool beingDragged = false;
    public Vector3 patoTop;
    private GameObject player;
    public Transform dejarObjetoPos;
    public Rigidbody Rigidbody;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Rigidbody = this.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (beingDragged == true)
        {
            transform.position = player.transform.position + patoTop;
        }
    }

    public void StartDragging()
    {
        Rigidbody.useGravity = false;
        beingDragged = true;
    }

    public void EndDragging()
    {
        beingDragged = false;
        transform.position = dejarObjetoPos.transform.position;
        Rigidbody.useGravity = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Snap")
        {
            this.transform.position = other.transform.position;
            Rigidbody.useGravity=false;
            Rigidbody.isKinematic = true;
        }
    }
}