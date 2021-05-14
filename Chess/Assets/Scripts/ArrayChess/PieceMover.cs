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
    public Color cacheColor;


    Ray ray;
    RaycastHit hit;
    private string mouseClickPosition;
    private string rayHitStringChild;
    private GameObject rayHitGameObject;


    private Renderer rend;
    private Renderer pieceSquare;
    private Renderer moveSquareRend;
    private Renderer parentSquareRend;

    private bool isHover = false;
    private bool isColourUpdated = false;
    private bool isShowingMoveSquare = false;
    private bool isClickedOnMoveSquare = false;
    // private bool isCached = false;

    public GameObject testCameraBody;
    public GameObject piece;
    public GameObject chessSquare;
    public GameObject nearestSquare;
    public GameObject[] chessSquares;
    public GameObject[] pieces;
    public GameObject moveSquareParent;
    public GameObject moveSquare;
    public GameObject cachedSquareObject;

    private string[] moveSquareName;
    public string nearestSquareParent;

    public int[] moveSquareCoords;
    public int allowedMovement;

    private Vector3[] chessSquaresDistance;

    private float targetDistance = 1f;

    void Start()
    {
        rend = GetComponent<Renderer>();
        pieceStartColor = rend.material.color;
        chessSquares = GameObject.FindGameObjectsWithTag("square"); // Populates the chessboard array of all square positions 
    }

    void Update()
    {
        //cacheSquareData(); TODO: Will be info on previous square, for now will be default colour

        if (Input.GetMouseButton(0)) // All permutations of mouse clicks go here
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hit);
            mouseClickPosition = hit.transform.parent.name;

            updateNearestSquareCoords();
            updateMovementSquare();

            var isChessPieceClicked = mouseClickPosition == piece.transform.name;
            var isMoveSquareClicked = mouseClickPosition == moveSquare.transform.parent.name;

            // isCached = true;

            if (!isShowingMoveSquare && isChessPieceClicked) // Shows square where the piece can move to when piece is clicked
            {
                pieceSquare.material.color = squareHoverColor;
                moveSquareRend.material.color = moveHoverColor;
                isShowingMoveSquare = true;
            }

            if (isShowingMoveSquare && isMoveSquareClicked && !isHover) // Move piece to the move square on 2nd click
            {
                piece.transform.position = hit.transform.position;
                pieceSquare.material.color = squareStartColor;
                moveSquareRend.material.color = moveStartColor;
                isShowingMoveSquare = false;
            }

            // if (isShowingMoveSquare) // TODO: Clicking outside of piece or move square
            // {
            //     pieceSquare.material.color = squareStartColor;
            //     moveSquareRend.material.color = moveStartColor;
            //     isShowingMoveSquare = false;
            // }

        }
    }


    // to do - write script to cache colour in array spawner per square. use getcomponent pull the cache color from square.

    //void cacheSquareData()
    //{
    //    cachedSquareObject = GameObject.Find(nearestSquareParent);
    //    parentSquareRend = cachedSquareObject.GetComponentInChildren<Renderer>();

    //    Color blk = new Color(0, 0, 0, 1);
    //    Color wht = new Color(255, 255, 255, 255);


    //    if (isCached && cacheColor != blk || cacheColor != wht) // n piece selection or cache
    //    {
    //        cacheColor = parentSquareRend.material.color;
    //        Debug.Log("second selection " + cacheColor);
    //        Debug.Log(blk);
    //        Debug.Log(wht);
    //        Debug.Log("MouseDown " + Time.frameCount + " : ");


    //    }

    //    if (!isCached) // first selection or cache
    //    {
    //        cacheColor = parentSquareRend.material.color;
    //        Debug.Log(cacheColor);

    //        isCached = true;
    //    }
    //}


    void updateNearestSquareCoords() // Populates each pieces nearest square with the renderer component. Useful for landmarking or reference available positions.
    {
        foreach (GameObject _targetSquare in chessSquares)
        {
            float dist = Vector3.Distance(this.gameObject.transform.position, _targetSquare.transform.position);

            if (dist < targetDistance)
            {
                nearestSquare = _targetSquare;
                nearestSquareParent = _targetSquare.gameObject.transform.parent.name;

                pieceSquare = nearestSquare.GetComponent<Renderer>() as Renderer;
                squareStartColor = pieceSquare.material.color;
            }
        }
    }


    void updateMovementSquare()
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


