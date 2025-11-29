using HarmonyLib;
using RimWorld;
using System;
using UnityEngine;

namespace BiotechPatch.HemogenFarmAnyone
{
    [HarmonyPatch(typeof(ITab_Pawn_Health))]
    [HarmonyPatch(MethodType.Constructor)]
    [HarmonyPatch(new Type[] { })]
    public static class Patch_ITab_Pawn_Health
    {
        public static void Postfix(ref Vector2 ___size)
        {
            if (BiotechPatchSettings.HemogenFarmAnyone)
            {
                ___size.y += 23f;
            }
        }
    }
}
