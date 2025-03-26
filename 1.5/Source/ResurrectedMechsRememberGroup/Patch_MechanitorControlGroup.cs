using HarmonyLib;
using RimWorld;
using Verse;

namespace BiotechPatch.ResurrectedMechsRememberGroup
{
    [HarmonyPatch(typeof(MechanitorControlGroup))]
    [HarmonyPatch(nameof(MechanitorControlGroup.TryUnassign))]
    public static class Patch_MechanitorControlGroup_TryUnassign
    {
        public static void Postfix(MechanitorControlGroup __instance, Pawn pawn, bool __result)
        {
            if (__result)
            {
                pawn.SetLastControlGroupIndex(__instance.Tracker.Pawn, __instance.Index - 1);
            }
        }
    }

    [HarmonyPatch(typeof(MechanitorControlGroup))]
    [HarmonyPatch(nameof(MechanitorControlGroup.Assign))]
    public static class Patch_MechanitorControlGroup_Assign
    {
        public static void Postfix(MechanitorControlGroup __instance, Pawn pawn)
        {
            pawn.SetLastControlGroupIndex(__instance.Tracker.Pawn, __instance.Index - 1);
        }
    }
}
