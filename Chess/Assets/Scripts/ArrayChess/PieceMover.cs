using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceMover : MonoBehaviour
{
    public Color hoverColor;
    private Renderer rend;
    private Color startColor;
    private bool isHover = false;
    private bool boardArrayLoaded = false;
    public GameObject piece;
    public GameObject chessSquare;
    private GameObject nearestSquare;
    public GameObject[] chessSquares;
    public Vector3[] chessSquaresDistance;


       


    void Start()
    {
       rend = GetComponent<Renderer>();
        startColor = rend.material.color;


    }

    void Update()
    {
        if (isHover == true && boardArrayLoaded == false)
        {
            Debug.Log("Finding All Squares");
            findAllSquares();
            boardArrayLoaded = true;
        }

        if (isHover == true && boardArrayLoaded == true)
        {
            foreach (GameObject chessSquare in chessSquares)
            {
                Debug.DrawLine(chessSquare.transform.position, piece.transform.position, Color.red, 0f, false);
            }
                
        }
        


    }

    void findAllSquares()
    {
        
        chessSquares = GameObject.FindGameObjectsWithTag("square");

        Debug.Log("Found All Squares");
        boardArrayLoaded = true;
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