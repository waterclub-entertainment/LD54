using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityToken : MonoBehaviour
{
    Entity parent;

    // Start is called before the first frame update
    void Start()
    {
        parent = GetComponentInParent<Entity>();
    }

    public void OnDropInSlot(NavNode slot)
    {
        parent.ApproachToken(slot);
    }

    public void OnCompleteOperation()
    {
        transform.SetParent(parent.transform);
        transform.localPosition = Vector3.zero;
    }

    public bool IsMatchingEntity(Entity e) { return e == parent; }
}
