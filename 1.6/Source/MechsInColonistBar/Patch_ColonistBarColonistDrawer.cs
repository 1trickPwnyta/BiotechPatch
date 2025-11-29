using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using UnityEngine;
using Verse;

namespace BiotechPatch.MechsInColonistBar
{
    [HarmonyPatch(typeof(ColonistBarColonistDrawer))]
    [HarmonyPatch(nameof(ColonistBarColonistDrawer.DrawColonist))]
    public static class Patch_ColonistBarColonistDrawer
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator il)
        {
            List<CodeInstruction> instructionsList = instructions.ToList();

            Label label = il.DefineLabel();
            int index = instructionsList.FindIndex(i => i.Calls(typeof(GUI).Method(nameof(GUI.DrawTexture), new[] { typeof(Rect), typeof(Texture) })));
            instructionsList.InsertRange(index + 4, new[]
            {
                new CodeInstruction(OpCodes.Ldarg_2),
                new CodeInstruction(OpCodes.Call, typeof(Pawn).PropertyGetter(nameof(Pawn.IsColonyMech))),
                new CodeInstruction(OpCodes.Brtrue_S, label)
            });

            index = instructionsList.FindIndex(i => i.Calls(typeof(Need).PropertyGetter(nameof(Need.CurLevelPercentage))));
            instructionsList[index - 9].labels.Add(label);
            instructionsList[index - 1] = new CodeInstruction(OpCodes.Call, typeof(Patch_ColonistBarColonistDrawer).Method(nameof(GetMoodNeed)));

            index = instructionsList.FindIndex(i => i.Calls(typeof(MoodThresholdExtensions).Method(nameof(MoodThresholdExtensions.CurrentMoodThresholdFor))));
            instructionsList.InsertRange(index - 1, new[]
            {
                new CodeInstruction(OpCodes.Pop),
                new CodeInstruction(OpCodes.Ldarg_2),
                new CodeInstruction(OpCodes.Call, typeof(Patch_ColonistBarColonistDrawer).Method(nameof(ShouldShowMechEnergy)))
            });

            index = instructionsList.FindIndex(i => i.Calls(typeof(MoodThresholdExtensions).Method(nameof(MoodThresholdExtensions.CurrentMoodThresholdFor))));
            instructionsList.InsertRange(index + 1, new[]
            {
                new CodeInstruction(OpCodes.Ldarg_2),
                new CodeInstruction(OpCodes.Call, typeof(Patch_ColonistBarColonistDrawer).Method(nameof(GetMoodThreshold)))
            });

            return instructionsList;
        }

        private static Need GetMoodNeed(Pawn_NeedsTracker needs) => needs.mood as Need ?? needs.energy;

        private static bool ShouldShowMechEnergy(Pawn pawn) => Prefs.VisibleMood && pawn.IsColonyMech && !pawn.Dead;

        private static MoodThreshold GetMoodThreshold(MoodThreshold original, Pawn pawn)
        {
            if (pawn.IsColonyMech)
            {
                MechanitorControlGroup group = pawn.GetMechControlGroup();
                if (group == null || pawn.needs.energy.CurLevelPercentage > group.mechRechargeThresholds.min)
                {
                    return MoodThreshold.None;
                }
                if (pawn.needs.energy.CurLevelPercentage > group.mechRechargeThresholds.min / 2)
                {
                    return MoodThreshold.Minor;
                }
                if (pawn.needs.energy.CurLevelPercentage > 0f)
                {
                    return MoodThreshold.Major;
                }
                return MoodThreshold.Extreme;
            }
            else
            {
                return original;
            }
        }
    }
}
