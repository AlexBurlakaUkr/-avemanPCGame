using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaChannelLow : MonoBehaviour
{
    private SpriteRenderer sR;
    void Start()
    {
        sR = GetComponent<SpriteRenderer>();    
    }
    void Update()
    {
        if (Input.GetKeyDown("f"))
        {
            StartCoroutine("Fade");
        }
    }
    IEnumerator Fade()
    {
        for (float f = 1f; f >= 0; f -= 0.05f)
        {
            Color c = sR.material.color;
            c.a = f;
            sR.material.color = c;
            yield return new WaitForSeconds(.1f);
        }
    }
}
