using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    private Vector3 dragOrigin;
    public float zoomStep, minCameraSize, maxCameraSize;

    private void Update()
    {
        PanCamera();
        if (Input.GetAxis("Mouse ScrollWheel") != 0f) // forward
        {
            float SizeChange = Input.GetAxis("Mouse ScrollWheel");
            cam.orthographicSize -= SizeChange * zoomStep;
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, minCameraSize, maxCameraSize);
        }
    }


    private void PanCamera()
    {
        if (Input.GetMouseButtonDown(0))
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButton(0))
        {
            Vector3 difference = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);
            //Für Troubleshootingfzwecke
            print("origin" + dragOrigin + " neue Position " + cam.ScreenToWorldPoint(Input.mousePosition) + " =unterschied" + difference);

            cam.transform.position += difference;
        }
    }
}
