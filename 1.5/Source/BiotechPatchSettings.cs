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
        public static bool MechSmartWorkMode = true;

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
            listingStandard.CheckboxLabeled("BiotechPatch_DrugDeficiencyAlert".Translate(), ref DrugDeficiencyAlert);
            listingStandard.CheckboxLabeled("BiotechPatch_LoadGrowthVats".Translate() + " " + "BiotechPatch_RestartRequired".Translate(), ref LoadGrowthVats);
            listingStandard.CheckboxLabeled("BiotechPatch_AutoChildNicknamesDisabled".Translate(), ref AutoChildNicknamesDisabled);
            listingStandard.CheckboxLabeled("BiotechPatch_ChildrenGoByFirstName".Translate(), ref ChildrenGoByFirstName);
            listingStandard.CheckboxLabeled("BiotechPatch_XenogermCreationForced".Translate(), ref XenogermCreationForced);
            listingStandard.CheckboxLabeled("BiotechPatch_GeneTraitsDontCancelIdentical".Translate(), ref GeneTraitsDontCancelIdentical);
            listingStandard.CheckboxLabeled("BiotechPatch_GrowthMomentChoiceColors".Translate(), ref GrowthMomentChoiceColors);
            listingStandard.CheckboxLabeled("BiotechPatch_GrowthMomentTraitSuppression".Translate(), ref GrowthMomentTraitSuppression);
            listingStandard.CheckboxLabeled("BiotechPatch_ExostriderLostAllowsComplex".Translate(), ref ExostriderLostAllowsComplex);
            listingStandard.CheckboxLabeled("BiotechPatch_CustomHybridXenotypes".Translate(), ref CustomHybridXenotypes);
            listingStandard.CheckboxLabeled("BiotechPatch_BreastfeedingCanBeInterrupted".Translate(), ref BreastfeedingCanBeInterrupted);
            listingStandard.CheckboxLabeled("BiotechPatch_BirthNotCancelledWhenNotDowned".Translate(), ref BirthNotCancelledWhenNotDowned);
            listingStandard.CheckboxLabeled("BiotechPatch_BreastfeedAfterBirth".Translate(), ref BreastfeedAfterBirth);
            listingStandard.CheckboxLabeled("BiotechPatch_MoveBabyToSaferTempLater".Translate(), ref MoveBabyToSaferTempLater);
            listingStandard.CheckboxLabeled("BiotechPatch_MechsControlledByCaravan".Translate(), ref MechsControlledByCaravan);
            listingStandard.CheckboxLabeled("BiotechPatch_MechSmartWorkMode".Translate(), ref MechSmartWorkMode);

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
            Scribe_Values.Look(ref MechSmartWorkMode, "MechSmartWorkMode", true);
        }
    }
}
