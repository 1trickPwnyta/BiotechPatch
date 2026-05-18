using HarmonyLib;
using RimWorld;
using SpecialSauce.ModSettings;
using SpecialSauce.Multipatch;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Verse;

namespace BiotechPatch.MechsOutsideRadius
{
    [HarmonyPatch_Compatibility(SpecialMod_Multipatch_Biotech.PACKAGE_ID, Settings.MechsOutsideRadius)]
    [HarmonyPatch(typeof(FloatMenuUtility))]
    [HarmonyPatch(nameof(FloatMenuUtility.GetRangedAttackAction))]
    public static class Patch_FloatMenuUtility
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            foreach (CodeInstruction instruction in instructions)
            {
                if (instruction.opcode == OpCodes.Callvirt && instruction.operand is MethodInfo info && info == typeof(Pawn).Method("get_IsColonyMechPlayerControlled"))
                {
                    instruction.operand = typeof(PatchUtility_FloatMenuUtility).Method(nameof(PatchUtility_FloatMenuUtility.IsColonyMechPlayerControlledWithCommandRangeApplied));
                }

                yield return instruction;
            }
        }
    }

    public static class PatchUtility_FloatMenuUtility
    {
        public static bool IsColonyMechPlayerControlledWithCommandRangeApplied(Pawn pawn)
        {
            return pawn.IsColonyMechPlayerControlled && !Settings.MechsOutsideRadius.Enabled();
        }
    }
}
