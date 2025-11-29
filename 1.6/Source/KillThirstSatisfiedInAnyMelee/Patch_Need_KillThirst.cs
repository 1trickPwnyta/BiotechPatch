using HarmonyLib;
using RimWorld;
using Verse;

namespace BiotechPatch.KillThirstSatisfiedInAnyMelee
{
    [HarmonyPatch(typeof(Need_KillThirst))]
    [HarmonyPatch(nameof(Need_KillThirst.Notify_KilledPawn))]
    public static class Patch_Need_KillThirst
    {
        public static void Postfix(Need_KillThirst __instance, DamageInfo? dinfo)
        {
            if (BiotechPatchSettings.KillThirstSatisfiedInAnyMelee && dinfo?.Def.isRanged == false)
            {
                __instance.CurLevel = 1f;
            }
        }
    }
}
