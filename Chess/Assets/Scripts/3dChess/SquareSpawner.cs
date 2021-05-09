using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareSpawner : MonoBehaviour
{

    private Vector3 currentLocation;
    public GameObject Square;

    // Start is called before the first frame update
    void Start()
    {
        currentLocation = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("w"))
        {
            if (GameObject.Find("Square"))
            {
                Debug.Log("Square already exists");
                return;

            }           
               
           
            GameObject.Instantiate(Square);
        }
        
    }
}
