using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavNodeSlot : NavNode
{
    public string Tag;

    protected override void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;

        base.OnDrawGizmos();
    }
}
