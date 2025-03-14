using HarmonyLib;
using RimWorld;

namespace BiotechPatch.MechSmartWorkMode
{
    [HarmonyPatch(typeof(Pawn_MechanitorTracker))]
    [HarmonyPatch("Notify_ControlGroupAmountMayChanged")]
    public static class Patch_Pawn_MechanitorTracker
    {
        public static void Prefix(Pawn_MechanitorTracker __instance)
        {
            if (BiotechPatchSettings.MechSmartWorkMode && __instance.controlGroups.Count == 0 && __instance.TotalAvailableControlGroups > 1)
            {
                MechanitorControlGroup smartWorkGroup = new MechanitorControlGroup(__instance);
                smartWorkGroup.SetWorkMode(MechWorkModeDefOf.SmartWork);
                __instance.controlGroups.Add(smartWorkGroup);
                MechanitorControlGroup shutdownGroup = new MechanitorControlGroup(__instance);
                smartWorkGroup.SetWorkMode(RimWorld.MechWorkModeDefOf.SelfShutdown);
                __instance.controlGroups.Add(shutdownGroup);
            }
        }
    }
}
