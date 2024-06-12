using RimWorld;

namespace BiotechPatch.MechAutoRepair
{
    // Patched manually in mod constructor
    public static class Patch_CompMechRepairable
    {
        public static void Postfix(CompMechRepairable __instance)
        {
            if (BiotechPatchSettings.MechAutoRepair)
            {
                __instance.autoRepair = true;
            }
        }
    }
}
