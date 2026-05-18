using HarmonyLib;
using RimWorld;
using SpecialSauce.ModSettings;
using SpecialSauce.Multipatch;
using Verse;

namespace BiotechPatch.MoveBabyToSaferTempLater
{
    [HarmonyPatch_Compatibility(SpecialMod_Multipatch_Biotech.PACKAGE_ID, Settings.MoveBabyToSaferTempLater)]
    [HarmonyPatch(typeof(ChildcareUtility))]
    [HarmonyPatch(nameof(ChildcareUtility.BabyNeedsMovingForTemperatureReasons))]
    public static class Patch_ChildcareUtility
    {
        public static void Postfix(Pawn baby, ref bool __result)
        {
            if (Settings.MoveBabyToSaferTempLater.Enabled() && __result)
            {
                if (!baby.health.hediffSet.HasTemperatureInjury(TemperatureInjuryStage.Initial))
                {
                    __result = false;
                }
            }
        }
    }
}
