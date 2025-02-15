using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using Verse;

namespace BiotechPatch.ChildrenGoByFirstName
{
    [HarmonyPatch(typeof(Dialog_NamePawn))]
    [HarmonyPatch(nameof(Dialog_NamePawn.DoWindowContents))]
    public static class Patch_Dialog_NamePawn
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            CodeInstruction loadNameInstruction = instructions.Last(i => i.opcode == OpCodes.Ldloc_S && ((LocalBuilder)i.operand).LocalIndex == 19);

            foreach (CodeInstruction instruction in instructions)
            {
                if (instruction == loadNameInstruction)
                {
                    instruction.opcode = OpCodes.Ldarg_0;
                    instruction.operand = null;
                    yield return instruction;
                    yield return new CodeInstruction(OpCodes.Ldfld, typeof(Dialog_NamePawn).Field("pawn"));
                    yield return new CodeInstruction(OpCodes.Call, typeof(Pawn).Method("get_Name"));
                    continue;
                }

                yield return instruction;
            }
        }
    }
}
