using HarmonyLib;
using RimWorld;
using Verse;

namespace BiotechPatch.MoveBabyToSaferTempLater
{
    [HarmonyPatch(typeof(ChildcareUtility))]
    [HarmonyPatch(nameof(ChildcareUtility.BabyNeedsMovingForTemperatureReasons))]
    public static class Patch_ChildcareUtility
    {
        public static void Postfix(Pawn baby, ref bool __result)
        {
            if (SpecialModSettings_Multipatch_Biotech.MoveBabyToSaferTempLater && __result)
            {
                if (!baby.health.hediffSet.HasTemperatureInjury(TemperatureInjuryStage.Initial))
                {
                    __result = false;
                }
            }
        }
    }
}
