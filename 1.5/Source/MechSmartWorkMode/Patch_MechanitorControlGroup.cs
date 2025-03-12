using Verse;

namespace BiotechPatch.MechSmartWorkMode
{
    // Patched manually in mod constructor
    public static class Patch_MechanitorControlGroup
    {
        public static void Postfix(ref MechWorkModeDef ___workMode)
        {
            if (BiotechPatchSettings.MechSmartWorkMode)
            {
                ___workMode = MechWorkModeDefOf.SmartWork;
            }
        }
    }
}
