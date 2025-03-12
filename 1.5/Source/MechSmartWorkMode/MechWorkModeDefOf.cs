using RimWorld;
using Verse;

namespace BiotechPatch.MechSmartWorkMode
{
    [DefOf]
    public static class MechWorkModeDefOf
    {
        static MechWorkModeDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(MechWorkModeDefOf));
        }

        public static MechWorkModeDef SmartWork;
    }
}
