using Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enums
{
    public enum Species
    {
        STAG,
        CAT,
        RAT,
        RAVEN,
        SPARROW,
        LIZARD,
        FROG,
        CRANE,
        CAPIBARA,
        MANTIS,
        COUNT
    }

    public enum Operation //TODO
    {
        STEAM_BATH
    }

    public enum Mood //TODO
    {
        NONE
    }
}

namespace Helper
{
    public class EnumHelper
    {
        public static Mood GetMood(int moodLevel)
        {
            return Mood.NONE; //TODO
        }
    }

}