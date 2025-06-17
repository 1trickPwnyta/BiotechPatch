using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
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
            CodeInstruction brInstruction = list.Last(i => i.opcode == OpCodes.Brfalse_S);
            list.InsertRange(list.IndexOf(brInstruction) + 1, new[] 
            {
                new CodeInstruction(OpCodes.Ldarg_0), 
                new CodeInstruction(OpCodes.Ldarg_1), 
                new CodeInstruction(OpCodes.Ldarg_2),
                new CodeInstruction(OpCodes.Call, typeof(PatchUtility_SteadyEnvironmentEffects).Method(nameof(PatchUtility_SteadyEnvironmentEffects.ShouldSendMessage))), 
                brInstruction
            });
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
