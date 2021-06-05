using System;
using UnityEngine;

public class PieceMover : MonoBehaviour
{
    public Color squareHoverColor;
    public Color squareStartColor;
    public Color pieceStartColor;
    public Color pieceClickedColor;
    public Color moveHoverColor;
    public Color moveStartColor;
    public Color cacheColor;
    public Color firstSquareStartColor;


    Ray ray;
    RaycastHit hit;
    private string mouseClickPosition;
    private string rayHitStringChild;
    private GameObject rayHitGameObject;


    private Renderer pieceRend;
    private Renderer pieceSquareRend;
    private Renderer moveSquareRend;
    private Renderer parentSquareRend;
    private Renderer firstPieceSquareRend;

    private bool isColourUpdated = false;
    private bool isShowingMoveSquare = false;
    private bool isClickedOnMoveSquare = false;
    private bool isMoveSquareRendered = false;

    private bool isChessPieceClicked = false;
    private bool isMoveSquareClicked = false;
    private bool isFirstSquareRendererLoaded = false;

    public GameObject testCameraBody;
    public GameObject piece;
    public GameObject chessSquare;
    public GameObject nearestSquare;
    public GameObject[] chessSquares;
    public GameObject[] pieces;
    public GameObject moveSquareParent;
    public GameObject moveSquare;
    public GameObject cachedSquareObject;
    public GameObject firstNearestSquare;
    




    private string[] moveSquareName;
    public string nearestSquareParent;
    public string firstNearestSquareParent;

    public int[] moveSquareCoords;
    public int allowedMovement;

    private Vector3[] chessSquaresDistance;

    private float targetDistance = 1f;

    void Start()
    {
        pieceRend = GetComponent<Renderer>();
        pieceStartColor = pieceRend.material.color;
        chessSquares = GameObject.FindGameObjectsWithTag("square"); // Populates the chessboard array of all square positions 
    }

    void Update()
    {

        if (!isFirstSquareRendererLoaded) firstUpdateNearestSquareCoords();  // firstSquareRenderer bool and function to be replaced by a duplicate chessSquares and/or array for start colours of all squares.

        //cacheSquareData(); TODO: Will be info on previous square, for now will be default colour

        if (Input.GetMouseButtonDown(0))// All permutations of mouse clicks go here
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hit);
            mouseClickPosition = hit.transform.parent.name;

            updateNearestSquareCoords();
            updateMovementSquare();
            Debug.Log(isMoveSquareRendered);

            isChessPieceClicked = mouseClickPosition == piece.transform.name;
            isMoveSquareClicked = mouseClickPosition == moveSquare.transform.parent.name;

            if (isChessPieceClicked) pieceRend.material.color = pieceClickedColor;
            if (!isChessPieceClicked) pieceRend.material.color = pieceStartColor;

            if (!isChessPieceClicked && isShowingMoveSquare && !isMoveSquareClicked)
            {
                pieceRend.material.color = pieceStartColor;
                pieceSquareRend.material.color = firstSquareStartColor;
                moveSquareRend.material.color = moveStartColor;
                isMoveSquareRendered = false;
            }

            if (!isShowingMoveSquare && isChessPieceClicked) // Shows square where the piece can move to when piece is clicked
            {
                pieceSquareRend.material.color = squareHoverColor;
                moveSquareRend.material.color = moveHoverColor;
                isShowingMoveSquare = true;
            }

            if (isShowingMoveSquare && isMoveSquareClicked) // Move piece to the move square on 2nd click
            {
                piece.transform.position = hit.transform.position;
                pieceSquareRend.material.color = firstSquareStartColor;
                moveSquareRend.material.color = moveStartColor;
                isFirstSquareRendererLoaded = false;
                isShowingMoveSquare = false;
                isMoveSquareRendered = false;
                
            }

        }
    }

    void turnUpdateParentSquare()
    {
        
    }


    void firstUpdateNearestSquareCoords() // Populates each pieces nearest square with the renderer component. Useful for landmarking or reference available positions.
    {
        foreach (GameObject _targetSquare in chessSquares)
        {
            float dist = Vector3.Distance(this.gameObject.transform.position, _targetSquare.transform.position);

            if (dist < targetDistance)
            {
                firstNearestSquare = _targetSquare;
                firstNearestSquareParent = _targetSquare.gameObject.transform.parent.name;

                firstPieceSquareRend = firstNearestSquare.GetComponent<Renderer>() as Renderer;
                firstSquareStartColor = firstPieceSquareRend.material.color;
            }
        }
        isFirstSquareRendererLoaded = true;
    }


    void updateNearestSquareCoords() // Populates each pieces nearest square with the renderer component. Useful for landmarking or reference available positions.
    {
        foreach (GameObject _targetSquare in chessSquares)
        {
            float dist = Vector3.Distance(this.gameObject.transform.position, _targetSquare.transform.position);

            if (dist < targetDistance)
            {
                nearestSquare = _targetSquare;
                nearestSquareParent = _targetSquare.gameObject.transform.parent.name;

                pieceSquareRend = nearestSquare.GetComponent<Renderer>() as Renderer;
                squareStartColor = pieceSquareRend.material.color;
            }
        }
    }


    void updateMovementSquare()
    {
    
        if (isMoveSquareRendered == false)
        {
            if (this.gameObject.tag == "pawn")
            {
                allowedMovement = 1; // Temp movement of pawn, for now we'll just do moving forward 1 square

                // Conversion of array formatted coordinates string to allow for adding in allowed movement so that piece can move there
                moveSquareName = nearestSquareParent.Split(char.Parse(","));
                moveSquareCoords = Array.ConvertAll(moveSquareName, s => int.Parse(s));
                moveSquareCoords[1] += allowedMovement;
                moveSquareParent = GameObject.Find(string.Join(",", moveSquareCoords[0], moveSquareCoords[1]));

                moveSquare = moveSquareParent.transform.GetChild(0).gameObject; // Square that piece can move to
                moveSquareRend = moveSquare.GetComponent<Renderer>() as Renderer; // Render movement square
                moveStartColor = moveSquareRend.material.color; // Change colour of movement square


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
            isMoveSquareRendered = true;
        }
    }

}


