using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[RequireComponent(typeof(NavNodeSlot))]
public class Slot : MonoBehaviour
{
    public Room SpaRoom;
    private EntityToken token;
    private Entity entity;
    public bool IsOccupied { get { return entity != null || token != null; } }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnSetToken(EntityToken token)
    {
        this.token = token;
    }

    public void OnEntityArrived(Entity e)
    {
        if (entity == null && token != null && token.IsMatchingEntity(e)) {
            entity = e;
            e.Navigate.StopNavigation();
        }
    }

    public void CompleteOperation()
    {
        entity.Navigate.StartNavigation();
        token.OnCompleteOperation();
        entity = null;
        token = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (entity != null)
        {
            //operate
        }
    }
}
