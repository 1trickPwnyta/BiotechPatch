using RimWorld;
using System.Linq;
using Verse;

namespace BiotechPatch.CustomHybridXenotypes
{
    // Patched manually in initializer
    public static class Patch_PregnancyUtility_TryGetInheritedXenotype
    {
        public static void Postfix(Pawn mother, Pawn father, ref XenotypeDef xenotype, ref bool __result)
        {
            if (PatchUtility_PregnancyUtility.ShouldBeHybrid(mother, father))
            {
                xenotype = null;
                __result = false;
            }
        }
    }

    // Patched manually in intializer
    public static class Patch_PregnancyUtility_ShouldByHybrid
    {
        public static void Postfix(Pawn mother, Pawn father, ref bool __result)
        {
            if (PatchUtility_PregnancyUtility.ShouldBeHybrid(mother, father))
            {
                __result = true;
            }
        }
    }

    public static class PatchUtility_PregnancyUtility
    {
        public static bool ShouldBeHybrid(Pawn mother, Pawn father)
        {
            if (BiotechPatchSettings.CustomHybridXenotypes)
            {
                Pawn customXenotypePawn = new[] { mother, father }.FirstOrDefault(p => p != null && p.genes != null && p.genes.CustomXenotype != null);
                if (customXenotypePawn != null)
                {
                    Pawn otherPawn = customXenotypePawn == mother ? father : mother;
                    if (otherPawn != null && (otherPawn.genes == null || otherPawn.genes.CustomXenotype != customXenotypePawn.genes.CustomXenotype))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
