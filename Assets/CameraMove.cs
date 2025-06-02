using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private Vector3 Origin;
    private Vector3 Difference;
    private Vector3 ResetCamera;

    private bool drag = false;

    

    private void Start()
    {
        ResetCamera = Camera.main.transform.position;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            Camera.main.transform.position = Camera.main.transform.position + Vector3.right*(Time.deltaTime * 6f);
        }
        if (Input.GetKey(KeyCode.A))
        {
            Camera.main.transform.position = Camera.main.transform.position - Vector3.right*(Time.deltaTime * 6f);
        }
        if (Input.GetKey(KeyCode.W))
        {
            Camera.main.transform.position = Camera.main.transform.position + Vector3.up*(Time.deltaTime * 6f);
        }
        if (Input.GetKey(KeyCode.S))
        {
            Camera.main.transform.position = Camera.main.transform.position - Vector3.up*(Time.deltaTime * 6f);
        }
    }

    private void LateUpdate()
    {

        if (Input.GetAxis("Mouse ScrollWheel")>0f || Input.GetKeyDown(KeyCode.Q)) // forward
		{
            if (Camera.main.orthographicSize>1) Camera.main.orthographicSize--;
		}
		if (Input.GetAxis("Mouse ScrollWheel")<0f  || Input.GetKeyDown(KeyCode.E)) // backwards
		{
			Camera.main.orthographicSize++;
		}

        if (Input.GetMouseButton(2))
        {
            Difference = (Camera.main.ScreenToWorldPoint(Input.mousePosition)) - Camera.main.transform.position;
            if(drag == false)
            {
                drag = true;
                Origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }

        }
        else
        {
            drag = false;
        }

        if (drag)
        {
            Camera.main.transform.position = Origin - Difference;
        }

        


    }
}
