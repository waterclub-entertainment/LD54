using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class EntityToken : MonoBehaviour
{
    Entity parent;
    Renderer renderHandler;

    // Start is called before the first frame update
    void Start()
    {
        parent = GetComponentInParent<Entity>();
        renderHandler = GetComponent<Renderer>();
    }

    public bool OnDropInSlot(NavNode slot)
    {
        return parent.ApproachToken(slot);
    }

    public void OnCompleteOperation()
    {
        transform.SetParent(parent.transform);
        transform.localPosition = Vector3.zero;
        renderHandler.enabled = false;
    }

    public void OnCancelDrag()
    {
        renderHandler.enabled = false;
    }
    public void OnStartDrag()
    {
        renderHandler.enabled = true;
    }

    public bool IsMatchingEntity(Entity e) { return e == parent; }
}
