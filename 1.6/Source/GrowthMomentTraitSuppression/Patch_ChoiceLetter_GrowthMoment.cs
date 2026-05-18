using HarmonyLib;
using RimWorld;
using SpecialSauce.ModSettings;
using SpecialSauce.Multipatch;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using Verse;

namespace BiotechPatch.GrowthMomentTraitSuppression
{
    [HarmonyPatch_Compatibility(SpecialMod_Multipatch_Biotech.PACKAGE_ID, Settings.GrowthMomentTraitSuppression)]
    [HarmonyPatch(typeof(ChoiceLetter_GrowthMoment))]
    [HarmonyPatch(nameof(ChoiceLetter_GrowthMoment.MakeChoices))]
    public static class Patch_ChoiceLetter_GrowthMoment
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            CodeInstruction instruction = instructions.ElementAt(instructions.FirstIndexOf(i => i.opcode == OpCodes.Callvirt && (MethodInfo)i.operand == typeof(TraitSet).Method(nameof(TraitSet.GainTrait))) - 1);
            instruction.opcode = OpCodes.Call;
            instruction.operand = typeof(Patch_ChoiceLetter_GrowthMoment).PropertyGetter(nameof(GrowthMomentTraitSuppression));
            return instructions;
        }

        private static bool GrowthMomentTraitSuppression => Settings.GrowthMomentTraitSuppression.Enabled();
    }
}
