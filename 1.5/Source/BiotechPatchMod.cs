using BiotechPatch.MechsOutsideRadius;
using HarmonyLib;
using Verse;

namespace BiotechPatch
{
    public class BiotechPatchMod : Mod
    {
        public const string PACKAGE_ID = "biotechpatch.1trickPwnyta";
        public const string PACKAGE_NAME = "1trickPwnyta's Biotech Patch";

        public BiotechPatchMod(ModContentPack content) : base(content)
        {
            var harmony = new Harmony(PACKAGE_ID);
            harmony.PatchAll();
            if (AccessTools.TypeByName("AchtungMod.Colonist") != null)
            {
                harmony.Patch(AccessTools.TypeByName("AchtungMod.Colonist").Method("UpdateOrderPos"), null, null, typeof(CompatibilityPatch_AchtungMod_Colonist).Method(nameof(CompatibilityPatch_AchtungMod_Colonist.Transpiler)));
            }

            Log.Message($"[{PACKAGE_NAME}] Loaded.");
        }
    }
}
