using SpecialSauce.Multipatch;

namespace BiotechPatch
{
    public class SpecialModSettings_Multipatch_Biotech : SpecialModSettings_Multipatch<Settings>
    {
        protected override string SettingKeyPrefix => "BiotechPatch";
    }

    public static class Category
    {
        public const string Children = "BiotechPatch_Children";
        public const string Genetics = "BiotechPatch_Genetics";
        public const string Mechanoids = "BiotechPatch_Mechanoids";
        public const string Misc = "BiotechPatch_Misc";
    }

    public enum Settings
    {
        [MultipatchSetting(Category.Children, restartRequired: true)] ChildLaborEncouraged,
        [MultipatchSetting(Category.Children, restartRequired: true)] LoadGrowthVats,
        [MultipatchSetting(Category.Children)] AutoChildNicknamesDisabled,
        [MultipatchSetting(Category.Children)] ChildrenGoByFirstName,
        [MultipatchSetting(Category.Children)] GrowthMomentChoiceColors,
        [MultipatchSetting(Category.Children, bugFix: true)] GrowthMomentTraitSuppression,
        [MultipatchSetting(Category.Children)] BreastfeedingCanBeInterrupted,
        [MultipatchSetting(Category.Children)] MoveBabyToSaferTempLater,

        [MultipatchSetting(Category.Genetics)] DeathrestAutoWake,
        [MultipatchSetting(Category.Genetics)] DeathrestWakeupMessage,
        [MultipatchSetting(Category.Genetics, restartRequired: true)] WebbedPhalangesCanBeWet,
        [MultipatchSetting(Category.Genetics)] AllowForbiddenXenogermImplantation,
        [MultipatchSetting(Category.Genetics)] DrugDeficiencyAlert,
        [MultipatchSetting(Category.Genetics)] XenogermCreationForced,
        [MultipatchSetting(Category.Genetics)] KillThirstSatisfiedInAnyMelee,
        [MultipatchSetting(Category.Genetics, bugFix: true)] GeneTraitsDontCancelIdentical,
        [MultipatchSetting(Category.Genetics, bugFix: true)] CustomHybridXenotypes,

        [MultipatchSetting(Category.Mechanoids)] MechsInColonistBar,
        [MultipatchSetting(Category.Mechanoids)] MechsOutsideRadius,
        [MultipatchSetting(Category.Mechanoids)] MechAutoRepair,
        [MultipatchSetting(Category.Mechanoids)] MechsControlledByCaravan,
        [MultipatchSetting(Category.Mechanoids)] MechsCanSleepOnConduits,
        [MultipatchSetting(Category.Mechanoids)] ResurrectedMechsRememberGroup,
        [MultipatchSetting(Category.Mechanoids)] MechEnergyDepletedAlert,
        [MultipatchSetting(Category.Mechanoids, bugFix: true)] MechTaskPrioritization,

        [MultipatchSetting(Category.Misc, restartRequired: true)] HemogenFarmAnyone,
        [MultipatchSetting(Category.Misc)] HemogenExtractionSpam,
        [MultipatchSetting(Category.Misc)] WastepackDeteriorationMuted
    }
}
