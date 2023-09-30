using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickHandler : MonoBehaviour
{
    private Camera cam;

    private GameObject currentHighlighted;

    public LayerMask TokenLayerMask;
    public LayerMask SlotLayerMask;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, Mathf.Infinity, TokenLayerMask);
            if (hit)
            {
                Debug.Log("Hit " + hitInfo.transform.gameObject.name);
                currentHighlighted = hitInfo.transform.gameObject;
            }
            else
            {
                Debug.Log("No hit");
            }
        }
        if (Input.GetMouseButton(0))
        {
            if (currentHighlighted != null)
                currentHighlighted.transform.position = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -cam.transform.position.z));
        }   
        if (Input.GetMouseButtonUp(0))
        {
            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, Mathf.Infinity, SlotLayerMask);
            if (hit)
            {
                Debug.Log("Hit " + hitInfo.transform.gameObject.name);
                currentHighlighted.transform.SetParent(hitInfo.transform);
                currentHighlighted.transform.localPosition = Vector3.zero;
                currentHighlighted = null;
            }
            else
            {
                currentHighlighted.transform.localPosition = Vector3.zero;
                currentHighlighted = null;
            }
        }
    }
}
