using System.Collections.Generic;
using Verse;

namespace BiotechPatch.MechsInColonistBar
{
    public static class ColonistBarUtility
    {
        public static void AddColonyMechs(Map map, List<Pawn> pawns)
        {
            if (BiotechPatchSettings.MechsInColonistBar)
            {
                pawns.AddRange(map.mapPawns.SpawnedColonyMechs);
            }
        }

        public static bool ShouldShowMechInColonistBar(Pawn mech)
        {
            return BiotechPatchSettings.MechsInColonistBar && mech.IsColonyMech;
        }
    }
}
