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


    public HashSet<Enums.Species> Check(int[] speciesCounts)
    {
        HashSet<Enums.Species> conflictingSpecies = new HashSet<Enums.Species>();
        foreach(SpeciesTuple s in Blacklist)
        {
            if (speciesCounts[(int)s.sp1] > 0 && speciesCounts[(int)s.sp2] > 0)
                conflictingSpecies.Add(s.sp1);
                conflictingSpecies.Add(s.sp2);
        }
        return conflictingSpecies;
    }
}
