using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using Verse;

namespace BiotechPatch.HemogenFarmAnyone
{
    [HarmonyPatch(typeof(Alert_AwaitingMedicalOperation))]
    [HarmonyPatch("AwaitingMedicalOperation")]
    [HarmonyPatch(MethodType.Getter)]
    public static class Patch_Alert_AwaitingMedicalOperation
    {
        public static void Postfix(List<Pawn> __result)
        {
            __result.RemoveWhere(p => p.health.surgeryBills.Count == 1 && p.health.surgeryBills[0].recipe == RecipeDefOf.ExtractHemogenPack && p.TryGetComp<Comp_HemogenFarm>()?.HemogenFarmEnabled == true);
        }
    }
}
