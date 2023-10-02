using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/RuleSet")]
public class RuleSet : ScriptableObject
{
    [Serializable]
    public class SpeciesTuple
    {
        public Enums.Species sp1;
        public Enums.Species sp2;
    }

    public List<SpeciesTuple> Blacklist;


    public Dictionary<Enums.Species, Enums.Species> Check(int[] speciesCounts)
    {
        Dictionary<Enums.Species, Enums.Species> conflictingSpecies = new Dictionary<Enums.Species, Enums.Species>();
        foreach(SpeciesTuple s in Blacklist)
        {
            if (speciesCounts[(int)s.sp1] > 0 && speciesCounts[(int)s.sp2] > 0) {
                conflictingSpecies[s.sp1] = s.sp2;
                conflictingSpecies[s.sp2] = s.sp1;
            }
        }
        return conflictingSpecies;
    }
}
