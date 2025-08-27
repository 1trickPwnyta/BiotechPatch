using RimWorld;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace BiotechPatch.ChildrenGoByFirstName
{
    public static class NameUtility
    {
        private static Dictionary<NameTriple, Pawn> names = new Dictionary<NameTriple, Pawn>();

        public static Pawn GetPawn(this NameTriple nameTriple)
        {
            if (!names.ContainsKey(nameTriple))
            {
                LongEventHandler.ExecuteWhenFinished(() =>
                {
                    Pawn pawn = PawnsFinder.All_AliveOrDead.FirstOrDefault(p => p.Name is NameTriple name && name.First == nameTriple.First && name.Last == nameTriple.Last);
                    if (pawn != null || Scribe.mode == LoadSaveMode.Inactive)
                    {
                        names[nameTriple] = pawn;
                    }
                });
                return null;
            }
            return names[nameTriple];
        }

        public static void ClearCache()
        {
            names.Clear();
        }
    }
}
