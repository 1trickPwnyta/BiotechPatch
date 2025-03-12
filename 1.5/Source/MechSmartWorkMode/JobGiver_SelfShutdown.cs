using RimWorld;
using Verse;
using Verse.AI;

namespace BiotechPatch.MechSmartWorkMode
{
    public class JobGiver_SelfShutdown : ThinkNode_JobGiver
    {
        protected override Job TryGiveJob(Pawn pawn)
        {
            if (pawn.GetOverseer()?.mechanitor?.controlGroups.Any(g => g.WorkMode == RimWorld.MechWorkModeDefOf.SelfShutdown) ?? false)
            {
                if (RCellFinder.TryFindNearbyMechSelfShutdownSpot(pawn.Position, pawn, pawn.Map, out IntVec3 c, true))
                {
                    Job job = JobMaker.MakeJob(JobDefOf.SelfShutdown, c);
                    job.forceSleep = true;
                    job.expiryInterval = 300;
                    job.checkOverrideOnExpire = true;
                    return job;
                }
            }

            return null;
        }
    }
}
