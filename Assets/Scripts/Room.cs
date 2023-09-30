using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NavRoom))]
public class Room : MonoBehaviour
{
    public RuleSet SpeciesRules;


    private NavRoom navRoom;
    public NavRoom NavRoom {  get { return navRoom; } }


    private List<Entity> entities = new List<Entity>();

    // Start is called before the first frame update
    void Start()
    {
        navRoom = GetComponent<NavRoom>();
    }

    public void EnterRoom(Entity e)
    {
        entities.Add(e);
    }

    public void LeaveRoom(Entity e)
    {
        entities.Remove(e);
    }

    //VERIFY

    // Update is called once per frame
    void Update()
    {
        
    }
}
