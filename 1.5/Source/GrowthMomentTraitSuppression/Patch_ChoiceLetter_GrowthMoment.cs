using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using Verse;

namespace BiotechPatch.GrowthMomentTraitSuppression
{
    [HarmonyPatch(typeof(ChoiceLetter_GrowthMoment))]
    [HarmonyPatch(nameof(ChoiceLetter_GrowthMoment.MakeChoices))]
    public static class Patch_ChoiceLetter_GrowthMoment
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            CodeInstruction instruction = instructions.ElementAt(instructions.FirstIndexOf(i => i.opcode == OpCodes.Callvirt && (MethodInfo)i.operand == typeof(TraitSet).Method(nameof(TraitSet.GainTrait))) - 1);
            instruction.opcode = OpCodes.Ldsfld;
            instruction.operand = typeof(BiotechPatchSettings).Field(nameof(BiotechPatchSettings.GrowthMomentTraitSuppression));
            return instructions;
        }
    }
}
