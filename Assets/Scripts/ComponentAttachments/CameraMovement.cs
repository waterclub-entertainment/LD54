using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    [Range(0f, 0.5f)]
    public float panThreshold = 0.1f;
    public float scrollSpeed;

    public float xRange, yRange;

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
        Vector2 mousePos = Input.mousePosition;
        mousePos = new Vector2(mousePos.x / Screen.width, mousePos.y / Screen.height);

        if (mousePos.x < panThreshold)
        {
            cam.transform.position -= Vector3.right * Time.deltaTime * scrollSpeed;
        }
        if (mousePos.x > 1f - panThreshold)
        {
            cam.transform.position += Vector3.right * Time.deltaTime * scrollSpeed;
        }
        if (mousePos.y < panThreshold)
        {
            cam.transform.position -= Vector3.up * Time.deltaTime * scrollSpeed;
        }
        if (mousePos.y > 1f - panThreshold)
        {
            cam.transform.position += Vector3.up * Time.deltaTime * scrollSpeed;
        }
        Vector3 pos = cam.transform.position;
        pos.x = Mathf.Clamp(pos.x, -xRange, xRange);
        pos.y = Mathf.Clamp(pos.y, -yRange, yRange);
        cam.transform.position = pos;
    }
}
