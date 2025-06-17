using BiotechPatch.CustomHybridXenotypes;
using HarmonyLib;
using RimWorld;
using Verse;

namespace BiotechPatch
{
    [StaticConstructorOnStartup]
    public static class BiotechPatchInitializer
    {
        static BiotechPatchInitializer()
        {
            Harmony harmony = new Harmony(BiotechPatchMod.PACKAGE_ID);
            harmony.Patch(typeof(PregnancyUtility).Method("TryGetInheritedXenotype"), null, typeof(Patch_PregnancyUtility_TryGetInheritedXenotype).Method(nameof(Patch_PregnancyUtility_TryGetInheritedXenotype.Postfix)));
            harmony.Patch(typeof(PregnancyUtility).Method("ShouldByHybrid"), null, typeof(Patch_PregnancyUtility_ShouldByHybrid).Method(nameof(Patch_PregnancyUtility_ShouldByHybrid.Postfix)));
            //harmony.Patch(typeof(PregnancyUtility).Method(nameof(PregnancyUtility.ApplyBirthOutcome_NewTemp)), null, typeof(Patch_PregnancyUtility).Method(nameof(Patch_PregnancyUtility.Postfix)));
        }
    }
}
