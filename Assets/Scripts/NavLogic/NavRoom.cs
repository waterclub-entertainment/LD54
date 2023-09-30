using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Room))]
public class NavRoom : MonoBehaviour
{
    private NavNode[] nodes;
    private NavNodeInterface[] roomInterfaces;
    private NavNodeSlot[] roomSlots;

    public NavNodeInterface[] Interfaces
    {
        get
        {
            return roomInterfaces;
        }
    }
    public NavNode[] InternalNodes
    {
        get
        {
            return nodes;
        }
    }
    public NavNodeInterface[] FreeInterfaces
    {
        get
        {
            return roomInterfaces.Where(x => !x.IsLinked).ToArray();
        }
    }


    private Room room;
    public Room Room {  get { return room; } }

    // Start is called before the first frame update
    void Start()
    {
        nodes = GetComponentsInChildren<NavNode>();
        roomInterfaces = GetComponentsInChildren<NavNodeInterface>();
        roomSlots = GetComponentsInChildren<NavNodeSlot>();
        room = GetComponent<Room>();

        foreach (NavNode node in nodes)
        {
            node.Register(this);
        }
        foreach (NavNode node in roomInterfaces)
        {
            node.Register(this);
        }
        foreach (NavNode node in roomSlots)
        {
            node.Register(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
