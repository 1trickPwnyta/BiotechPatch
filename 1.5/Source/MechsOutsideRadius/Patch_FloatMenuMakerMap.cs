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
}
