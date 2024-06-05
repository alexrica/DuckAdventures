using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    public CharacterMovement CharacterMovement;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Suelo")
        {
            CharacterMovement.jump = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Suelo")
        {
            CharacterMovement.jump = false;
        }
    }
}
