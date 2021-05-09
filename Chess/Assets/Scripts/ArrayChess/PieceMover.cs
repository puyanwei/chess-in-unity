using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceMover : MonoBehaviour
{
    public Color squareHoverColor;
    private Color squareStartColor;
    public Color pieceHoverColor;
    private Color pieceStartColor;
    public Color nearSquare;

    private Renderer rend;
    public Renderer squareRend;
    
    private bool isHover = false;
    private bool boardArrayLoaded = false;
    public GameObject piece;
    public GameObject chessSquare;
    public GameObject nearestSquare;
    public GameObject[] chessSquares;
    public Vector3[] chessSquaresDistance;
    private float oldDistance = 9999;
    


    void squareColor() // Chessboard square spawner which allows for dynamic naming.
    {
        Renderer squareRend = piece.GetComponent("Renderer") as Renderer;
    }

    void Start()
    {
       rend = GetComponent<Renderer>();
        pieceStartColor = rend.material.color;      
    }

    void Update()
    {
        if (isHover == true && boardArrayLoaded == false)
        {
            //squareColor();
            //squareStartColor = squareRend.material.color;
            findAllSquares();                 
            boardArrayLoaded = true;
        }

        if (isHover == true && boardArrayLoaded == true)
        {
            foreach (GameObject targetSquare in chessSquares)
            {
                Debug.DrawLine(targetSquare.transform.position, piece.transform.position, Color.red, 0f, false);
                float dist = Vector3.Distance(this.gameObject.transform.position, targetSquare.transform.position);
                if (dist < oldDistance)
                {
                    nearestSquare = targetSquare;
                    oldDistance = dist;
                }
                //squareRend.material.color = squareHoverColor;
            }
                
        }      


    }

    void findAllSquares()
    {
        
        chessSquares = GameObject.FindGameObjectsWithTag("square");     
        boardArrayLoaded = true;
    }


    void OnMouseOver()
    {
        rend.material.color = pieceHoverColor;
        isHover = true;

    }

    void OnMouseExit()
    {
        rend.material.color = pieceStartColor;
        isHover = false;

    }
}


//GameObject[] objs;
//objs = GameObject.FindGameObjectsWithTag("LightUsers");
//foreach (gameObject lightuser in objs)
//{
//    lightuser.GetComponent<light>().enabled = false;
//}