using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using Verse;

namespace BiotechPatch.MechTaskPrioritization
{
    [HarmonyPatch(typeof(FloatMenuMakerMap))]
    [HarmonyPatch(nameof(FloatMenuMakerMap.ChoicesAtFor))]
    public static class Patch_FloatMenuMakerMap_ChoicesAtFor
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> list = instructions.ToList();
            CodeInstruction instruction = list.First(i => i.opcode == OpCodes.Ldsfld && (FieldInfo)i.operand == typeof(DebugSettings).Field(nameof(DebugSettings.allowUndraftedMechOrders)));
            instruction.opcode = OpCodes.Call;
            instruction.operand = typeof(PatchUtility_FloatMenuMakerMap).Method(nameof(PatchUtility_FloatMenuMakerMap.AllowUndraftedMechOrders));
            return list;
        }
    }

    [HarmonyPatch(typeof(FloatMenuMakerMap))]
    [HarmonyPatch("AddDraftedOrders")]
    public static class Patch_FloatMenuMakerMap_AddDraftedOrders
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator il)
        {
            List<CodeInstruction> list = instructions.ToList();
            list.First(i => i.opcode == OpCodes.Callvirt && i.operand is MethodInfo info && info == typeof(RaceProperties).Method("get_IsMechanoid")).operand = typeof(PatchUtility_FloatMenuMakerMap).Method(nameof(PatchUtility_FloatMenuMakerMap.DisallowDraftedMechOrders));
            CodeInstruction checkRepairInstruction = list.GetRange(0, list.FindIndex(i => i.opcode == OpCodes.Ldfld && (FieldInfo)i.operand == typeof(Pawn).Field(nameof(Pawn.skills)))).FindLast(i => i.opcode == OpCodes.Ldloc_0);
            CodeInstruction repairInstruction = list.Skip(list.IndexOf(checkRepairInstruction)).ToList().Find(i => i.opcode == OpCodes.Ldarg_0);
            repairInstruction.labels.Add(il.DefineLabel());
            CodeInstruction[] newInstructions = new[]
            {
                new CodeInstruction(OpCodes.Ldarg_1),
                new CodeInstruction(OpCodes.Call, typeof(PatchUtility_FloatMenuMakerMap).Method(nameof(PatchUtility_FloatMenuMakerMap.CanDoConstruction))), 
                new CodeInstruction(OpCodes.Brtrue, repairInstruction.labels.Last())
            };
            newInstructions[0].labels.AddRange(checkRepairInstruction.labels);
            checkRepairInstruction.labels.Clear();
            list.InsertRange(list.IndexOf(checkRepairInstruction), newInstructions);
            return list;
        }
    }

    public static class PatchUtility_FloatMenuMakerMap
    {
        public static bool AllowUndraftedMechOrders() => DebugSettings.allowUndraftedMechOrders || BiotechPatchSettings.MechTaskPrioritization;

        public static bool DisallowDraftedMechOrders(RaceProperties race) => !BiotechPatchSettings.MechTaskPrioritization && race.IsMechanoid;

        public static bool CanDoConstruction(this Pawn pawn)
        {
            return (BiotechPatchSettings.MechTaskPrioritization && pawn.IsColonyMech && pawn.def.race.mechEnabledWorkTypes.Contains(WorkTypeDefOf.Construction)) || (pawn.skills != null && !pawn.skills.GetSkill(SkillDefOf.Construction).TotallyDisabled);
        }
    }
}
