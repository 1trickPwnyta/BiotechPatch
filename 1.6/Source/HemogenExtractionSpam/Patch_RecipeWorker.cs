using HarmonyLib;
using RimWorld;
using SpecialSauce.ModSettings;
using SpecialSauce.Multipatch;
using System.Collections.Generic;
using System.Reflection.Emit;
using Verse;

namespace BiotechPatch.HemogenExtractionSpam
{
    [HarmonyPatch_Compatibility(SpecialMod_Multipatch_Biotech.PACKAGE_ID, Settings.HemogenExtractionSpam)]
    [HarmonyPatch(typeof(RecipeWorker))]
    [HarmonyPatch("ReportViolation")]
    public static class Patch_RecipeWorker_ReportViolation
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            foreach (CodeInstruction instruction in instructions)
            {
                if (instruction.opcode == OpCodes.Ldc_I4_1)
                {
                    yield return new CodeInstruction(OpCodes.Ldarg_3);
                    yield return new CodeInstruction(OpCodes.Ldarg_S, 5);
                    yield return new CodeInstruction(OpCodes.Call, typeof(Patch_RecipeWorker_ReportViolation).Method(nameof(ShouldSendMessage)));
                    continue;
                }

                yield return instruction;
            }
        }

        private static bool ShouldSendMessage(Faction factionToInform, HistoryEventDef def)
        {
            return !Settings.HemogenExtractionSpam.Enabled() || def != HistoryEventDefOf.ExtractedHemogenPack || !factionToInform.HostileTo(Faction.OfPlayer);
        }
    }
}
