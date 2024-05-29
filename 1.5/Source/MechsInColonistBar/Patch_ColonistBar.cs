using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace BiotechPatch.MechsInColonistBar
{
    [HarmonyPatch(typeof(ColonistBar))]
    [HarmonyPatch("CheckRecacheEntries")]
    public static class Patch_ColonistBar_CheckRecacheEntries
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            bool foundClear = false;
            bool foundCaravans = false;
            bool foundColonistCheck = false;
            bool addedMechCheck = false;

            foreach (CodeInstruction instruction in instructions)
            {
                if (!foundClear && instruction.opcode == OpCodes.Callvirt && (MethodInfo)instruction.operand == BiotechPatchRefs.m_List_Pawn_Clear)
                {
                    yield return instruction;
                    yield return new CodeInstruction(OpCodes.Ldsfld, BiotechPatchRefs.f_ColonistBar_tmpMaps);
                    yield return new CodeInstruction(OpCodes.Ldloc_1);
                    yield return new CodeInstruction(OpCodes.Callvirt, BiotechPatchRefs.m_List_Map_get_Item);
                    yield return new CodeInstruction(OpCodes.Ldsfld, BiotechPatchRefs.f_ColonistBar_tmpPawns);
                    yield return new CodeInstruction(OpCodes.Call, BiotechPatchRefs.m_ColonistBarUtility_AddColonyMechs);
                    foundClear = true;
                    continue;
                }
                
                if (!foundCaravans && instruction.opcode == OpCodes.Ldsfld && (FieldInfo)instruction.operand == BiotechPatchRefs.f_ColonistBar_tmpCaravans)
                {
                    foundCaravans = true;
                }

                if (foundCaravans && !foundColonistCheck && instruction.opcode == OpCodes.Callvirt && (MethodInfo)instruction.operand == BiotechPatchRefs.m_Pawn_get_IsColonist)
                {
                    foundColonistCheck = true;
                }

                if (foundColonistCheck && !addedMechCheck && instruction.opcode == OpCodes.Brtrue_S)
                {
                    yield return instruction;
                    yield return new CodeInstruction(OpCodes.Ldsfld, BiotechPatchRefs.f_ColonistBar_tmpPawns);
                    yield return new CodeInstruction(OpCodes.Ldloc_S, 12);
                    yield return new CodeInstruction(OpCodes.Callvirt, BiotechPatchRefs.m_List_Pawn_get_Item);
                    yield return new CodeInstruction(OpCodes.Callvirt, BiotechPatchRefs.m_Pawn_get_IsColonyMech);
                    yield return instruction;
                    addedMechCheck = true;
                    continue;
                }

                yield return instruction;
            }
        }
    }
}
