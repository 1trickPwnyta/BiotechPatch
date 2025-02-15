using RimWorld;
using Verse;
using Verse.AI;

namespace BiotechPatch.BreastfeedingCanBeInterrupted
{
    public class WorkGiver_BreastfeedCarried : WorkGiver_Breastfeed
    {
        public override ThingRequest PotentialWorkThingRequest => ThingRequest.ForGroup(BiotechPatchSettings.BreastfeedingCanBeInterrupted ? ThingRequestGroup.Pawn : ThingRequestGroup.Undefined);

        public override Job JobOnThing(Pawn pawn, Thing t, bool forced)
        {
            Job job = base.JobOnThing(pawn, t, forced);
            if (BiotechPatchSettings.BreastfeedingCanBeInterrupted)
            {
                Pawn baby = t as Pawn;
                if (!FeedPatientUtility.IsHungry(baby) && baby.CarriedBy != pawn)
                {
                    job = null;
                }
                if (baby.mindState.AutofeedSetting(pawn) != AutofeedMode.Childcare)
                {
                    job = null;
                }
            }
            return job;
        }
    }
}
