using HarmonyLib;
using RimWorld;
using SpecialSauce.ModSettings;
using SpecialSauce.Multipatch;
using System;
using UnityEngine;

namespace BiotechPatch.HemogenFarmAnyone
{
    [HarmonyPatch_Compatibility(SpecialMod_Multipatch_Biotech.PACKAGE_ID, Settings.HemogenFarmAnyone)]
    [HarmonyPatch(typeof(ITab_Pawn_Health))]
    [HarmonyPatch(MethodType.Constructor)]
    [HarmonyPatch(new Type[] { })]
    public static class Patch_ITab_Pawn_Health
    {
        public static void Postfix(ref Vector2 ___size)
        {
            if (Settings.HemogenFarmAnyone.Enabled())
            {
                ___size.y += 23f;
            }
        }
    }
}
