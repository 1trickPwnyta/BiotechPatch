using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine;
using Verse;

namespace BiotechPatch.GrowthMomentChoiceColors
{
    [HarmonyPatch(typeof(Dialog_GrowthMomentChoices))]
    [HarmonyPatch("DrawTraitChoices")]
    public static class Patch_Dialog_GrowthMomentChoices_DrawTraitChoices
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            CodeInstruction radioInstruction = instructions.First(i => i.opcode == OpCodes.Callvirt && i.operand is MethodInfo info && info == typeof(Listing_Standard).Method(nameof(Listing_Standard.RadioButton), new[] { typeof(string), typeof(bool), typeof(float), typeof(string), typeof(float?) }));

            foreach (CodeInstruction instruction in instructions)
            {
                if (instruction.opcode == OpCodes.Callvirt && (MethodInfo)instruction.operand == typeof(Trait).Method(nameof(Trait.TipString)))
                {
                    instruction.opcode = OpCodes.Call;
                    instruction.operand = typeof(PatchUtility_Dialog_GrowthMomentChoices).Method(nameof(PatchUtility_Dialog_GrowthMomentChoices.GetTipString));
                }

                if (instruction == radioInstruction)
                {
                    yield return new CodeInstruction(OpCodes.Ldloc_S, 5);
                    yield return new CodeInstruction(OpCodes.Ldarg_0);
                    yield return new CodeInstruction(OpCodes.Ldfld, typeof(Dialog_GrowthMomentChoices).Field("letter"));
                    yield return new CodeInstruction(OpCodes.Ldfld, typeof(ChoiceLetter_GrowthMoment).Field(nameof(ChoiceLetter_GrowthMoment.pawn)));
                    yield return new CodeInstruction(OpCodes.Call, typeof(PatchUtility_Dialog_GrowthMomentChoices).Method(nameof(PatchUtility_Dialog_GrowthMomentChoices.SetColor), new[] { typeof(Trait), typeof(Pawn) }));
                    yield return instruction;
                    yield return new CodeInstruction(OpCodes.Call, typeof(PatchUtility_Dialog_GrowthMomentChoices).Method(nameof(PatchUtility_Dialog_GrowthMomentChoices.ResetColor)));
                    continue;
                }

                yield return instruction;
            }
        }
    }

    [HarmonyPatch(typeof(Dialog_GrowthMomentChoices))]
    [HarmonyPatch("DrawPassionChoices")]
    public static class Patch_Dialog_GrowthMomentChoices_DrawPassionChoices
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            CodeInstruction radioInstruction = instructions.First(i => i.opcode == OpCodes.Callvirt && i.operand is MethodInfo info && info == typeof(Listing_Standard).Method(nameof(Listing_Standard.RadioButton), new[] { typeof(string), typeof(bool), typeof(float), typeof(string), typeof(float?) }));

            foreach (CodeInstruction instruction in instructions)
            {
                if (instruction == radioInstruction || instruction.opcode == OpCodes.Callvirt && instruction.operand is MethodInfo info && info == typeof(Listing_Standard).Method(nameof(Listing_Standard.CheckboxLabeled), new[] { typeof(string), typeof(bool).MakeByRefType(), typeof(float) }))
                {
                    yield return new CodeInstruction(OpCodes.Ldloc_S, 5);
                    yield return new CodeInstruction(OpCodes.Ldarg_0);
                    yield return new CodeInstruction(OpCodes.Ldfld, typeof(Dialog_GrowthMomentChoices).Field("letter"));
                    yield return new CodeInstruction(OpCodes.Ldfld, typeof(ChoiceLetter_GrowthMoment).Field(nameof(ChoiceLetter_GrowthMoment.pawn)));
                    yield return new CodeInstruction(OpCodes.Call, typeof(PatchUtility_Dialog_GrowthMomentChoices).Method(nameof(PatchUtility_Dialog_GrowthMomentChoices.SetColor), new[] { typeof(SkillDef), typeof(Pawn) }));
                    yield return instruction;
                    yield return new CodeInstruction(OpCodes.Call, typeof(PatchUtility_Dialog_GrowthMomentChoices).Method(nameof(PatchUtility_Dialog_GrowthMomentChoices.ResetColor)));
                    continue;
                }

                yield return instruction;
            }
        }
    }

    public static class PatchUtility_Dialog_GrowthMomentChoices
    {
        public static Color guiColor = Color.white;

        public static void ResetColor()
        {
            guiColor = Color.white;
            GUI.color = Color.white;
        }

        public static void SetColor(Trait trait, Pawn pawn)
        {
            guiColor = Color.white;
            if (pawn.GetSuppressingGenes(trait).Count > 0)
            {
                guiColor = ColoredText.SubtleGrayColor;
            }
        }

        public static void SetColor(SkillDef skill, Pawn pawn)
        {
            GUI.color = Color.white;
            if (pawn.GetConflictingGenes(skill).Count > 0)
            {
                GUI.color = ColoredText.WarningColor;
            }
        }

        public static string GetTipString(this Trait trait, Pawn pawn)
        {
            List<Gene> suppressingGenes = pawn.GetSuppressingGenes(trait);
            string result = trait.TipString(pawn);
            if (suppressingGenes.Count > 0) 
            {
                result += "\n\n" + ("BiotechPatch_SuppressedBy".Translate() + ":\n  - " + string.Join("\n  - ", suppressingGenes.Select(g => g.LabelCap))).Colorize(ColoredText.SubtleGrayColor);
            }
            return result;
        }

        private static List<Gene> GetSuppressingGenes(this Pawn pawn, Trait trait)
        {
            if (BiotechPatchSettings.GrowthMomentChoiceColors && BiotechPatchSettings.GrowthMomentTraitSuppression && pawn.genes != null)
            {
                return pawn.genes.GenesListForReading.Where(g => g.Active && g.def.suppressedTraits != null && g.def.suppressedTraits.Any(d => d.def == trait.def && d.degree == trait.Degree)).ToList();
            }
            else
            {
                return new List<Gene>();
            }
        }

        private static List<Gene> GetConflictingGenes(this Pawn pawn, SkillDef skill)
        {
            if (BiotechPatchSettings.GrowthMomentChoiceColors && pawn.genes != null)
            {
                return pawn.genes.GenesListForReading.Where(g => g.Active && g.def.passionMod != null && g.def.passionMod.modType == PassionMod.PassionModType.DropAll && g.def.passionMod.skill == skill).ToList();
            }
            else
            {
                return new List<Gene>();
            }
        }
    }
}
