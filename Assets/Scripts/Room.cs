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
        if (SpeciesRules == null)
            return;
        int[] countMap = new int[(int)Enums.Species.COUNT];

        foreach(Entity e in entities)
        {
            countMap[(int)e.Species]++;
        }
        if (!SpeciesRules.Check(countMap))
            Debug.Log("CONFLICT!!!!");
    }
}
