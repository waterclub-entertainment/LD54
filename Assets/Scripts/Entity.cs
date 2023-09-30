using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Navigator))]
public class Entity : MonoBehaviour
{
    private Navigator nav;
    public Navigator Navigate { get { return nav; } }
    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<Navigator>();
        nav.OnEnterRoom.AddListener(OnEnterRoom);
        nav.OnLeaveRoom.AddListener(OnLeaveRoom);
        nav.OnNavigatorArrived.AddListener(OnEnterNode);
    }

    void OnLeaveRoom(Navigator nav, NavRoom room)
    {
        room.Room.LeaveRoom(this);
    }
    void OnEnterRoom(Navigator nav, NavRoom room)
    {
        room.Room.EnterRoom(this);
    }

    void OnEnterNode(Navigator nav, NavNode n)
    {
        if (n is NavNodeSlot)
        {
            var slt = n.GetComponent<Slot>();
            slt.OnEntityArrived(this);
        }
    }

    public void ApplyTreatment(Enums.Operation op)
    {
        Debug.Log("Applied " + op.ToString());
    }
    public void ApproachToken(NavNode slot)
    {
        nav.SetDirection(slot);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
