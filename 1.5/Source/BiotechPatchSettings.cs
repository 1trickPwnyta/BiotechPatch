using UnityEngine;
using Verse;

namespace BiotechPatch
{
    public class BiotechPatchSettings : ModSettings
    {
        public static bool DeathrestAutoWake = true;
        public static bool MechsInColonistBar = true;
        public static bool MechsOutsideRadius = true;
        public static bool HemogenExtractionSpam = true;
        public static bool DeathrestWakeupMessage = true;
        public static bool MechAutoRepair = true;
        public static bool ChildLaborEncouraged = true;
        public static bool AllowForbiddenXenogermImplantation = true;

        public static void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listingStandard = new Listing_Standard();

            listingStandard.Begin(inRect);

            listingStandard.CheckboxLabeled("BiotechPatch_DeathrestAutoWake".Translate(), ref DeathrestAutoWake);
            listingStandard.CheckboxLabeled("BiotechPatch_MechsInColonistBar".Translate(), ref MechsInColonistBar);
            listingStandard.CheckboxLabeled("BiotechPatch_MechsOutsideRadius".Translate(), ref MechsOutsideRadius);
            listingStandard.CheckboxLabeled("BiotechPatch_HemogenExtractionSpam".Translate(), ref HemogenExtractionSpam);
            listingStandard.CheckboxLabeled("BiotechPatch_DeathrestWakeupMessage".Translate(), ref DeathrestWakeupMessage);
            listingStandard.CheckboxLabeled("BiotechPatch_MechAutoRepair".Translate(), ref MechAutoRepair);
            listingStandard.CheckboxLabeled("BiotechPatch_ChildLaborEncouraged".Translate() + " " + "BiotechPatch_RestartRequired".Translate(), ref ChildLaborEncouraged);
            listingStandard.CheckboxLabeled("BiotechPatch_AllowForbiddenXenogermImplantation".Translate(), ref AllowForbiddenXenogermImplantation);

            listingStandard.End();
        }

        public override void ExposeData()
        {
            Scribe_Values.Look(ref DeathrestAutoWake, "DeathrestAutoWake", true);
            Scribe_Values.Look(ref MechsInColonistBar, "MechsInColonistBar", true);
            Scribe_Values.Look(ref MechsOutsideRadius, "MechsOutsideRadius", true);
            Scribe_Values.Look(ref HemogenExtractionSpam, "HemogenExtractionSpam", true);
            Scribe_Values.Look(ref DeathrestWakeupMessage, "DeathrestWakeupMessage", true);
            Scribe_Values.Look(ref MechAutoRepair, "MechAutoRepair", true);
            Scribe_Values.Look(ref ChildLaborEncouraged, "ChildLaborEncouraged", true);
            Scribe_Values.Look(ref AllowForbiddenXenogermImplantation, "AllowForbiddenXenogermImplantation", true);
        }
    }
}
