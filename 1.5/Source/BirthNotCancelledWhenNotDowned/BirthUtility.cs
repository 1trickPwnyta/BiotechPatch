using RimWorld;
using Verse;
using Verse.AI.Group;

namespace BiotechPatch.BirthNotCancelledWhenNotDowned
{
    public static class BirthUtility
    {
        public static LocalTargetInfo GetBirthRitualTargetMother(this Pawn pawn)
        {
            if (pawn.GetLord()?.LordJob is LordJob_Ritual job)
            {
                if (job.Ritual?.def == PreceptDefOf.ChildBirth && job.RoleFor(pawn, true) is RitualRole_Mother && job.StageIndex >= 2)
                {
                    return new LocalTargetInfo(job.selectedTarget.Thing);
                }
            }
            return null;
        }
    }
}
