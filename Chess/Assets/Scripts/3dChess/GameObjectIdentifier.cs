using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectIdentifier : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;  
    private object mouseOverObject;


    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
                        
            mouseOverObject = hit.transform.position;
           
        }
    }
}
