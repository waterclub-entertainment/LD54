using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    private Vector3 dragOrigin;


    private void Update()
    {
        PanCamera(); 
    }


    private void PanCamera()
    {
        if (Input.GetMouseButtonDown(0))
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButton(0))
        {
            Vector3 difference = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);
            //F�r Troubleshootingfzwecke
            print("origin" + dragOrigin + " neue Position " + cam.ScreenToWorldPoint(Input.mousePosition) + " =unbterschied" + difference);

            cam.transform.position += difference;
        }
    }
}
