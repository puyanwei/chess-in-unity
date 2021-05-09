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



    void getSquareRenderers() // Populates each pieces nearest square with the renderer component. Useful for landmarking or reference available positions.
    {
        foreach (GameObject targetSquare in chessSquares)
        {

            float dist = Vector3.Distance(this.gameObject.transform.position, targetSquare.transform.position);
            if (dist < oldDistance)
            {
                nearestSquare = targetSquare;
                oldDistance = dist;
            }
            squareRend = nearestSquare.GetComponent("Renderer") as Renderer;

            //if (isHover == true && boardArrayLoaded == true) // this doesn't work as intended.
            //{
            //    Debug.DrawLine(targetSquare.transform.position, piece.transform.position, Color.red, 0f, false);
            //    squareStartColor = squareRend.material.color;
            //    squareRend.material.color = squareHoverColor;
            //}
            
        }
    }

    void Start()
    {
        rend = GetComponent<Renderer>();
        pieceStartColor = rend.material.color;
    }

    void Update()
    {
        getSquareRenderers();

        if (isHover == true && boardArrayLoaded == false)
        {

            findAllSquares();
            boardArrayLoaded = true;
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

