using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class EntityToken : MonoBehaviour, IToken
{
    Entity parent;
    Transform currentParent;
    Renderer renderHandler;

    // Start is called before the first frame update
    void Start()
    {
        parent = GetComponentInParent<Entity>();
        currentParent = parent.transform;
        renderHandler = GetComponent<Renderer>();
    }

    public EntityToken GetToken()
    { return this; }
    public bool OnDropInSlot(NavNode slot)
    {
        bool res = parent.ApproachToken(slot);
        if (res)
            currentParent = slot.transform;
        return res;
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
        transform.localPosition = Vector3.zero;
    }
    public void OnStartDrag()
    {
        renderHandler.enabled = true;
        if (currentParent != parent.transform)
        {
            currentParent.gameObject.GetComponent<Slot>().RemoveToken();
            transform.SetParent(parent.transform);
            transform.localPosition = Vector3.zero;
            currentParent = parent.transform;
        }
    }

    public bool IsMatchingEntity(Entity e) { return e == parent; }
}
