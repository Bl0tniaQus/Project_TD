﻿using System.Collections;
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


    private void LateUpdate()
    {

        if (Input.GetAxis("Mouse ScrollWheel")>0f) // forward
		{
            if (Camera.main.orthographicSize>1) Camera.main.orthographicSize--;
		}
		if (Input.GetAxis("Mouse ScrollWheel")<0f ) // backwards
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
