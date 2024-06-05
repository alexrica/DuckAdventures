using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeTransparentMaterials : MonoBehaviour
{
    public Material myMaterial;

    [Range(0f, 1f)]
    public float alpha = 0f;

    bool fadeIn = false;
    bool fadeOut = false;   

    // Start is called before the first frame update
    void Start()
    {
        alpha = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        myMaterial.color = new Color (myMaterial.color.r, myMaterial.color.g, myMaterial.color.b, alpha);
        if (fadeIn)
        {
            if (alpha >= 1f)
            {
                fadeIn = false;
            }
            else
            {
                alpha += 0.4f * Time.deltaTime;
            }
        }
        if(fadeOut)
        {
            if (alpha <= 0)
            {
                fadeOut = false;
            }
            else
            {
                alpha -= 0.4f * Time.deltaTime;
            }
        }
    }

    public void FadeIn()
    {
        if (fadeOut)
        {
            fadeOut = false;
        }
        fadeIn = true;
    }

    public void FadeOut()
    {
        if (fadeIn)
        {
            fadeIn = false;
        }
        fadeOut = true;
    }
}
