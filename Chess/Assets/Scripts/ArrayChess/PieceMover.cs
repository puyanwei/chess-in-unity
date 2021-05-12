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

    Ray ray;
    RaycastHit hit;
    private string rayHitStringParent;
    private string rayHitStringChild;
    private GameObject rayHitGameObject;


    private Renderer rend;
    private Renderer pieceSquareRend;
    private Renderer moveSquareRend;

    private bool isHover = false;
    private bool isBoardArrayLoaded = false;
    private bool isSquareRendLoaded = false;
    private bool isColourUpdated = false;
    private bool isShowingMovement = false;
    private bool isClickedOnMoveSquare = false;
    private bool isInitialSquareRendererLoaded = false;

    public GameObject testCameraBody;
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

    private float targetDistance = 0.5f;
    private float loadtimer = 1f;

    void Start()
    {
        rend = GetComponent<Renderer>();
        pieceStartColor = rend.material.color;

    }


    void Update()
    {
        if (isInitialSquareRendererLoaded == false)
        {
            getSquareRenderers();
            

        }


         // add a if to load only once per turn.


        if (!isHover && isBoardArrayLoaded && isSquareRendLoaded && isColourUpdated) // Reset colour of piece and pieceSquare.
        {

            resetColors(false);
            isColourUpdated = false;

        }

        if (!isBoardArrayLoaded) // Loads chessBoard.
        {

            findAllSquares();
            isBoardArrayLoaded = true;
        }

        if (Input.GetMouseButton(0) && isSquareRendLoaded) // Clicking piece locks colour of movesquare until another piece is clicked.
        {
           
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hit);
            rayHitStringParent = hit.transform.parent.name;
            getSquareRenderers();

            if (isShowingMovement && rayHitStringParent == moveSquare.transform.parent.name)  
            {
                
                piece.transform.position = hit.transform.position;
                //isInitialSquareRendererLoaded = true;

            }


            if (isShowingMovement)
            {

                resetColors(false);
                isShowingMovement = false;


            }

            if (!isShowingMovement && rayHitStringParent == piece.transform.name)
            {

                pieceSquareRend.material.color = squareHoverColor;
                moveSquareRend.material.color = moveHoverColor;
                isShowingMovement = true;
                isInitialSquareRendererLoaded = true;
            }
        }
    }

    void resetColors(bool updateColor)
    {
        pieceSquareRend.material.color = squareStartColor;
        moveSquareRend.material.color = moveStartColor;
        isColourUpdated = updateColor;
    }



    void getSquareRenderers() // Populates each pieces nearest square with the renderer component. Useful for landmarking or reference available positions.
    {
        foreach (GameObject _targetSquare in chessSquares)
        {
            float dist = Vector3.Distance(this.gameObject.transform.position, _targetSquare.transform.position);
            float debugsum = (targetDistance - dist);



            if (dist < Mathf.Abs(1)) // when targetDistance == 1 this statement doesnt work. when it is a local (1), it does.... no idea... float incompatible? 
            {
                nearestSquare = _targetSquare;
                nearestSquareParent = _targetSquare.gameObject.transform.parent.name;
                targetDistance = dist;
                pieceSquareRend = nearestSquare.GetComponent<Renderer>() as Renderer;
                squareStartColor = pieceSquareRend.material.color;
                moveableSquares();
                isSquareRendLoaded = true;
                Debug.Log(dist + " " + _targetSquare.gameObject.transform.parent.name);
            }

        }
        

    }


    void moveableSquares()
    {
        if (this.gameObject.tag == "pawn")
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

    void findAllSquares()
    {
        if (loadtimer > 0f)
        {

            loadtimer -= Time.deltaTime;
            findAllSquares();
        }

        chessSquares = GameObject.FindGameObjectsWithTag("square");

        isBoardArrayLoaded = true;

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


