using HarmonyLib;
using RimWorld;
using Verse;

namespace BiotechPatch.ResurrectedMechsRememberGroup
{
    [HarmonyPatch(typeof(Pawn_MechanitorTracker))]
    [HarmonyPatch(nameof(Pawn_MechanitorTracker.AssignPawnControlGroup))]
    public static class Patch_Pawn_MechanitorTracker
    {
        public static bool Prefix(Pawn_MechanitorTracker __instance, Pawn pawn)
        {
            if (BiotechPatchSettings.ResurrectedMechsRememberGroup)
            {
                Pawn mech = pawn, mechanitor = __instance.Pawn;
                int index = mech.GetLastControlGroupIndex(mechanitor);
                if (index >= 0)
                {
                    if (__instance.controlGroups.Count > index)
                    {
                        __instance.controlGroups[index].Assign(mech);
                        __instance.Notify_BandwidthChanged();
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
