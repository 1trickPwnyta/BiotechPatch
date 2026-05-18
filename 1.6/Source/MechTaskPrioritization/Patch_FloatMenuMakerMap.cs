using HarmonyLib;
using RimWorld;
using SpecialSauce.ModSettings;
using SpecialSauce.Multipatch;

namespace BiotechPatch.MechTaskPrioritization
{
    [HarmonyPatch_Compatibility(SpecialMod_Multipatch_Biotech.PACKAGE_ID, Settings.MechTaskPrioritization)]
    [HarmonyPatch(typeof(FloatMenuOptionProvider_WorkGivers))]
    [HarmonyPatch("MechanoidCanDo")]
    [HarmonyPatch(MethodType.Getter)]
    public static class Patch_FloatMenuOptionProvider_WorkGivers
    {
        public static void Postfix(ref bool __result)
        {
            if (Settings.MechTaskPrioritization.Enabled())
            {
                __result = true;
            }
        }
    }
}