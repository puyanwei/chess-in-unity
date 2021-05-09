using UnityEngine;

public class ArraySpawner : MonoBehaviour
{
    public GameObject squarePrefab; // square prefab with hover / collision / any desired properties needed for the board square.
    private GameObject chessSquare; // private GameObject class used to store instantiated objects in order to get their components later for material changing. By default any component selecting is on the object with script.

    public Transform spawnPoint;

    public Material whiteMat;
    public Material darkMat;



    public int xDim = 0;        // user changeable dimension of board.
    public int yDim = 0;        // user changeable dimension of board.
    private int placedIndex = 0; // counter of total blocks placed. i.e. 8x8 board will count to 64. This is used to find endpoint.
    private int xIndex = 0;     // x axis counter to identify when the end of row has been reached. 
    private int yIndex = 0;     // y axis counter used to alternate colouring of chess squares. When yIndex % 2 == 0 (even), white squares are 1,3,5,..,n. When yIndex % 2 != 0 (odd), white squares are 2,4,6,..,n.


    private void Update()
    {

        BoardSpawner();
        // spawns board as soon as scene begins based on framerate.
        
        
    }

    void BoardSpawner()
    {
        if (placedIndex <= ((xDim * yDim) - 1)) // When total squares placed is <= the area of the desired board, it draws the board. As placedIndex starts at 0, the total board size will be 1 less.
            // 
        {
            SquareSpawner();
        }


    }

    void spawnSquare(string x) // Chessboard square spawner which allows for dynamic naming.
    {
        GameObject chessSquare = (GameObject)Instantiate(squarePrefab, spawnPoint.position, spawnPoint.rotation) as GameObject;
        chessSquare.name = x;
    }


    void findJustSpawnedSquare() // Method to find the just spawned square used for material changing.
    {
        chessSquare = GameObject.Find(string.Join(",", xIndex + 1, yIndex + 1));
    }

    void colourSquareWhite() // Method to set material of instantiated object.
    {
        chessSquare.GetComponentInChildren<Renderer>().material = whiteMat;
    }

    void colourSquareDark() // Method to set material of instantiated object.
    {
        chessSquare.GetComponentInChildren<Renderer>().material = darkMat;
    }


    void SquareSpawner() 
    {

        if (xIndex <= (xDim - 1)) // loops through desired number of squares in a row, -1 again due to starting at 0.
        {
            spawnSquare(string.Join(",", xIndex + 1, yIndex + 1)); // spawns square and names it a logical name using +1 on x/y. Starting block is 1,1.
            spawnPoint.position += new Vector3(1, 0, 0); // shifts spawnpoint of next block by 1 unit.
            if (yIndex % 2 == 0) // to get alternating pattern, the y axis / height of the board is used. this alternates the pattern.
            {
                SquareColourOdd();
            }
            else
            {
                SquareColourEven();
            }

            xIndex++;
            placedIndex++;
        }
        else
        {
            spawnPoint.position += new Vector3((-xIndex), 0, 1); // once row is cycled through, spawn point is moved x units back to the start and height increased by 1 unit for the next row.
            yIndex++;
            xIndex = 0;

        }
    }

    void SquareColourOdd() // alternating material patterns based on if the square number is odd or even.
    {
        if (xIndex % 2 == 0)
        {
            findJustSpawnedSquare();
            colourSquareWhite();
        }
        else
        {
            findJustSpawnedSquare();
            colourSquareDark();
        }
    }

    void SquareColourEven()
    {
        if (xIndex % 2 == 0)
        {
            findJustSpawnedSquare();
            colourSquareDark();
        }
        else
        {
            findJustSpawnedSquare();
            colourSquareWhite();
        }
    }


}














