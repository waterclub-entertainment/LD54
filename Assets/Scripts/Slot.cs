using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[RequireComponent(typeof(NavNodeSlot))]
public class Slot : MonoBehaviour
{
    public Room SpaRoom;

    SpriteRenderer refSprite;

    private EntityToken token;
    private Entity entity;
    public bool IsOccupied { get { return entity != null || token != null; } }

    public Enums.Operation appliedTreatment;
    public float operationTime;
    private float operating;

    // Start is called before the first frame update
    void Start()
    {
        refSprite = GetComponentInChildren<SpriteRenderer>();
        refSprite.transform.up = Vector3.up;
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
            operating = 0;
        }
    }

    public void RemoveToken()
    {
        token = null;
    }

    public void CompleteOperation()
    {
        entity.ApplyTreatment(appliedTreatment);
        entity.Navigate.StartNavigation();
        token.OnCompleteOperation();
        entity = null;
        token = null;
    }

    public void StopHovered()
    {
        refSprite.enabled = false;
    }
    public void IsHovered(EntityToken token)
    {
        refSprite.sprite = token.gameObject.GetComponent<SpriteRenderer>().sprite;
        refSprite.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (entity != null && !SpaRoom.HasConflict())
        {
            operating += Time.deltaTime;
            if (operating > operationTime)
            {
                CompleteOperation();
            }
        }
    }

}
