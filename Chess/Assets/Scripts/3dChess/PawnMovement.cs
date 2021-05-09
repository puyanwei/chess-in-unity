using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnMovement : MonoBehaviour
{
    Ray ray;
    Ray pieceRay;
    RaycastHit hit;
    // test
    public GameObject piece;
    public GameObject square;
    private Vector3 movement;
    public GameObject kill;
    private Vector3 allowedMovement;
    public Color hoverColor;
    private Renderer rend;
    private Color startColor;
    private bool isHover = false;
    private string cubeInFront;

    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        movement = new Vector3(0, 0, 2);
        piece.transform.position = movement;
    }

    void OnDrawGizmos()
    {
      //  Gizmos.color = Color.red;
       // Gizmos.DrawRay(transform.position, piece.transform.position + allowedMovement);
    }

    void Update()
    {

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        pieceRay = Camera.main.ScreenPointToRay(piece.transform.position + allowedMovement);

        if (isHover)
        {
           
            Physics.Raycast(pieceRay, out hit);
            cubeInFront = hit.transform.name;
// Debug.Log(cubeInFront);



        }

        if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out hit))
        {
            if (hit.transform.name != piece.name)
            {

                piece.transform.position = movement;
                movement += new Vector3(0, 0, 1);
                

            }
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == kill.name)
        {
            GameObject.Destroy(piece);

        }

    }

    void OnMouseOver()
    {
        rend.material.color = hoverColor;
        isHover = true;

    }

    void OnMouseExit()
    {
        rend.material.color = startColor;
        isHover = false;

    }


    void functiontohit(Vector3 position)
    {
        ray = Camera.main.ScreenPointToRay(piece.transform.position + position);
        Physics.Raycast(ray, out hit);

    }

}


