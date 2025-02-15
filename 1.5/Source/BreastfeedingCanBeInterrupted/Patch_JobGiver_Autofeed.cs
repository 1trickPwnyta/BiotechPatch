using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Verse;

namespace BiotechPatch.BreastfeedingCanBeInterrupted
{
    [HarmonyPatch(typeof(JobGiver_Autofeed))]
    [HarmonyPatch("TryGiveJob")]
    public static class Patch_JobGiver_Autofeed
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            foreach (CodeInstruction instruction in instructions)
            {
                if (instruction.opcode == OpCodes.Callvirt && instruction.operand is MethodInfo info && info == typeof(Thing).Method("get_Spawned"))
                {
                    yield return new CodeInstruction(OpCodes.Ldloc_1);
                    yield return new CodeInstruction(OpCodes.Call, typeof(PatchUtility_JobGiver_Autofeed).Method(nameof(PatchUtility_JobGiver_Autofeed.SpawnedOrCarriedByBreastfeeder)));
                    continue;
                }

                yield return instruction;
            }
        }
    }

    public static class PatchUtility_JobGiver_Autofeed
    {
        public static bool SpawnedOrCarriedByBreastfeeder(this Pawn baby, Thing lactator)
        {
            if (BiotechPatchSettings.BreastfeedingCanBeInterrupted)
            {
                return baby.Spawned || baby.CarriedBy == lactator;
            }
            else
            {
                return baby.Spawned;
            }
        }
    }
}
