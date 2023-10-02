using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using UnityEngine;
using UnityEngine.Events;

public class Navigator : MonoBehaviour
{
    public float speed;
    public float idleWaitTime = 1f;
    public float sqrArrivalRadius;

    public NavNode goal = null;
    public NavBuilding navManager;
    public NavNode currentNode = null;
    private NavRoom currentRoom = null;
    public bool isGoing;

    public Vector2 Heading
    {
        get
        {
            if (positionSeries.Count() > 0)
            {
                return transform.position - positionSeries[0].transform.position;
            }
            else if (goal != null)
            {
                return transform.position - goal.transform.position;
            }
            else
            {
                return Vector2.right;
            }
        }
    }

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
        Debug.Log("Stop");
        CancelInvoke("StartNavigation");
        goal = null;
        isGoing = false;
    }
    public void StartNavigation()
    {
        Debug.Log("Start");
        CancelInvoke("StartNavigation");
        isGoing=true;
        if (goal == null)
            FindDirection();
    }

    void FindDirection()
    {
        positionSeries.Clear();
        if (isGoing)
        {
            if (currentRoom.InternalNodes.Length > 1)
            {
                goal = currentRoom.InternalNodes[UnityEngine.Random.Range(0, currentRoom.InternalNodes.Length)];
            }
            else
            {
                goal = (currentRoom.InternalNodes[0] as NavNodeInterface).Other;
            }
            List<NavNode> tmp = new List<NavNode>();
            navManager.GetPath(currentNode, ref positionSeries, goal, ref tmp);
            positionSeries.Insert(0, currentNode);
        }
    }

    public void SetDirection(NavNode g)
    {
        positionSeries.Clear();
        goal = g;
        List<NavNode> tmp = new List<NavNode>();
        navManager.GetPath(currentNode, ref positionSeries, goal, ref tmp);
        positionSeries.Insert(0, currentNode);
        StartNavigation();
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
                        else if ((positionSeries.Count > 1 && (frst as NavNodeInterface).isExternal(positionSeries[1])) ||
                            (positionSeries.Count == 1 && (frst as NavNodeInterface).isExternal(goal)))
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
                    if (goal is NavNodeInterface)
                    {
                        if (currentRoom == null)
                        {
                            currentRoom = goal.Room;
                            OnEnterRoom.Invoke(this, currentRoom);
                        }
                    }

                    currentNode = goal;
                    OnNavigatorArrived.Invoke(this, goal);
                    goal = null;
                    if (isGoing) {
                        StopNavigation();
                        Invoke("StartNavigation", idleWaitTime);
                    }
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        if (currentNode != null)
            Gizmos.DrawLine(transform.position, currentNode.transform.position);
        if (positionSeries.Count > 0)
            Gizmos.DrawLine(transform.position, positionSeries[0].transform.position);
        else if (goal != null)
            Gizmos.DrawLine(transform.position, goal.transform.position);
    }
}
