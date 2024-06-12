using HarmonyLib;
using System.Collections.Generic;
using System.Reflection.Emit;
using Verse;

namespace BiotechPatch.HemogenExtractionSpam
{
    [HarmonyPatch(typeof(RecipeWorker))]
    [HarmonyPatch("ReportViolation")]
    public static class Patch_RecipeWorker_ReportViolation
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            foreach (CodeInstruction instruction in instructions)
            {
                if (instruction.opcode == OpCodes.Ldc_I4_1)
                {
                    yield return new CodeInstruction(OpCodes.Ldarg_3);
                    yield return new CodeInstruction(OpCodes.Ldarg_S, 5);
                    yield return new CodeInstruction(OpCodes.Call, BiotechPatchRefs.m_GoodwillUtility_ShouldSendMessage);
                    continue;
                }

                yield return instruction;
            }
        }
    }
}
