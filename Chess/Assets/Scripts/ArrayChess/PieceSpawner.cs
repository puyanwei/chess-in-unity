using UnityEngine;



public class PieceSpawner : MonoBehaviour    
{

    public GameObject piecePrefab;
    private Vector3 spawnPoint;
    private int pieceIndex = 0;
    public int xDim = 0;
    public int yDim = 0;
    private int xIndex = 0;
    private int yIndex = 0;



    private int squareIndex;


    void spawnPiece(string x) // Chessboard piece spawner which allows for dynamic naming.
    {
        GameObject chessPiece = (GameObject)Instantiate(piecePrefab, spawnPoint, Quaternion.identity) as GameObject;
        chessPiece.name = x;
    }



    void Update()
    {
        if (GameObject.Find(string.Join(",", xDim , yDim )) == true && pieceIndex < xDim)
        {
            
            PieceSpawnController();
            pieceIndex++;
            Debug.Log("SpawnScriptCalled");
            
        }
        else
        {
            return;
            Debug.Log("FinishedSpawning");
        }
        
      
    }

    void PieceSpawnController()
    {
        string squareNameforPawns = string.Join(",", xIndex + 1, yIndex + 2);
        string squareNameforSpecials = string.Join(",", xIndex + 1, yIndex + 1);
  


        if (xIndex <= (xDim - 1) && GameObject.Find(squareNameforPawns)) // loops through row, find eligible pieces for pawns and place them.
        {
            
            spawnPoint = GameObject.Find(squareNameforPawns).transform.position;
            spawnPiece(string.Join(",", "Pawn", xIndex + 1, yIndex + 1)); // spawns pawn and names it a logical name using +1 on x/y. Starting block is 1,1.
            Debug.Log(squareNameforPawns);
            xIndex++;


        }
        else
        {
            return;
        }

        if (xIndex == xDim || xIndex <= 1 && GameObject.Find(squareNameforSpecials)) // loops through row, find eligible pieces for pawns and place them.
        {

            spawnPoint = GameObject.Find(squareNameforSpecials).transform.position;
            spawnPiece(string.Join(",", "Rook", xIndex + 1, yIndex + 1));
            Debug.Log(string.Join(" ", squareNameforSpecials, "rooks"));            

        }
        else
        {
            return;
        }

        

    }



}
