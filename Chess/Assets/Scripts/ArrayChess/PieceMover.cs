using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceMover : MonoBehaviour
{
    public Color squareHoverColor;
    public Color squareStartColor;
    public Color pieceHoverColor;
    private Color pieceStartColor;
    public Color nearSquare;

    private Renderer rend;
    public Renderer squareRend;

    private bool isHover = false;
    private bool boardArrayLoaded = false;
    private bool squareRendLoaded = false;
    public GameObject piece;
    public GameObject chessSquare;
    public GameObject nearestSquare;
    public GameObject[] chessSquares;
    public GameObject[] pieces;
    public Vector3[] chessSquaresDistance;
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
                oldDistance = dist;
                squareRend = nearestSquare.GetComponent<Renderer>() as Renderer;
                squareStartColor = squareRend.material.color;
                squareRendLoaded = true;
            }


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


        if (isHover == true && boardArrayLoaded == true)
        {

            squareRend.material.color = squareHoverColor;

        }


        if (isHover == false && boardArrayLoaded == true && squareRendLoaded == true)
        {

            squareRend.material.color = squareStartColor;

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


