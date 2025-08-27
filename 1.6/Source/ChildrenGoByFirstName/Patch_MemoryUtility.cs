using HarmonyLib;
using Verse.Profile;

namespace BiotechPatch.ChildrenGoByFirstName
{
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
