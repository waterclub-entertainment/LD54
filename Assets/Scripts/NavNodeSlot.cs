using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavNodeSlot : NavNode
{
    public string Tag;

    public bool hasToken; //Change Downstream

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    protected override void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;

        base.OnDrawGizmos();
    }
}
