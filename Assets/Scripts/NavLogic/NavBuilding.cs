using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class NavBuilding : MonoBehaviour
{
    private NavRoom[] rooms; //needed?

    [Serializable]
    public class NavLink
    {
        public NavRoom roomA;
        public NavRoom roomB;
    }

    public List<NavLink> connections;

    private void BuildNavGraphConnection(NavNodeInterface a, NavNodeInterface b)
    {
        bool xAligned = a.GlobalPosition.x == b.GlobalPosition.x;
        bool yAligned = a.GlobalPosition.y == b.GlobalPosition.y;
        Vector2 delta = b.GlobalPosition - a.GlobalPosition;

        if (!xAligned && !yAligned)
        {
            if (Vector2.Dot(delta, a.Direction) >= 0 && Vector2.Dot(delta, b.Direction) <= 0) //check if on the correct side
            {
                NavNode navA = null, navB = null;
                if (a.Direction.x < a.Direction.y)
                {
                    float half = delta.y / 2f;

                    var newNavNode = new GameObject();
                    newNavNode.transform.SetParent(transform);
                    newNavNode.transform.position = a.GlobalPosition + new Vector2(0, half);
                    navA = newNavNode.AddComponent<NavNode>();
                    newNavNode = new GameObject();
                    newNavNode.transform.SetParent(transform);
                    newNavNode.transform.position = a.GlobalPosition + new Vector2(delta.x, half);
                    navB = newNavNode.AddComponent<NavNode>();
                }
                else
                {
                    float half = delta.x / 2f;

                    var newNavNode = new GameObject();
                    newNavNode.transform.SetParent(transform);
                    newNavNode.transform.position = a.GlobalPosition + new Vector2(half, 0);
                    navA = newNavNode.AddComponent<NavNode>();
                    newNavNode = new GameObject();
                    newNavNode.transform.SetParent(transform);
                    newNavNode.transform.position = a.GlobalPosition + new Vector2(half, delta.y);
                    navB = newNavNode.AddComponent<NavNode>();
                }
                a.AddExternalPeer(navA);
                navA.AddPeer(navB);
                b.AddExternalPeer(navB);
            }
            else
            {
                throw new System.Exception("This would require complex pathfinding. This is not happening");
            }
        }
        else
        {
            a.AddExternalPeer(b);
            b.AddExternalPeer(a);
        }
        a.SetOtherSide(b);
        b.SetOtherSide(a);
    }

    public float GetPath(NavNode current, ref List<NavNode> path, NavNode goal, ref List<NavNode> visited)
    {
        float minDist = 99999f;

        visited.Add(current);

        var predPath = new List<NavNode>();

        foreach(NavNode node in current.peers.OrderBy(x => (goal.transform.position - x.transform.position).sqrMagnitude))
        {
            if (visited.Contains(node))
            {
                continue;
            }
            if (node == goal)
            {
                return (current.transform.position - node.transform.position).magnitude;
            }
            predPath.Clear();

            float dist = GetPath(node, ref predPath, goal, ref visited);
            dist += (current.transform.position - node.transform.position).magnitude;
            if (dist < minDist)//reset
            {
                //add new distance
                path.Clear();
                path.AddRange(predPath);
                path.Insert(0, node);
                minDist = dist;
            }
        }
        return minDist;
    }

    // Start is called before the first frame update
    void Start()
    {
        rooms = GetComponentsInChildren<NavRoom>();

        foreach (var con in connections)
        {
            //find closest pair of interfaces
            NavNodeInterface a = null, b = null;
            float lastDist = 99999999;
            foreach(var i in con.roomA.FreeInterfaces)
            {
                if (a == null)
                    a = i;
                foreach (var j in con.roomB.FreeInterfaces)
                {
                    if (b == null)
                        b = j;
                    if (lastDist > (i.GlobalPosition - j.GlobalPosition).sqrMagnitude)
                    {
                        a = i; b = j;
                        lastDist = (i.GlobalPosition - j.GlobalPosition).sqrMagnitude;
                    }
                }
            }
            BuildNavGraphConnection(a, b);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
