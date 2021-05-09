using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourHover : MonoBehaviour

{

    public Color hoverColor;
    private Renderer rend;
    private Color startColor;

    void Start()
    {

        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        Debug.Log("you hoverd over the pawn!");

    }

    void OnMouseOver()
    {
        rend.material.color = hoverColor;
        Debug.Log("you hoverd over the pawn!");
    }

    void OnMouseExit()
    {
        rend.material.color = startColor;
        Debug.Log("you left over the pawn!");
    }
}
