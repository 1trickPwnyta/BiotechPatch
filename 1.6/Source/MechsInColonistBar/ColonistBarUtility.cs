using System.Collections.Generic;
using System.Linq;
using Verse;

namespace BiotechPatch.MechsInColonistBar
{
    public static class ColonistBarUtility
    {
        public static void AddColonyMechs(Map map, List<Pawn> pawns)
        {
            if (BiotechPatchSettings.MechsInColonistBar)
            {
                pawns.AddRange(map.mapPawns.SpawnedColonyMechs.Where(m => ShouldShowMechInColonistBar(m)));
            }
        }

        public static bool ShouldShowMechInColonistBar(Pawn mech)
        {
            return BiotechPatchSettings.MechsInColonistBar && mech.IsColonyMech && mech.OverseerSubject != null;
        }
    }
}
