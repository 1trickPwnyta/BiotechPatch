using System.Collections.Generic;
using Verse;

namespace BiotechPatch.MechsInColonistBar
{
    public static class ColonistBarUtility
    {
        public static void AddColonyMechs(Map map, List<Pawn> pawns)
        {
            pawns.AddRange(map.mapPawns.SpawnedColonyMechs);
        }
    }
}
