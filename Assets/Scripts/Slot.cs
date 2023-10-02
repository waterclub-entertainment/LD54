using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(NavNodeSlot))]
public class Slot : MonoBehaviour
{
    public Room SpaRoom;
    public Image progressImage;

    public bool IsActive { get { return progressImage.enabled; } }

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
        progressImage.enabled = false;
        SpaRoom = GetComponentInParent<Room>();
    }

    public void OnSetToken(EntityToken token)
    {
        this.token = token;
    }

    public void OnEntityArrived(Entity e)
    {
        if (entity == null && token != null && token.IsMatchingEntity(e)) {
            SpaRoom.EnterRoom(e);
            entity = e;
            e.Navigate.StopNavigation();
            operating = 0;
        }
    }

    public void RemoveToken()
    {
        if (entity != null)
        {
            SpaRoom.LeaveRoom(entity);
            entity.Navigate.StartNavigation();
            entity = null;
        }
        SpaRoom.LeaveRoom(token.Parent);
        token = null;
    }

    public void CompleteOperation()
    {
        entity.ApplyTreatment(appliedTreatment);
        entity.Navigate.StartNavigation();
        token.OnCompleteOperation();
        entity = null;
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
        if (entity != null)
        {
            if (!SpaRoom.HasConflict()) {
                entity.SetConflict(null);
                operating += Time.deltaTime;
                progressImage.enabled = true;
                progressImage.fillAmount = 1.0f - (operating / operationTime);
                if (operating > operationTime)
                {
                    CompleteOperation();
                }
            } else {
                var conflict = SpaRoom.IsConflicting(entity.Species);
                entity.SetConflict(conflict);
                if (conflict is Enums.Species species) {
                    Debug.Log("Conflicting: " + entity.Species + " with " + species);
                }
            }
        }
        else
        {
            progressImage.enabled = false;
        }
    }

}
