using HarmonyLib;
using LudeonTK;
using System.Collections.Generic;
using System.Reflection.Emit;
using Verse;

namespace BiotechPatch.AutoChildNicknamesDisabled
{
    [HarmonyPatch(typeof(Pawn_AgeTracker))]
    [HarmonyPatch("BirthdayBiological")]
    public static class Patch_Pawn_AgeTracker
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            foreach (CodeInstruction instruction in instructions)
            {
                if (instruction.opcode == OpCodes.Ldc_R4 && (float)instruction.operand == 0.5f)
                {
                    instruction.opcode = OpCodes.Call;
                    instruction.operand = typeof(PatchUtility_Pawn_AgeTracker).Method(nameof(PatchUtility_Pawn_AgeTracker.GetNicknameChance));
                }

                yield return instruction;
            }
        }
    }

    public static class PatchUtility_Pawn_AgeTracker
    {
#if DEBUG
        [DebugAction("Pawns", "Set pawn age in ticks", actionType = DebugActionType.ToolMapForPawns, allowedGameStates = AllowedGameStates.PlayingOnMap)]
        private static void SetPawnAge(Pawn p)
        {
            Find.WindowStack.Add(new Dialog_Input<long>("Enter pawn biological age in ticks", ticks =>
            {
                p.ageTracker.AgeBiologicalTicks = ticks;
            }, long.Parse, p.ageTracker.AgeBiologicalTicks));
        }
#endif

        public static float GetNicknameChance()
        {
            return BiotechPatchSettings.AutoChildNicknamesDisabled ? 0f : 0.5f;
        }
    }
}
