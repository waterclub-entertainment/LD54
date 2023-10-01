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

    public enum Operation
    {
        STEAM_BATH,
        CHANGING_ROOM,
        EXIT,
        SAUNA,
        HOT_SPRING_INSIDE,
        HOT_SPRING_OUTSIDE,
        NORMAL_BATH,
        SHOWER,
        COLD_BATH
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

        public static bool IsOperationHot(Operation? op) {
            return op == Operation.HOT_SPRING_OUTSIDE || op == Operation.HOT_SPRING_INSIDE || op == Operation.SAUNA;
        }
    }

}