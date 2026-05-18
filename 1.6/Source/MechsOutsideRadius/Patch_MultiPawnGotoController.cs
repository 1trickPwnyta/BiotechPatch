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
    [HarmonyPatch(typeof(MultiPawnGotoController))]
    [HarmonyPatch("RecomputeDestinations")]
    public static class Patch_MultiPawnGotoController_RecomputeDestinations
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            foreach (CodeInstruction instruction in instructions)
            {
                if (instruction.Calls(typeof(ModsConfig).PropertyGetter(nameof(ModsConfig.BiotechActive))))
                {
                    yield return new CodeInstruction(OpCodes.Call, typeof(Patch_MultiPawnGotoController_RecomputeDestinations).PropertyGetter(nameof(MechsOutsideRadius)));
                    yield return new CodeInstruction(OpCodes.Ldc_I4_1);
                    yield return new CodeInstruction(OpCodes.Xor);
                    continue;
                }

                yield return instruction;
            }
        }

        private static bool MechsOutsideRadius => Settings.MechsOutsideRadius.Enabled();
    }

    [HarmonyPatch_Compatibility(SpecialMod_Multipatch_Biotech.PACKAGE_ID, Settings.MechsOutsideRadius)]
    [HarmonyPatch(typeof(MultiPawnGotoController))]
    [HarmonyPatch("<RecomputeDestinations>g__CanGoTo|27_0")]
    public static class Patch_MultiPawnGotoController_CanGoTo
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            foreach (CodeInstruction instruction in instructions)
            {
                if (instruction.Calls(typeof(ModsConfig).PropertyGetter(nameof(ModsConfig.BiotechActive))))
                {
                    instruction.operand = typeof(Patch_MultiPawnGotoController_CanGoTo).PropertyGetter(nameof(MechsOutsideRadius));
                    yield return instruction;
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
