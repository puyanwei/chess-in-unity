using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceHover : MonoBehaviour
{
    public Color hoverColor;
    private Renderer rend;
    private Color startColor;

    void Start()
    {

        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        

    }

    void OnMouseOver()
    {
        rend.material.color = hoverColor;
        
    }

    void OnMouseExit()
    {
        rend.material.color = startColor;
        
    }
}
