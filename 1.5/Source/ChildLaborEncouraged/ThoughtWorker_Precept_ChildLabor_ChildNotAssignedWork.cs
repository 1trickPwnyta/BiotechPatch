using RimWorld;
using Verse;

namespace BiotechPatch.ChildLaborEncouraged
{
    public class ThoughtWorker_Precept_ChildLabor_ChildNotAssignedWork : ThoughtWorker_Precept
    {
        protected override ThoughtState ShouldHaveThought(Pawn p)
        {
            if (!ModsConfig.IdeologyActive || !ModsConfig.BiotechActive)
            {
                return ThoughtState.Inactive;
            }
            foreach (Pawn pawn in p.MapHeld.mapPawns.SpawnedPawnsInFaction(Faction.OfPlayer))
            {
                if (pawn.RaceProps.Humanlike && pawn.DevelopmentalStage.Child() && pawn.timetable != null)
                {
                    if (pawn.timetable.times.Count(t => t == TimeAssignmentDefOf.Work) < 4)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
