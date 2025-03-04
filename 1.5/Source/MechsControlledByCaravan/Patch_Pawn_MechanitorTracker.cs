using HarmonyLib;
using RimWorld;
using RimWorld.Planet;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Verse;

namespace BiotechPatch.MechsControlledByCaravan
{
    [HarmonyPatch(typeof(Pawn_MechanitorTracker))]
    [HarmonyPatch("get_CanControlMechs")]
    public static class Patch_Pawn_MechanitorTracker
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            foreach (CodeInstruction instruction in instructions)
            {
                if (instruction.opcode == OpCodes.Callvirt && instruction.operand is MethodInfo info && info == typeof(Thing).Method("get_Spawned"))
                {
                    instruction.operand = typeof(PatchUtility_Pawn_MechanitorTracker).Method(nameof(PatchUtility_Pawn_MechanitorTracker.SpawnedOrInCaravan));
                }

                yield return instruction;
            }
        }
    }

    public static class PatchUtility_Pawn_MechanitorTracker
    {
        public static bool SpawnedOrInCaravan(this Pawn pawn)
        {
            return pawn.Spawned || (BiotechPatchSettings.MechsControlledByCaravan && pawn.IsCaravanMember());
        }
    }
}
