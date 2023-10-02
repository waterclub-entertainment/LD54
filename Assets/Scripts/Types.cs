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
        PISSED,
        UNHAPPY,
        NEUTRAL,
        HAPPY,
        ASCENDED
    }
}

namespace Helper
{
    public class EnumHelper
    {
        public static Mood GetMood(int moodLevel)
        {
            if (moodLevel >= 0) {
                return Mood.ASCENDED;
            } else if (moodLevel >= -3) {
                return Mood.HAPPY;
            } else if (moodLevel >= -5) {
                return Mood.NEUTRAL;
            } else if (moodLevel >= -8) {
                return Mood.UNHAPPY;
            } else if (moodLevel >= -10) {
                return Mood.PISSED;
            } else {
                Debug.LogError("THIS SHOULDNT HAPPEN");
                return Mood.ASCENDED;
            }
        }

        public static bool IsOperationHot(Operation? op) {
            return op == Operation.HOT_SPRING_OUTSIDE || op == Operation.HOT_SPRING_INSIDE || op == Operation.SAUNA;
        }
    }

}