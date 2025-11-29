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
        public static bool CustomHybridXenotypes = true;
        public static bool BreastfeedingCanBeInterrupted = true;
        public static bool MoveBabyToSaferTempLater = true;
        public static bool MechsControlledByCaravan = true;
        public static bool MechsCanSleepOnConduits = true;
        public static bool WastepackDeteriorationMuted = true;
        public static bool ResurrectedMechsRememberGroup = true;
        public static bool MechEnergyDepletedAlert = true;
        public static bool MechTaskPrioritization = true;
        public static bool WebbedPhalangesCanBeWet = true;
        public static bool HemogenFarmAnyone = true;

        private static Vector2 scrollPosition;
        private static float y;

        public static void DoSettingsWindowContents(Rect inRect)
        {
            Rect viewRect = new Rect(0f, 0f, inRect.width - 20f, y);
            Widgets.BeginScrollView(inRect, ref scrollPosition, viewRect);
            Listing_Standard listing = new Listing_Standard() { maxOneColumn = true };
            listing.Begin(viewRect);

            DoHeader(listing, "BiotechPatch_Children");
            DoSetting(listing, "BiotechPatch_ChildLaborEncouraged", ref ChildLaborEncouraged, restartRequired: true);
            DoSetting(listing, "BiotechPatch_LoadGrowthVats", ref LoadGrowthVats, restartRequired: true);
            DoSetting(listing, "BiotechPatch_AutoChildNicknamesDisabled", ref AutoChildNicknamesDisabled);
            DoSetting(listing, "BiotechPatch_ChildrenGoByFirstName", ref ChildrenGoByFirstName);
            DoSetting(listing, "BiotechPatch_GrowthMomentChoiceColors", ref GrowthMomentChoiceColors);
            DoSetting(listing, "BiotechPatch_GrowthMomentTraitSuppression", ref GrowthMomentTraitSuppression, bugFix: true);
            DoSetting(listing, "BiotechPatch_BreastfeedingCanBeInterrupted", ref BreastfeedingCanBeInterrupted);
            DoSetting(listing, "BiotechPatch_MoveBabyToSaferTempLater", ref MoveBabyToSaferTempLater);

            listing.Gap();

            DoHeader(listing, "BiotechPatch_Genetics");
            DoSetting(listing, "BiotechPatch_DeathrestAutoWake", ref DeathrestAutoWake);
            DoSetting(listing, "BiotechPatch_DeathrestWakeupMessage", ref DeathrestWakeupMessage);
            DoSetting(listing, "BiotechPatch_WebbedPhalangesCanBeWet", ref WebbedPhalangesCanBeWet, restartRequired: true);
            DoSetting(listing, "BiotechPatch_AllowForbiddenXenogermImplantation", ref AllowForbiddenXenogermImplantation);
            DoSetting(listing, "BiotechPatch_DrugDeficiencyAlert", ref DrugDeficiencyAlert);
            DoSetting(listing, "BiotechPatch_XenogermCreationForced", ref XenogermCreationForced);
            DoSetting(listing, "BiotechPatch_GeneTraitsDontCancelIdentical", ref GeneTraitsDontCancelIdentical, bugFix: true);
            DoSetting(listing, "BiotechPatch_CustomHybridXenotypes", ref CustomHybridXenotypes, bugFix: true);

            listing.Gap();

            DoHeader(listing, "BiotechPatch_Mechanoids");
            DoSetting(listing, "BiotechPatch_MechsInColonistBar", ref MechsInColonistBar);
            DoSetting(listing, "BiotechPatch_MechsOutsideRadius", ref MechsOutsideRadius);
            DoSetting(listing, "BiotechPatch_MechAutoRepair", ref MechAutoRepair);
            DoSetting(listing, "BiotechPatch_MechsControlledByCaravan", ref MechsControlledByCaravan);
            DoSetting(listing, "BiotechPatch_MechsCanSleepOnConduits", ref MechsCanSleepOnConduits);
            DoSetting(listing, "BiotechPatch_ResurrectedMechsRememberGroup", ref ResurrectedMechsRememberGroup);
            DoSetting(listing, "BiotechPatch_MechEnergyDepletedAlert", ref MechEnergyDepletedAlert);
            DoSetting(listing, "BiotechPatch_MechTaskPrioritization", ref MechTaskPrioritization, bugFix: true);

            listing.Gap();

            DoHeader(listing, "BiotechPatch_Misc");
            DoSetting(listing, "BiotechPatch_HemogenFarmAnyone", ref HemogenFarmAnyone, restartRequired: true);
            DoSetting(listing, "BiotechPatch_HemogenExtractionSpam", ref HemogenExtractionSpam);
            DoSetting(listing, "BiotechPatch_WastepackDeteriorationMuted", ref WastepackDeteriorationMuted);

            y = listing.CurHeight;
            listing.End();

            Widgets.EndScrollView();
        }

        private static void DoHeader(Listing_Standard listing, string key)
        {
            using (new TextBlock(GameFont.Medium))
            {
                listing.Label(key.Translate());
            }
            listing.GapLine();
        }

        private static void DoSetting(Listing_Standard listing, string key, ref bool setting, bool restartRequired = false, bool bugFix = false, bool dependsOn = true, int indentLevel = 0)
        {
            if (dependsOn)
            {
                string indent = new string(' ', indentLevel * 2);
                listing.CheckboxLabeled(indent + (bugFix ? "BiotechPatch_BugFix".Translate() + ": " : TaggedString.Empty) + key.Translate() + (restartRequired ? " " + "BiotechPatch_RestartRequired".Translate() : TaggedString.Empty), ref setting);
            }
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
            Scribe_Values.Look(ref CustomHybridXenotypes, "CustomHybridXenotypes", true);
            Scribe_Values.Look(ref BreastfeedingCanBeInterrupted, "BreastfeedingCanBeInterrupted", true);
            Scribe_Values.Look(ref MoveBabyToSaferTempLater, "MoveBabyToSaferTempLater", true);
            Scribe_Values.Look(ref MechsControlledByCaravan, "MechsControlledByCaravan", true);
            Scribe_Values.Look(ref MechsCanSleepOnConduits, "MechsCanSleepOnConduits", true);
            Scribe_Values.Look(ref WastepackDeteriorationMuted, "WastepackDeteriorationMuted", true);
            Scribe_Values.Look(ref ResurrectedMechsRememberGroup, "ResurrectedMechsRememberGroup", true);
            Scribe_Values.Look(ref MechEnergyDepletedAlert, "MechEnergyDepletedAlert", true);
            Scribe_Values.Look(ref MechTaskPrioritization, "MechTaskPrioritization", true);
            Scribe_Values.Look(ref WebbedPhalangesCanBeWet, "WebbedPhalangesCanBeWet", true);
            Scribe_Values.Look(ref HemogenFarmAnyone, "HemogenFarmAnyone", true);
        }
    }
}
