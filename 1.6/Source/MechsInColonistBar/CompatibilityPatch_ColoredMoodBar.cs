using ColoredMoodBar13;
using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using Verse;

namespace BiotechPatch.MechsInColonistBar
{
    [StaticConstructorOnStartup]
    public static class CompatibilityPatch_ColoredMoodBar
    {
        static CompatibilityPatch_ColoredMoodBar()
        {
            Type type = AccessTools.TypeByName("ColoredMoodBar13.MoodCache");
            if (type != null)
            {
                var harmony = new Harmony(BiotechPatchMod.PACKAGE_ID);
                harmony.Patch(type.Method("DoMood"), transpiler: typeof(CompatibilityPatch_ColoredMoodBar_DoMood).Method(nameof(CompatibilityPatch_ColoredMoodBar_DoMood.Transpiler)));
            }
        }
    }

    public static class CompatibilityPatch_ColoredMoodBar_DoMood
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator il)
        {
            List<CodeInstruction> instructionsList = instructions.ToList();

            Label trueLabel = il.DefineLabel();
            instructionsList.InsertRange(3, new[]
            {
                new CodeInstruction(OpCodes.Ldarg_1),
                new CodeInstruction(OpCodes.Call, typeof(Pawn).PropertyGetter(nameof(Pawn.IsColonyMech))),
                new CodeInstruction(OpCodes.Brtrue_S, trueLabel)
            });

            int index = instructionsList.FindIndex(i => i.Calls(typeof(Need).PropertyGetter(nameof(Need.CurLevel))));
            instructionsList[index - 6].labels.Add(trueLabel);
            instructionsList[index - 1] = new CodeInstruction(OpCodes.Call, typeof(CompatibilityPatch_ColoredMoodBar_DoMood).Method(nameof(GetMoodNeed)));

            index = instructionsList.FindIndex(i => i.Calls(typeof(Need).PropertyGetter(nameof(Need.CurLevelPercentage))));
            instructionsList[index - 1] = new CodeInstruction(OpCodes.Call, typeof(CompatibilityPatch_ColoredMoodBar_DoMood).Method(nameof(GetMoodNeed)));

            Label nonMechLabel = il.DefineLabel();
            index = instructionsList.FindLastIndex(i => i.LoadsField(typeof(MoodCache).Field("BreakThresholdExtreme")));
            instructionsList[index].labels.Add(nonMechLabel);
            instructionsList.InsertRange(index, new[]
            {
                new CodeInstruction(OpCodes.Ldarg_1),
                new CodeInstruction(OpCodes.Ldarg_0),
                new CodeInstruction(OpCodes.Call, typeof(CompatibilityPatch_ColoredMoodBar_DoMood).Method(nameof(SetParametersForEnergy))),
                new CodeInstruction(OpCodes.Brfalse_S, nonMechLabel),
                new CodeInstruction(OpCodes.Pop),
                new CodeInstruction(OpCodes.Pop),
                new CodeInstruction(OpCodes.Ret)
            });

            return instructionsList;
        }

        private static Need GetMoodNeed(Pawn_NeedsTracker needs) => needs.mood as Need ?? needs.energy;

        private static bool SetParametersForEnergy(Pawn pawn, MoodCache cache)
        {
            if (pawn.IsColonyMech)
            {
                MechanitorControlGroup group = pawn.GetMechControlGroup();
                if (group == null || pawn.needs.energy.CurLevelPercentage > group.mechRechargeThresholds.min)
                {
                    cache.MoodLevel = MoodLevel.Neutral;
                    cache.MoodTexture = Main.neutralTex;
                    cache.MoodColor = Main.Settings.Neutral;
                }
                else if (pawn.needs.energy.CurLevelPercentage > group.mechRechargeThresholds.min / 2)
                {
                    cache.MoodLevel = MoodLevel.Minor;
                    cache.MoodTexture = Main.minorBreakTex;
                    cache.MoodColor = Main.Settings.Minor;
                }
                else if (pawn.needs.energy.CurLevelPercentage > 0f)
                {
                    cache.MoodLevel = MoodLevel.Major;
                    cache.MoodTexture = Main.majorBreakTex;
                    cache.MoodColor = Main.Settings.Major;
                }
                else
                {
                    cache.MoodLevel = MoodLevel.Extreme;
                    cache.MoodTexture = Main.extremeBreakTex;
                    cache.MoodColor = Main.Settings.Extreme;
                }
                return true;
            }
            return false;
        }
    }
}
