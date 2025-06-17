using System.Collections.Generic;
using Verse;

namespace BiotechPatch.ResurrectedMechsRememberGroup
{
    public static class DeadMechUtility
    {
        private static Dictionary<Pawn, Dictionary<Pawn, int>> lastControlGroup = new Dictionary<Pawn, Dictionary<Pawn, int>>();

        public static Dictionary<Pawn, int> GetLastControlGroups(this Pawn mech)
        {
            if (lastControlGroup.ContainsKey(mech))
            {
                return lastControlGroup[mech];
            }
            else
            {
                return new Dictionary<Pawn, int>();
            }
        }

        public static int GetLastControlGroupIndex(this Pawn mech, Pawn mechanitor)
        {
            if (lastControlGroup.ContainsKey(mech) && lastControlGroup[mech] != null && lastControlGroup[mech].ContainsKey(mechanitor))
            {
                return lastControlGroup[mech][mechanitor];
            }
            else
            {
                return -1;
            }
        }

        public static void SetLastControlGroups(this Pawn mech, Dictionary<Pawn, int> lastControlGroups)
        {
            lastControlGroup[mech] = lastControlGroups;
        }

        public static void SetLastControlGroupIndex(this Pawn mech, Pawn mechanitor, int index)
        {
            if (!lastControlGroup.ContainsKey(mech) || lastControlGroup[mech] == null)
            {
                lastControlGroup[mech] = new Dictionary<Pawn, int>();
            }
            lastControlGroup[mech][mechanitor] = index;
        }
    }
}
