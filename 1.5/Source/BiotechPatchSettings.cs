using RimWorld;
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
        public static bool DrugDeficiencyAlert = true;
        public static bool LoadGrowthVats = true;
        public static bool AutoChildNicknamesDisabled = true;
        public static bool ChildrenGoByFirstName = true;
        public static bool XenogermCreationForced = true;
        public static bool GeneTraitsDontCancelIdentical = true;
        public static bool GrowthMomentChoiceColors = true;
        public static bool GrowthMomentTraitSuppression = true;
        public static bool ExostriderLostAllowsComplex = true;
        public static bool CustomHybridXenotypes = true;
        public static bool BreastfeedingCanBeInterrupted = true;
        public static bool BirthNotCancelledWhenNotDowned = true;
        public static bool BreastfeedAfterBirth = true;
        public static bool MoveBabyToSaferTempLater = true;
        public static bool MechsControlledByCaravan = true;
        public static bool MechsCanSleepOnConduits = true;
        public static bool WastepackDeteriorationMuted = true;
        public static bool ResurrectedMechsRememberGroup = true;
        public static bool MechEnergyDepletedAlert = true;
        public static bool MechTaskPrioritization = true;

        private static Vector2 scrollPosition;
        private static float y;

        public static void DoSettingsWindowContents(Rect inRect)
        {
            Rect viewRect = new Rect(0f, 0f, inRect.width - 20f, y);
            Widgets.BeginScrollView(inRect, ref scrollPosition, viewRect);

            Listing_Standard listing = new Listing_Standard() { maxOneColumn = true };
            listing.Begin(viewRect);

            DoHeader(listing, "BiotechPatch_Children");
            listing.CheckboxLabeled("BiotechPatch_ChildLaborEncouraged".Translate() + " " + "BiotechPatch_RestartRequired".Translate(), ref ChildLaborEncouraged);
            listing.CheckboxLabeled("BiotechPatch_LoadGrowthVats".Translate() + " " + "BiotechPatch_RestartRequired".Translate(), ref LoadGrowthVats);
            listing.CheckboxLabeled("BiotechPatch_AutoChildNicknamesDisabled".Translate(), ref AutoChildNicknamesDisabled);
            listing.CheckboxLabeled("BiotechPatch_ChildrenGoByFirstName".Translate(), ref ChildrenGoByFirstName);
            listing.CheckboxLabeled("BiotechPatch_GrowthMomentChoiceColors".Translate(), ref GrowthMomentChoiceColors);
            listing.CheckboxLabeled("BiotechPatch_GrowthMomentTraitSuppression".Translate(), ref GrowthMomentTraitSuppression);
            listing.CheckboxLabeled("BiotechPatch_BreastfeedingCanBeInterrupted".Translate(), ref BreastfeedingCanBeInterrupted);
            listing.CheckboxLabeled("BiotechPatch_BirthNotCancelledWhenNotDowned".Translate(), ref BirthNotCancelledWhenNotDowned);
            listing.CheckboxLabeled("BiotechPatch_BreastfeedAfterBirth".Translate(), ref BreastfeedAfterBirth);
            listing.CheckboxLabeled("BiotechPatch_MoveBabyToSaferTempLater".Translate(), ref MoveBabyToSaferTempLater);

            listing.Gap();

            DoHeader(listing, "BiotechPatch_Genetics");
            listing.CheckboxLabeled("BiotechPatch_DeathrestAutoWake".Translate(), ref DeathrestAutoWake);
            listing.CheckboxLabeled("BiotechPatch_DeathrestWakeupMessage".Translate(), ref DeathrestWakeupMessage);
            listing.CheckboxLabeled("BiotechPatch_AllowForbiddenXenogermImplantation".Translate(), ref AllowForbiddenXenogermImplantation);
            listing.CheckboxLabeled("BiotechPatch_DrugDeficiencyAlert".Translate(), ref DrugDeficiencyAlert);
            listing.CheckboxLabeled("BiotechPatch_XenogermCreationForced".Translate(), ref XenogermCreationForced);
            listing.CheckboxLabeled("BiotechPatch_GeneTraitsDontCancelIdentical".Translate(), ref GeneTraitsDontCancelIdentical);
            listing.CheckboxLabeled("BiotechPatch_CustomHybridXenotypes".Translate(), ref CustomHybridXenotypes);

            listing.Gap();

            DoHeader(listing, "BiotechPatch_Mechanoids");
            listing.CheckboxLabeled("BiotechPatch_MechsInColonistBar".Translate(), ref MechsInColonistBar);
            listing.CheckboxLabeled("BiotechPatch_MechsOutsideRadius".Translate(), ref MechsOutsideRadius);
            listing.CheckboxLabeled("BiotechPatch_MechAutoRepair".Translate(), ref MechAutoRepair);
            listing.CheckboxLabeled("BiotechPatch_ExostriderLostAllowsComplex".Translate(), ref ExostriderLostAllowsComplex);
            listing.CheckboxLabeled("BiotechPatch_MechsControlledByCaravan".Translate(), ref MechsControlledByCaravan);
            listing.CheckboxLabeled("BiotechPatch_MechsCanSleepOnConduits".Translate(), ref MechsCanSleepOnConduits);
            listing.CheckboxLabeled("BiotechPatch_ResurrectedMechsRememberGroup".Translate(), ref ResurrectedMechsRememberGroup);
            listing.CheckboxLabeled("BiotechPatch_MechEnergyDepletedAlert".Translate(), ref MechEnergyDepletedAlert);
            listing.CheckboxLabeled("BiotechPatch_MechTaskPrioritization".Translate(), ref MechTaskPrioritization);

            listing.Gap();

            DoHeader(listing, "BiotechPatch_Misc");
            listing.CheckboxLabeled("BiotechPatch_HemogenExtractionSpam".Translate(), ref HemogenExtractionSpam);
            listing.CheckboxLabeled("BiotechPatch_WastepackDeteriorationMuted".Translate(), ref WastepackDeteriorationMuted);

            y = listing.CurHeight;
            listing.End();

            Widgets.EndScrollView();
        }

        private static void DoHeader(Listing_Standard listing, string key)
        {
            Text.Font = GameFont.Medium;
            listing.Label(key.Translate());
            listing.GapLine();
            Text.Font = GameFont.Small;
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
            Scribe_Values.Look(ref DrugDeficiencyAlert, "DrugDeficiencyAlert", true);
            Scribe_Values.Look(ref LoadGrowthVats, "LoadGrowthVats", true);
            Scribe_Values.Look(ref AutoChildNicknamesDisabled, "AutoChildNicknamesDisabled", true);
            Scribe_Values.Look(ref ChildrenGoByFirstName, "ChildrenGoByFirstName", true);
            Scribe_Values.Look(ref XenogermCreationForced, "XenogermCreationForced", true);
            Scribe_Values.Look(ref GeneTraitsDontCancelIdentical, "GeneTraitsDontCancelIdentical", true);
            Scribe_Values.Look(ref GrowthMomentChoiceColors, "GrowthMomentChoiceColors", true);
            Scribe_Values.Look(ref GrowthMomentTraitSuppression, "GrowthMomentTraitSuppression", true);
            Scribe_Values.Look(ref ExostriderLostAllowsComplex, "ExostriderLostAllowsComplex", true);
            Scribe_Values.Look(ref CustomHybridXenotypes, "CustomHybridXenotypes", true);
            Scribe_Values.Look(ref BreastfeedingCanBeInterrupted, "BreastfeedingCanBeInterrupted", true);
            Scribe_Values.Look(ref BirthNotCancelledWhenNotDowned, "BirthNotCancelledWhenNotDowned", true);
            Scribe_Values.Look(ref BreastfeedAfterBirth, "BreastfeedAfterBirth", true);
            Scribe_Values.Look(ref MoveBabyToSaferTempLater, "MoveBabyToSaferTempLater", true);
            Scribe_Values.Look(ref MechsControlledByCaravan, "MechsControlledByCaravan", true);
            Scribe_Values.Look(ref MechsCanSleepOnConduits, "MechsCanSleepOnConduits", true);
            Scribe_Values.Look(ref WastepackDeteriorationMuted, "WastepackDeteriorationMuted", true);
            Scribe_Values.Look(ref ResurrectedMechsRememberGroup, "ResurrectedMechsRememberGroup", true);
            Scribe_Values.Look(ref MechEnergyDepletedAlert, "MechEnergyDepletedAlert", true);
            Scribe_Values.Look(ref MechTaskPrioritization, "MechTaskPrioritization", true);
        }
    }
}
