using HarmonyLib;
using RimWorld;

namespace BiotechPatch.MechTaskPrioritization
{
    [HarmonyPatch(typeof(FloatMenuOptionProvider_DraftedRepair))]
    [HarmonyPatch("AppliesInt")]
    public static class Patch_FloatMenuOptionProvider_DraftedRepair
    {
        public static void Postfix(FloatMenuContext context, ref bool __result)
        {
            if (!__result && BiotechPatchSettings.MechTaskPrioritization)
            {
                if (context.FirstSelectedPawn.IsColonyMech && context.FirstSelectedPawn.def.race.mechEnabledWorkTypes.Contains(WorkTypeDefOf.Construction))
                {
                    __result = true;
                }
            }
        }
    }
}
