using HarmonyLib;
using RimWorld;
using SpecialSauce.ModSettings;
using SpecialSauce.Multipatch;
using Verse;

namespace BiotechPatch.KillThirstSatisfiedInAnyMelee
{
    [HarmonyPatch_Compatibility(SpecialMod_Multipatch_Biotech.PACKAGE_ID, Settings.KillThirstSatisfiedInAnyMelee)]
    [HarmonyPatch(typeof(Need_KillThirst))]
    [HarmonyPatch(nameof(Need_KillThirst.Notify_KilledPawn))]
    public static class Patch_Need_KillThirst
    {
        public static void Postfix(Need_KillThirst __instance, DamageInfo? dinfo)
        {
            if (Settings.KillThirstSatisfiedInAnyMelee.Enabled() && dinfo?.Def.isRanged == false)
            {
                __instance.CurLevel = 1f;
            }
        }
    }
}
