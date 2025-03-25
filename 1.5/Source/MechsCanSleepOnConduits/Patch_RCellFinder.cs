using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Verse;

namespace BiotechPatch.MechsCanSleepOnConduits
{
    [HarmonyPatch(typeof(RCellFinder))]
    [HarmonyPatch("CanSelfShutdown")]
    public static class Patch_RCellFinder
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            foreach (CodeInstruction instruction in instructions)
            {
                if (instruction.opcode == OpCodes.Call && instruction.operand is MethodInfo info && info == typeof(GridsUtility).Method(nameof(GridsUtility.GetFirstBuilding)))
                {
                    instruction.operand = typeof(PatchUtility_RCellFinder).Method(nameof(PatchUtility_RCellFinder.GetFirstBlockingBuilding));
                }

                yield return instruction;
            }
        }
    }

    public static class PatchUtility_RCellFinder
    {
        public static Building GetFirstBlockingBuilding(IntVec3 c, Map map)
        {
            List<Thing> things = map.thingGrid.ThingsListAt(c);
            foreach (Thing thing in things)
            {
                if (thing is Building building)
                {
                    if (!BiotechPatchSettings.MechsCanSleepOnConduits || (building.def.hasInteractionCell && building.InteractionCell == c) || building.def.building.isSittable)
                    {
                        return building;
                    }
                }
            }
            return null;
        }
    }
}
