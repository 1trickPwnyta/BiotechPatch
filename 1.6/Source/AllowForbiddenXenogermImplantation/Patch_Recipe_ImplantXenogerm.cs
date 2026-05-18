using HarmonyLib;
using RimWorld;
using SpecialSauce.ModSettings;
using SpecialSauce.Multipatch;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Verse;

namespace BiotechPatch.AllowForbiddenXenogermImplantation
{
    [HarmonyPatch_Compatibility(SpecialMod_Multipatch_Biotech.PACKAGE_ID, Settings.AllowForbiddenXenogermImplantation)]
    [HarmonyPatch(typeof(Recipe_ImplantXenogerm))]
    [HarmonyPatch(nameof(Recipe_ImplantXenogerm.AvailableOnNow))]
    public static class Patch_Recipe_ImplantXenogerm
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            foreach (CodeInstruction instruction in instructions)
            {
                if (instruction.opcode == OpCodes.Call && (MethodInfo)instruction.operand == typeof(ForbidUtility).Method(nameof(ForbidUtility.IsForbidden), new[] { typeof(Thing), typeof(Pawn) }))
                {
                    instruction.operand = typeof(PatchUtility_Recipe_ImplantXenogerm).Method(nameof(PatchUtility_Recipe_ImplantXenogerm.IsForbidden));
                }

                yield return instruction;
            }
        }
    }

    public static class PatchUtility_Recipe_ImplantXenogerm
    {
        public static bool IsForbidden(Thing xenogerm, Pawn pawn)
        {
            return Settings.AllowForbiddenXenogermImplantation.Enabled() ? false : ForbidUtility.IsForbidden(xenogerm, pawn);
        }
    }
}
