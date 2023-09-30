using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavNode : MonoBehaviour
{
    public List<NavNode> peers = new List<NavNode>();
    private NavRoom room;

    public NavRoom Room { get { return room; } }

    public void Register(NavRoom r)
    {
        room = r;
    }

    public void AddPeer(NavNode peer, bool propagate = true)
    {
        if (!peers.Contains(peer) && peer != null)
        {
            peers.Add(peer);
            if (propagate)
                peer.AddPeer(this, false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (NavNode node in peers)
        {
            if (node != null)
            {
                node.AddPeer(this, false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, 0.2f);
        Gizmos.color = Color.white;
        foreach (NavNode node in peers)
        {
            if (node != null)
                Gizmos.DrawLine(transform.position, node.transform.position);
        }
    }
}
