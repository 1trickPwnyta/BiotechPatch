using HarmonyLib;
using RimWorld;

namespace BiotechPatch.MechTaskPrioritization
{
    [HarmonyPatch(typeof(FloatMenuOptionProvider_WorkGivers))]
    [HarmonyPatch("MechanoidCanDo")]
    [HarmonyPatch(MethodType.Getter)]
    public static class Patch_FloatMenuOptionProvider_WorkGivers
    {
        public static void Postfix(ref bool __result)
        {
            if (SpecialModSettings_Multipatch_Biotech.MechTaskPrioritization)
            {
                __result = true;
            }
        }
    }
}