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
    private Dictionary<Enums.Species, Enums.Species> conflictingSpecies = new Dictionary<Enums.Species, Enums.Species>();

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
        e.StopConflict();
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
        conflictingSpecies = SpeciesRules.Check(countMap);
    }

    public bool HasConflict() {
        return conflictingSpecies.Count != 0;
    }

    public Enums.Species? IsConflicting(Enums.Species sp) {
        return conflictingSpecies[sp];
    }
}
