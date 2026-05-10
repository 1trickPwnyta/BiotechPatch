using HarmonyLib;
using RimWorld;
using SpecialSauce.ModSettings;
using System;

namespace BiotechPatch.DeathrestAutoWake
{
    [HarmonyPatch(typeof(Gene_Deathrest))]
    [HarmonyPatch(MethodType.Constructor)]
    [HarmonyPatch(new Type[] { })]
    public static class Patch_Gene_Deathrest
    {
        public static void Postfix(Gene_Deathrest __instance)
        {
            if (Settings.DeathrestAutoWake.Enabled())
            {
                __instance.autoWake = true;
            }
        }
    }
}
