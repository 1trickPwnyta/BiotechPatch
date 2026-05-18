using HarmonyLib;
using SpecialSauce.ModSettings;
using SpecialSauce.Multipatch;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using Verse;

namespace BiotechPatch.ChildrenGoByFirstName
{
    [HarmonyPatch_Compatibility(SpecialMod_Multipatch_Biotech.PACKAGE_ID, Settings.ChildrenGoByFirstName)]
    [HarmonyPatch(typeof(NameTriple))]
    [HarmonyPatch("get_Nick")]
    public static class Patch_NameTriple_get_Nick
    {
        public static void Postfix(NameTriple __instance, ref string __result)
        {
            if (Settings.ChildrenGoByFirstName.Enabled() && ((string)typeof(NameTriple).Field("nickInt").GetValue(__instance)).NullOrEmpty() && __instance.GetPawn() != null && !__instance.GetPawn().DevelopmentalStage.Adult())
            {
                __result = __instance.First;
            }
        }
    }

    [HarmonyPatch_Compatibility(SpecialMod_Multipatch_Biotech.PACKAGE_ID, Settings.ChildrenGoByFirstName)]
    [HarmonyPatch(typeof(NameTriple))]
    [HarmonyPatch(nameof(NameTriple.Equals))]
    public static class Patch_NameTriple_Equals
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            return instructions.Select(i => i.operand != null && i.operand is MethodInfo info && info == typeof(NameTriple).Method("get_Nick") ? new CodeInstruction(OpCodes.Ldfld, typeof(NameTriple).Field("nickInt")) : i);
        }
    }

    [HarmonyPatch_Compatibility(SpecialMod_Multipatch_Biotech.PACKAGE_ID, Settings.ChildrenGoByFirstName)]
    [HarmonyPatch(typeof(NameTriple))]
    [HarmonyPatch(nameof(NameTriple.GetHashCode))]
    public static class Patch_NameTriple_GetHashCode
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            return instructions.Select(i => i.operand != null && i.operand is MethodInfo info && info == typeof(NameTriple).Method("get_Nick") ? new CodeInstruction(OpCodes.Ldfld, typeof(NameTriple).Field("nickInt")) : i);
        }
    }
}
