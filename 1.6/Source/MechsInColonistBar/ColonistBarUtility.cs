using System.Collections.Generic;
using System.Linq;
using Verse;

namespace BiotechPatch.MechsInColonistBar
{
    public static class ColonistBarUtility
    {
        public static void AddColonyMechs(Map map, List<Pawn> pawns)
        {
            if (SpecialModSettings_Multipatch_Biotech.MechsInColonistBar)
            {
                pawns.AddRange(map.mapPawns.SpawnedColonyMechs.Where(m => ShouldShowMechInColonistBar(m)));
            }
        }

        public static bool ShouldShowMechInColonistBar(Pawn mech)
        {
            return SpecialModSettings_Multipatch_Biotech.MechsInColonistBar && mech.IsColonyMech && mech.OverseerSubject != null;
        }
    }
}
