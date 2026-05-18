using HarmonyLib;
using RimWorld;
using SpecialSauce.ModSettings;
using SpecialSauce.Multipatch;

namespace BiotechPatch.MechTaskPrioritization
{
    [HarmonyPatch_Compatibility(SpecialMod_Multipatch_Biotech.PACKAGE_ID, Settings.MechTaskPrioritization)]
    [HarmonyPatch(typeof(FloatMenuOptionProvider_DraftedRepair))]
    [HarmonyPatch("AppliesInt")]
    public static class Patch_FloatMenuOptionProvider_DraftedRepair
    {
        public static void Postfix(FloatMenuContext context, ref bool __result)
        {
            if (!__result && Settings.MechTaskPrioritization.Enabled())
            {
                if (context.FirstSelectedPawn.IsColonyMech && context.FirstSelectedPawn.def.race.mechEnabledWorkTypes.Contains(WorkTypeDefOf.Construction))
                {
                    __result = true;
                }
            }
        }
    }
}
