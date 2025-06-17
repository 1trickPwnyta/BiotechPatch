using HarmonyLib;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine;
using Verse;

namespace BiotechPatch.GrowthMomentChoiceColors
{
    [HarmonyPatch(typeof(Widgets))]
    [HarmonyPatch(nameof(Widgets.RadioButtonLabeled))]
    public static class Patch_Widgets
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            foreach (CodeInstruction instruction in instructions)
            {
                if (instruction.opcode == OpCodes.Call && instruction.operand is MethodInfo && (MethodInfo)instruction.operand == typeof(Color).Method("get_white"))
                {
                    instruction.opcode = OpCodes.Ldsfld;
                    instruction.operand = typeof(PatchUtility_Dialog_GrowthMomentChoices).Field(nameof(PatchUtility_Dialog_GrowthMomentChoices.guiColor));
                }

                yield return instruction;
            }
        }
    }
}
