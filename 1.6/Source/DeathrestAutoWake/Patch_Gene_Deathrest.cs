using RimWorld;

namespace BiotechPatch.DeathrestAutoWake
{
    // Patched manually in mod constructor
    public static class Patch_Gene_Deathrest
    {
        public static void Postfix(Gene_Deathrest __instance)
        {
            if (BiotechPatchSettings.DeathrestAutoWake)
            {
                __instance.autoWake = true;
            }
        }
    }
}
