using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentBat : MonoBehaviour
{
    public Transform target;
    public Vector3 direction;
    public float velocity;
    public float cooldownAttack;
    public bool cooldowntrue = true;
    public BatEvent batEventScript;

    public void DoWaitTimeCoroutine()
    {
        cooldowntrue = true;
        StartCoroutine(WaitForSeconds(cooldownAttack));
    }
    IEnumerator WaitForSeconds(float cooldownAttack)
    {
        yield return new WaitForSeconds(cooldownAttack);
        cooldowntrue = false;
        yield return new WaitForSeconds(1f);
        cooldowntrue = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Atrapado");
        batEventScript.BatGrabbed();
    }
}
