using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PieceMover : MonoBehaviour
{
    public Color squareHoverColor;
    public Color squareStartColor;
    public Color pieceHoverColor;
    public Color pieceStartColor;
    public Color moveHoverColor;
    public Color moveStartColor;


    private Renderer rend;
    private Renderer pieceSquareRend;
    private Renderer moveSquareRend;

    private bool isHover = false;
    private bool boardArrayLoaded = false;
    private bool squareRendLoaded = false;

    public GameObject piece;
    public GameObject chessSquare;
    public GameObject nearestSquare;
    public GameObject[] chessSquares;
    public GameObject[] pieces;
    public GameObject moveSquareParent;
    public GameObject moveSquare;

    private string[] moveSquareName;
    public string nearestSquareParent;

    public int[] moveSquareCoords;
    public int allowedMovement;

    private Vector3[] chessSquaresDistance;

    private float oldDistance = 1f;
    private float loadtimer = 1f;



    void getSquareRenderers() // Populates each pieces nearest square with the renderer component. Useful for landmarking or reference available positions.
    {

        foreach (GameObject _targetSquare in chessSquares)
        {

            float dist = Vector3.Distance(piece.gameObject.transform.position, _targetSquare.transform.position);
            if (dist < oldDistance)
            {
                nearestSquare = _targetSquare;
                nearestSquareParent = _targetSquare.gameObject.transform.parent.name;
                oldDistance = dist;
                pieceSquareRend = nearestSquare.GetComponent<Renderer>() as Renderer;
                squareStartColor = pieceSquareRend.material.color;
                moveableSquares();
                squareRendLoaded = true;

                



            }


        }

    }


    void moveableSquares()
    {
        if(this.gameObject.tag == "pawn")
        {
            allowedMovement = 1;
            moveSquareName = nearestSquareParent.Split(char.Parse(","));
            moveSquareCoords = Array.ConvertAll(moveSquareName, s => int.Parse(s));
            moveSquareCoords[1] += allowedMovement;
            moveSquareParent = GameObject.Find(string.Join(",", moveSquareCoords[0], moveSquareCoords[1]));
            moveSquare = moveSquareParent.transform.GetChild(0).gameObject;
            moveSquareRend = moveSquare.GetComponent<Renderer>() as Renderer;
            moveStartColor = moveSquareRend.material.color;
            
        }
        if (this.gameObject.tag == "rook")
        {
            

        }
        if (this.gameObject.tag == "knight")
        {


        }
        if (this.gameObject.tag == "queen")
        {


        }

    }




    void Start()
    {
        rend = GetComponent<Renderer>();
        pieceStartColor = rend.material.color;
    }


    void Update()
    {


        getSquareRenderers(); // add a if to load only once per turn.


        if (isHover == true && boardArrayLoaded == true)
        {

            pieceSquareRend.material.color = squareHoverColor;
            moveSquareRend.material.color = moveHoverColor;

            // moveSquareRend.material.color = moveHoverColor;

        }


        if (isHover == false && boardArrayLoaded == true && squareRendLoaded == true)
        {

            pieceSquareRend.material.color = squareStartColor;
            moveSquareRend.material.color = moveStartColor;

            //  moveSquareRend.material.color = moveStartColor;

        }

        if (boardArrayLoaded == false)
        {

            findAllSquares();
            boardArrayLoaded = true;
            Debug.Log(chessSquares.Length + " squares loaded");
        }


    }

    void findAllSquares()
    {
        if (loadtimer > 0f)
        {

            loadtimer -= Time.deltaTime;
            findAllSquares();
        }

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


