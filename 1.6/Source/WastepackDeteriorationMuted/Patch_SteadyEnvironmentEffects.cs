using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using Verse;

namespace BiotechPatch.WastepackDeteriorationMuted
{
    [HarmonyPatch(typeof(SteadyEnvironmentEffects))]
    [HarmonyPatch(nameof(SteadyEnvironmentEffects.DoDeteriorationDamage))]
    public static class Patch_SteadyEnvironmentEffects
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> list = instructions.ToList();
            CodeInstruction bltInstruction = list.Last(i => i.opcode == OpCodes.Blt_S);
            CodeInstruction ticksInstruction = list.Last(i => i.opcode == OpCodes.Call && i.operand is MethodInfo info && info == typeof(GenTicks).Method("get_TicksGame"));
            list.InsertRange(list.IndexOf(ticksInstruction), new[] 
            {
                new CodeInstruction(OpCodes.Ldarg_0) { labels = ticksInstruction.labels.ListFullCopy() }, 
                new CodeInstruction(OpCodes.Ldarg_1), 
                new CodeInstruction(OpCodes.Ldarg_2),
                new CodeInstruction(OpCodes.Call, typeof(PatchUtility_SteadyEnvironmentEffects).Method(nameof(PatchUtility_SteadyEnvironmentEffects.ShouldSendMessage))), 
                new CodeInstruction(OpCodes.Brfalse, bltInstruction.operand)
            });
            ticksInstruction.labels.Clear();
            return list;
        }
    }

    public static class PatchUtility_SteadyEnvironmentEffects
    {
        public static bool ShouldSendMessage(Thing t, IntVec3 pos, Map map)
        {
            return !BiotechPatchSettings.WastepackDeteriorationMuted || t.def != ThingDefOf.Wastepack || map.areaManager.Home[pos];
        }
    }
}
