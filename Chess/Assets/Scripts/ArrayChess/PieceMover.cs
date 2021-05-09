using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceMover : MonoBehaviour
{
    public Color hoverColor;
    private Renderer rend;
    private Color startColor;
    private bool isHover = false;
    public GameObject piece;
    public GameObject chessSquare;
    private GameObject nearestSquare;
    public GameObject[] chessSquares;
    public Vector3[] chessSquaresDistance;



    // This is a test commit.



    void Start()
    {
       rend = GetComponent<Renderer>();
        startColor = rend.material.color;


    }

    void Update()
    {
        if (isHover == true)
        {
            findAllSquares();
        }



    }

    void findAllSquares()
    {
        
        chessSquares = GameObject.FindGameObjectsWithTag("square");



        foreach (GameObject chessSquare in chessSquares)
        {
            Debug.Log(chessSquare.transform.position - piece.transform.position);

            Debug.DrawLine(chessSquare.transform.position, piece.transform.position, Color.red, 0f, false);
          
        }
    }


    void OnMouseOver()
    {
        rend.material.color = hoverColor;
        isHover = true;

    }

    void OnMouseExit()
    {
        rend.material.color = startColor;
        isHover = false;

    }
}


//GameObject[] objs;
//objs = GameObject.FindGameObjectsWithTag("LightUsers");
//foreach (gameObject lightuser in objs)
//{
//    lightuser.GetComponent<light>().enabled = false;
//}