using HarmonyLib;
using SpecialSauce.ModSettings;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using Verse;

namespace BiotechPatch.MechsOutsideRadius
{
    [StaticConstructorOnStartup]
    public static class CompatibilityPatch_AchtungMod_Colonist
    {
        static CompatibilityPatch_AchtungMod_Colonist()
        {
            Type type = AccessTools.TypeByName("AchtungMod.Colonist");
            if (type != null && (SpecialModSettings<Settings>.Instance as SpecialModSettings_Multipatch_Biotech).ShouldEnableCodeForSetting(Settings.MechsOutsideRadius))
            {
                var harmony = new Harmony(SpecialMod_Multipatch_Biotech.PACKAGE_ID);
                harmony.Patch(AccessTools.TypeByName("AchtungMod.Colonist").Method("UpdateOrderPos"), transpiler: typeof(CompatibilityPatch_AchtungMod_Colonist).Method(nameof(Transpiler)));
            }
        }

        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            foreach (CodeInstruction instruction in instructions)
            {
                if (instruction.Calls(typeof(ModsConfig).PropertyGetter(nameof(ModsConfig.BiotechActive))))
                {
                    yield return new CodeInstruction(OpCodes.Call, typeof(CompatibilityPatch_AchtungMod_Colonist).PropertyGetter(nameof(MechsOutsideRadius)));
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
