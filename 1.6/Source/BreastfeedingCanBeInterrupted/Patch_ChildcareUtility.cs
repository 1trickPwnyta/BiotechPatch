using HarmonyLib;
using RimWorld;
using SpecialSauce.ModSettings;
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
            if (Settings.BreastfeedingCanBeInterrupted.Enabled())
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
            if (Settings.BreastfeedingCanBeInterrupted.Enabled() && baby.CarriedBy != null)
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
            if (Settings.BreastfeedingCanBeInterrupted.Enabled())
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
