using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creditos : MonoBehaviour
{
    public Animator animator;
    public GameObject fadeToBlack;
    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitForSecons(4));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreditosFinalizados()
    {
        fadeToBlack.SetActive(true);
        StartCoroutine(WaitForSecons(12));
    }

    IEnumerator WaitForSecons(float time)
    {
        yield return new WaitForSeconds(time);
        if (animator.GetBool("StartCredits") == false)
        {
            animator.SetBool("StartCredits", true);
        }
        else
        {
            gameManager.GoToScene(0);
        }
    }
}
