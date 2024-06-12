using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace BiotechPatch.MechsOutsideRadius
{
    [HarmonyPatch(typeof(MultiPawnGotoController))]
    [HarmonyPatch("RecomputeDestinations")]
    public static class Patch_MultiPawnGotoController_RecomputeDestinations
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            foreach (CodeInstruction instruction in instructions)
            {
                if (instruction.opcode == OpCodes.Call && (MethodInfo)instruction.operand == BiotechPatchRefs.m_ModsConfig_get_BiotechActive)
                {
                    yield return new CodeInstruction(OpCodes.Ldsfld, BiotechPatchRefs.f_BiotechPatchSettings_MechsOutsideRadius);
                    yield return new CodeInstruction(OpCodes.Ldc_I4_1);
                    yield return new CodeInstruction(OpCodes.Xor);
                    continue;
                }

                yield return instruction;
            }
        }
    }

    [HarmonyPatch(typeof(MultiPawnGotoController))]
    [HarmonyPatch("<RecomputeDestinations>g__CanGoTo|27_0")]
    public static class Patch_MultiPawnGotoController_CanGoTo
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            foreach (CodeInstruction instruction in instructions)
            {
                if (instruction.opcode == OpCodes.Call && (MethodInfo)instruction.operand == BiotechPatchRefs.m_ModsConfig_get_BiotechActive)
                {
                    instruction.opcode = OpCodes.Ldsfld;
                    instruction.operand = BiotechPatchRefs.f_BiotechPatchSettings_MechsOutsideRadius;
                    yield return instruction;
                    yield return new CodeInstruction(OpCodes.Ldc_I4_1);
                    yield return new CodeInstruction(OpCodes.Xor);
                    continue;
                }

                yield return instruction;
            }
        }
    }
}
