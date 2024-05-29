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
            bool foundBiotech = false;
            bool finished = false;

            foreach (CodeInstruction instruction in instructions)
            {
                if (instruction.opcode == OpCodes.Call && (MethodInfo)instruction.operand == BiotechPatchRefs.m_ModsConfig_get_BiotechActive)
                {
                    foundBiotech = true;
                    continue;
                }
                
                if (foundBiotech && !finished && instruction.opcode == OpCodes.Brfalse_S)
                {
                    instruction.opcode = OpCodes.Br;
                    finished = true;
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
            bool foundBiotech = false;
            bool finished = false;

            foreach (CodeInstruction instruction in instructions)
            {
                if (instruction.opcode == OpCodes.Call && (MethodInfo)instruction.operand == BiotechPatchRefs.m_ModsConfig_get_BiotechActive)
                {
                    foundBiotech = true;
                    instruction.opcode = OpCodes.Nop;
                    instruction.operand = null;
                }

                if (foundBiotech && !finished && instruction.opcode == OpCodes.Brfalse_S)
                {
                    instruction.opcode = OpCodes.Br;
                    finished = true;
                }

                yield return instruction;
            }
        }
    }
}
