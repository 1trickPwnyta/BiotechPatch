using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Reflection;

namespace BiotechPatch.MechsOutsideRadius
{
    [HarmonyPatch(typeof(FloatMenuMakerMap))]
    [HarmonyPatch("GotoLocationOption")]
    public static class Patch_FloatMenuMakerMap
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
}
