using HarmonyLib;
using SpecialSauce.Multipatch;
using Verse.Profile;

namespace BiotechPatch.ChildrenGoByFirstName
{
    [HarmonyPatch_Compatibility(SpecialMod_Multipatch_Biotech.PACKAGE_ID, Settings.ChildrenGoByFirstName)]
    [HarmonyPatch(typeof(MemoryUtility))]
    [HarmonyPatch(nameof(MemoryUtility.ClearAllMapsAndWorld))]
    public static class Patch_MemoryUtility
    {
        public static void Postfix()
        {
            NameUtility.ClearCache();
        }
    }
}
