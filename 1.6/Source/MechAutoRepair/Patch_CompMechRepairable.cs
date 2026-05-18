using HarmonyLib;
using RimWorld;
using SpecialSauce.ModSettings;
using SpecialSauce.Multipatch;
using System;

namespace BiotechPatch.MechAutoRepair
{
    [HarmonyPatch_Compatibility(SpecialMod_Multipatch_Biotech.PACKAGE_ID, Settings.MechAutoRepair)]
    [HarmonyPatch(typeof(CompMechRepairable))]
    [HarmonyPatch(MethodType.Constructor)]
    [HarmonyPatch(new Type[] { })]
    public static class Patch_CompMechRepairable
    {
        public static void Postfix(CompMechRepairable __instance)
        {
            if (Settings.MechAutoRepair.Enabled())
            {
                __instance.autoRepair = true;
            }
        }
    }
}
