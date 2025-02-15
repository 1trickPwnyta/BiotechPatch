using HarmonyLib;
using RimWorld;
using Verse;
using Verse.AI;

namespace BiotechPatch.BreastfeedingCanBeInterrupted
{
    [HarmonyPatch(typeof(ChildcareUtility))]
    [HarmonyPatch(nameof(ChildcareUtility.MakeBreastfeedJob))]
    public static class Patch_ChildcareUtility_MakeBreastfeedJob
    {
        public static void Postfix(ref Job __result)
        {
            if (BiotechPatchSettings.BreastfeedingCanBeInterrupted)
            {
                __result.expiryInterval = 1200;
                __result.checkOverrideOnExpire = true;
            }
        }
    }

    [HarmonyPatch(typeof(ChildcareUtility))]
    [HarmonyPatch(nameof(ChildcareUtility.CanSuckleNow))]
    public static class Patch_ChildcareUtility_CanSuckleNow
    {
        public static void Postfix(Pawn baby, ref bool __result, ref ChildcareUtility.BreastfeedFailReason? reason)
        {
            if (BiotechPatchSettings.BreastfeedingCanBeInterrupted && baby.CarriedBy != null)
            {
                __result = true;
                reason = null;
            }
        }
    }

    [HarmonyPatch(typeof(ChildcareUtility))]
    [HarmonyPatch(nameof(ChildcareUtility.FindAutofeedBaby))]
    public static class Patch_ChildcareUtility_FindAutofeedBaby
    {
        public static void Postfix(Pawn mom, ref Thing food, ref Pawn __result)
        {
            if (BiotechPatchSettings.BreastfeedingCanBeInterrupted)
            {
                Pawn baby = mom.carryTracker.CarriedThing as Pawn;
                if (baby != null && baby.mindState.AutofeedSetting(mom) == AutofeedMode.Urgent)
                {
                    __result = baby;
                    food = mom;
                }
            }
        }
    }
}
