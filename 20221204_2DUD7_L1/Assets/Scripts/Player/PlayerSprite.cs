using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprite : MonoBehaviour
{
    private Color colorNew;
    private Color colorOld;
    private SpriteRenderer playerColor;
    private void Start()
    {
        playerColor = GetComponent<SpriteRenderer>();
        colorNew = new Color(0, 255, 255);
        colorOld = playerColor.color;
    }
    private void Update()
    {
        GetChangePlayerSkinColor();
    }
    private void GetChangePlayerSkinColor()
    {
        if (Input.GetKey(KeyCode.G)) playerColor.color = colorNew;
        else playerColor.color = colorOld;
    }
}
