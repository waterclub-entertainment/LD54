using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavNodeSlot : NavNode
{
    protected override void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;

        base.OnDrawGizmos();
    }
}
