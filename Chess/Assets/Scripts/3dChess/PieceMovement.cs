using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceMovement : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;

    public GameObject piece;
    public GameObject square;
   

    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out hit))
        {
            if (hit.transform.name != piece.name)
            {          
                transform.position = hit.transform.position;
                Debug.Log(hit.transform.name);
            }
        }
    }


}

