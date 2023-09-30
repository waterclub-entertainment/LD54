using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuleSet : ScriptableObject
{
    [Serializable]
    public class SpeciesTuple
    {
        public Species sp1;
        public Species sp2;
    }

    public List<SpeciesTuple> Blacklist;
}
