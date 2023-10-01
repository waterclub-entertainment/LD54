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
                Debug.Log("Hit Token");
                currentHighlighted = hitInfo.transform.gameObject;
                currentHighlighted.GetComponent<EntityToken>().OnStartDrag();
            }
        }
        if (Input.GetMouseButton(0))
        {
            if (currentHighlighted != null)
                currentHighlighted.transform.position = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -cam.transform.position.z));
        }
        if (Input.GetMouseButtonUp(0) && currentHighlighted != null)
        {
            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, Mathf.Infinity, SlotLayerMask);
            if (hit)
            {
                var slot = hitInfo.transform.gameObject.GetComponent<Slot>();

                if (!slot.IsOccupied)
                {
                    var token = currentHighlighted.GetComponent<EntityToken>();
                    var success = token.OnDropInSlot(hitInfo.transform.gameObject.GetComponent<NavNode>());
                    if (success) {
                        slot.OnSetToken(token);
                        currentHighlighted.transform.SetParent(hitInfo.transform);
                        currentHighlighted.transform.localPosition = Vector3.zero;
                        currentHighlighted = null;
                    }
                }
            }
            else
            {
                currentHighlighted.GetComponent<EntityToken>().OnCancelDrag();
                currentHighlighted.transform.localPosition = Vector3.zero;
                currentHighlighted = null;
            }
        }
    }
}
