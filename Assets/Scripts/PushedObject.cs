using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PushedObject : MonoBehaviour
{
    public float pushForce = 1f;
    PlayerController controller;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody rig = hit.collider.attachedRigidbody;
        controller = this.gameObject.GetComponent<PlayerController>();

        if (rig != null)
        {
            if (controller.isDragging == true)
            {
                Vector3 forceDirection = hit.gameObject.transform.position - transform.position;
                forceDirection.y = 0;
                forceDirection.Normalize();

                rig.AddForceAtPosition(forceDirection * pushForce, transform.position, ForceMode.Impulse);
            }
            
        }
    }
}
