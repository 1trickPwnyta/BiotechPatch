using RimWorld;
using Verse;

namespace BiotechPatch.HemogenFarmAnyone
{
    public class Comp_HemogenFarm : ThingComp
    {
        private bool hemogenFarmEnabled = false;

        public bool HemogenFarmEnabled
        {
            get => !Pawn.IsPrisoner ? hemogenFarmEnabled : Pawn.guest.IsInteractionEnabled(PrisonerInteractionModeDefOf.HemogenFarm);
            set
            {
                hemogenFarmEnabled = value;
                Bill bill = Pawn.BillStack?.Bills?.FirstOrDefault(b => b.recipe == RecipeDefOf.ExtractHemogenPack);
                if (hemogenFarmEnabled)
                {
                    if (bill == null && SanguophageUtility.CanSafelyBeQueuedForHemogenExtraction(Pawn))
                    {
                        HealthCardUtility.CreateSurgeryBill(Pawn, RecipeDefOf.ExtractHemogenPack, null);
                    }
                }
                else if (bill != null)
                {
                    Pawn.BillStack.Bills.Remove(bill);
                }
                if (Pawn.IsPrisoner)
                {
                    Pawn.guest.ToggleNonExclusiveInteraction(PrisonerInteractionModeDefOf.HemogenFarm, hemogenFarmEnabled);
                }
            }
        }

        public Pawn Pawn => parent as Pawn;

        public override void CompTick()
        {
            if (BiotechPatchSettings.HemogenFarmAnyone && HemogenFarmEnabled && !Pawn.IsPrisoner && Pawn.IsHashIntervalTick(15000) && SanguophageUtility.CanSafelyBeQueuedForHemogenExtraction(Pawn))
            {
                HealthCardUtility.CreateSurgeryBill(Pawn, RecipeDefOf.ExtractHemogenPack, null, sendMessages: false);
            }
        }

        public override void PostExposeData()
        {
            Scribe_Values.Look(ref hemogenFarmEnabled, "hemogenFarmEnabled", false);
        }
    }
}
