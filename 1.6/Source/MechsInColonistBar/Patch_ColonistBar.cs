using HarmonyLib;
using RimWorld;
using SpecialSauce.ModSettings;
using SpecialSauce.Multipatch;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using Verse;

namespace BiotechPatch.MechsInColonistBar
{
    [HarmonyPatch_Compatibility(SpecialMod_Multipatch_Biotech.PACKAGE_ID, Settings.MechsInColonistBar)]
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
                if (!foundClear && instruction.Calls(typeof(List<Pawn>).Method(nameof(List<Pawn>.Clear))))
                {
                    yield return instruction;
                    yield return new CodeInstruction(OpCodes.Ldsfld, typeof(ColonistBar).Field("tmpMaps"));
                    yield return new CodeInstruction(OpCodes.Ldloc_1);
                    yield return new CodeInstruction(OpCodes.Callvirt, typeof(List<Map>).IndexerGetter(new[] { typeof(int) }));
                    yield return new CodeInstruction(OpCodes.Ldsfld, typeof(ColonistBar).Field("tmpPawns"));
                    yield return new CodeInstruction(OpCodes.Call, typeof(Patch_ColonistBar_CheckRecacheEntries).Method(nameof(AddColonyMechs)));
                    foundClear = true;
                    continue;
                }
                
                if (!foundCaravans && instruction.LoadsField(typeof(ColonistBar).Field("tmpCaravans")))
                {
                    foundCaravans = true;
                }

                if (foundCaravans && !foundColonistCheck && instruction.Calls(typeof(Pawn).PropertyGetter(nameof(Pawn.IsColonist))))
                {
                    foundColonistCheck = true;
                }

                if (foundColonistCheck && !addedMechCheck && instruction.opcode == OpCodes.Brtrue_S)
                {
                    yield return instruction;
                    yield return new CodeInstruction(OpCodes.Ldsfld, typeof(ColonistBar).Field("tmpPawns"));
                    yield return new CodeInstruction(OpCodes.Ldloc_S, 12);
                    yield return new CodeInstruction(OpCodes.Callvirt, typeof(List<Pawn>).IndexerGetter(new[] { typeof(int) }));
                    yield return new CodeInstruction(OpCodes.Call, typeof(Patch_ColonistBar_CheckRecacheEntries).Method(nameof(ShouldShowMechInColonistBar)));
                    yield return instruction;
                    addedMechCheck = true;
                    continue;
                }

                yield return instruction;
            }
        }

        private static void AddColonyMechs(Map map, List<Pawn> pawns)
        {
            if (Settings.MechsInColonistBar.Enabled())
            {
                pawns.AddRange(map.mapPawns.SpawnedColonyMechs.Where(m => ShouldShowMechInColonistBar(m)));
            }
        }

        private static bool ShouldShowMechInColonistBar(Pawn mech)
        {
            return Settings.MechsInColonistBar.Enabled() && mech.IsColonyMech && mech.OverseerSubject != null;
        }
    }
}
