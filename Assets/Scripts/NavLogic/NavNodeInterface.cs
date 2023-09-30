using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class NavNodeInterface : NavNode
{
    public Vector2 Direction
    {
        get
        {
            return transform.right;
        }
    }
    public Vector2 GlobalPosition
    {
        get
        {
            return transform.position;
        }
    }
    public Vector2 LocalPosition
    {
        get
        {
            return transform.localPosition;
        }
    }

    private bool isLinked = false;

    private NavNodeInterface other;
    private List<NavNode> externalNodes = new List<NavNode>();
    public NavNodeInterface Other
    {
        get { return other; }
    }

    public bool IsLinked
    {
        get
        {
            return isLinked;
        }
    }

    public void SetOtherSide(NavNodeInterface n)
    {
        other = n;
    }

    public bool isExternal(NavNode n)
    {
        return externalNodes.Contains(n);
    }

    public void AddExternalPeer(NavNode peer, bool propagate = true)
    {
        if (!isLinked)
        {
            externalNodes.Add(peer);
            base.AddPeer(peer, propagate);
        }
        isLinked = true;
    }
    protected override void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(GlobalPosition, GlobalPosition + Direction);
        base.OnDrawGizmos();
    }
}
