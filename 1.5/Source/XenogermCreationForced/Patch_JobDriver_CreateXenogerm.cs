using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace BiotechPatch.XenogermCreationForced
{
    [HarmonyPatch(typeof(WorkGiver_CreateXenogerm))]
    [HarmonyPatch(nameof(WorkGiver_CreateXenogerm.JobOnThing))]
    public static class Patch_WorkGiver_CreateXenogerm
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            foreach (CodeInstruction instruction in instructions)
            {
                if (instruction.opcode == OpCodes.Ldc_I4 && (int)instruction.operand == 1200)
                {
                    yield return new CodeInstruction(OpCodes.Ldarg_3);
                    instruction.opcode = OpCodes.Call;
                    instruction.operand = typeof(PatchUtility_WorkGiver_CreateXenogerm).Method(nameof(PatchUtility_WorkGiver_CreateXenogerm.GetExpiryInterval));
                }

                yield return instruction;
            }
        }
    }

    public static class PatchUtility_WorkGiver_CreateXenogerm
    {
        public static int GetExpiryInterval(bool forced)
        {
            return BiotechPatchSettings.XenogermCreationForced && forced ? -1 : 1200;
        }
    }
}
