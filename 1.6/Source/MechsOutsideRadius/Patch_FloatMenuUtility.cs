using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Verse;

namespace BiotechPatch.MechsOutsideRadius
{
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
            return pawn.IsColonyMechPlayerControlled && !BiotechPatchSettings.MechsOutsideRadius;
        }
    }
}
