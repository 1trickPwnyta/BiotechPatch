using BiotechPatch.MechsOutsideRadius;
using HarmonyLib;
using SpecialSauce.Multipatch;
using Verse;

namespace BiotechPatch
{
    public class SpecialMod_Multipatch_Biotech : SpecialMod_Multipatch<SpecialModSettings_Multipatch_Biotech, Settings>
    {
        public const string PACKAGE_ID = "1trickpwnyta.biotechpatch";
        public const string PACKAGE_NAME = "1trickPwnyta's Biotech Patch";

        public SpecialMod_Multipatch_Biotech(ModContentPack content) : base(content)
        {
        }

        protected override string PackageName => PACKAGE_NAME;

        protected override string PackageId => PACKAGE_ID;

        protected override void OnInitialized()
        {
            base.OnInitialized();
            var harmony = new Harmony(PACKAGE_ID);
            harmony.PatchAll();
            if (AccessTools.TypeByName("AchtungMod.Colonist") != null)
            {
                harmony.Patch(AccessTools.TypeByName("AchtungMod.Colonist").Method("UpdateOrderPos"), transpiler: typeof(CompatibilityPatch_AchtungMod_Colonist).Method(nameof(CompatibilityPatch_AchtungMod_Colonist.Transpiler)));
            }
            //harmony.Patch(typeof(CompMechRepairable).Constructor(new Type[] { }), null, typeof(Patch_CompMechRepairable).Method(nameof(Patch_CompMechRepairable.Postfix)));
            Log.Info("Ready.");
        }
    }
}
