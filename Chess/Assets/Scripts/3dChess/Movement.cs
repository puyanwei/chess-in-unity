using UnityEngine;
using UnityEngine.EventSystems;



public class Movement : MonoBehaviour

{

    public GameObject pawn;
    Vector3 targetPosition;





    void Start()
    {

        targetPosition = transform.position;

    }


    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                targetPosition = hit.point;
                pawn.transform.position = targetPosition;
            }
        }



    }



}
