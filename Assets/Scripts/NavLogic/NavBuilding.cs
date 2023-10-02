using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Unity.Mathematics;
using UnityEngine;

public class NavBuilding : MonoBehaviour
{
    private NavRoom[] rooms; //needed?

    [Serializable]
    public class NavLink
    {
        public NavNodeInterface roomA;
        public NavNodeInterface roomB;
    }

    public List<NavLink> connections;

    private void BuildNavGraphConnection(NavNodeInterface a, NavNodeInterface b)
    {
        bool xAligned = a.GlobalPosition.x == b.GlobalPosition.x;
        bool yAligned = a.GlobalPosition.y == b.GlobalPosition.y;
        Vector2 delta = b.GlobalPosition - a.GlobalPosition;

        if (!xAligned && !yAligned)
        {
            if (a.Direction == b.Direction)
            {
                var aNode = a.GlobalPosition + a.Direction * 2f;
                var bNode = b.GlobalPosition + b.Direction * 2f;
                if (a.Direction.x > a.Direction.y)
                {
                    aNode.y = bNode.y = Mathf.Max(aNode.y, bNode.y);
                }
                else
                {
                    aNode.x = bNode.x = Mathf.Max(aNode.x, bNode.x);
                }
                NavNode navA = null, navB = null;
                var newNavNode = new GameObject();
                newNavNode.transform.SetParent(transform);
                newNavNode.transform.position = aNode;
                navA = newNavNode.AddComponent<NavNode>();
                newNavNode = new GameObject();
                newNavNode.transform.SetParent(transform);
                newNavNode.transform.position = bNode;
                navB = newNavNode.AddComponent<NavNode>();

                a.AddExternalPeer(navA);
                navA.AddPeer(navB);
                b.AddExternalPeer(navB);
            }
            else if (a.Direction == -b.Direction)
            {
                if (Vector2.Dot(delta, a.Direction) < 0 || Vector2.Dot(delta, b.Direction) > 0) //check if on the correct side
                {
                    var tmp = b;
                    b = a;
                    a = tmp;
                }
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
                Debug.Log("Single Corner");
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
        
        if (current == null || goal == null || !current.gameObject.activeSelf)
            return minDist;
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
            BuildNavGraphConnection(con.roomA, con.roomB);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
