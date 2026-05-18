using HarmonyLib;
using RimWorld;
using SpecialSauce.ModSettings;
using SpecialSauce.Multipatch;
using System.Collections.Generic;
using System.Reflection.Emit;
using Verse;

namespace BiotechPatch.MechsOutsideRadius
{
    [HarmonyPatch_Compatibility(SpecialMod_Multipatch_Biotech.PACKAGE_ID, Settings.MechsOutsideRadius)]
    [HarmonyPatch(typeof(FloatMenuOptionProvider_DraftedMove))]
    [HarmonyPatch(nameof(FloatMenuOptionProvider_DraftedMove.PawnCanGoto))]
    public static class Patch_FloatMenuOptionProvider_DraftedMove
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            foreach (CodeInstruction instruction in instructions)
            {
                if (instruction.Calls(typeof(ModsConfig).PropertyGetter(nameof(ModsConfig.BiotechActive))))
                {
                    yield return new CodeInstruction(OpCodes.Call, typeof(Patch_FloatMenuOptionProvider_DraftedMove).PropertyGetter(nameof(MechsOutsideRadius)));
                    yield return new CodeInstruction(OpCodes.Ldc_I4_1);
                    yield return new CodeInstruction(OpCodes.Xor);
                    continue;
                }

                yield return instruction;
            }
        }

        private static bool MechsOutsideRadius => Settings.MechsOutsideRadius.Enabled();
    }
}
