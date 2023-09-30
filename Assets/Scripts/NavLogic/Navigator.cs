using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using UnityEngine;
using UnityEngine.Events;

public class Navigator : MonoBehaviour
{
    public float speed;
    public float sqrArrivalRadius;

    public NavNode goal = null;
    public NavBuilding navManager;
    public NavNode currentNode = null;
    private NavRoom currentRoom = null;
    public bool isGoing;

    private List<NavNode> positionSeries = new List<NavNode>();

    [HideInInspector]
    public UnityEvent<Navigator, NavNode> OnNavigatorArrived = new UnityEvent<Navigator, NavNode>();
    [HideInInspector]
    public UnityEvent<Navigator, NavRoom> OnLeaveRoom = new UnityEvent<Navigator, NavRoom>();
    [HideInInspector]
    public UnityEvent<Navigator, NavRoom> OnEnterRoom = new UnityEvent<Navigator, NavRoom>();

    // Start is called before the first frame update
    void Start()
    {
        if (currentNode != null && currentRoom == null)
        currentRoom = currentNode.Room;
        FindDirection();
    }

    public void StopNavigation()
    {
        goal = null;
        isGoing = false;
    }
    public void StartNavigation()
    {
        isGoing=true;
        if (goal == null)
            FindDirection();
    }

    void FindDirection()
    {
        positionSeries.Clear();
        if (isGoing)
        {
            goal = currentRoom.InternalNodes[UnityEngine.Random.Range(0, currentRoom.InternalNodes.Length)];
        }
        List<NavNode> tmp = new List<NavNode>();
        navManager.GetPath(currentNode, ref positionSeries, goal, ref tmp);
        positionSeries.Insert(0, currentNode);
    }

    public void SetDirection(NavNode g)
    {
        positionSeries.Clear();
        isGoing = true;
        goal = g;
        List<NavNode> tmp = new List<NavNode>();
        navManager.GetPath(currentNode, ref positionSeries, goal, ref tmp);
        positionSeries.Insert(0, currentNode);
    }

    // Update is called once per frame
    void Update()
    {
        if (isGoing)
        {
            if (positionSeries.Count > 0)
            {
                var frst = positionSeries.First();
                Vector3 dist = (transform.position - frst.transform.position);

                transform.position -= dist.normalized * speed * Time.deltaTime;
                if (dist.sqrMagnitude < sqrArrivalRadius)
                {
                    if (frst is NavNodeInterface)
                    {
                        if (currentRoom == null)
                        {
                            currentRoom = frst.Room;
                            OnEnterRoom.Invoke(this, currentRoom);
                        }
                        else
                        {
                            OnLeaveRoom.Invoke(this, currentRoom);
                            currentRoom = null;
                        }
                    }
                    currentNode = positionSeries[0];
                    positionSeries.RemoveAt(0);
                }
            }
            else
            {
                Vector3 dist = (transform.position - goal.transform.position);

                transform.position -= dist.normalized * speed * Time.deltaTime;
                if (dist.sqrMagnitude < sqrArrivalRadius)
                {
                    currentNode = goal;
                    OnNavigatorArrived.Invoke(this, goal);
                    goal = null;
                    FindDirection();
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, currentNode.transform.position);
        if (positionSeries.Count > 0)
            Gizmos.DrawLine(transform.position, positionSeries[0].transform.position);
        else if (goal != null)
            Gizmos.DrawLine(transform.position, goal.transform.position);
    }
}
