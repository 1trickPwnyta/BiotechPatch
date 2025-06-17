using RimWorld;
using System.Linq;
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
                    int consecutive = 0;
                    bool satisfied = false;
                    foreach (TimeAssignmentDef assignment in pawn.timetable.times)
                    {
                        if (assignment == TimeAssignmentDefOf.Work) { consecutive++; }
                        else { consecutive = 0; }
                        if (consecutive >= 4) { satisfied = true; }
                    }
                    if (!satisfied && pawn.timetable.times[23] == TimeAssignmentDefOf.Work)
                    {
                        int consecutiveAtEnd = 1;
                        foreach (TimeAssignmentDef assignment in pawn.timetable.times.Reverse<TimeAssignmentDef>().Skip(1))
                        {
                            if (assignment == TimeAssignmentDefOf.Work) { consecutiveAtEnd++; }
                            else { break; }
                        }
                        int consecutiveAtBeginning = 0;
                        foreach (TimeAssignmentDef assignment in pawn.timetable.times)
                        {
                            if (assignment == TimeAssignmentDefOf.Work) { consecutiveAtBeginning++; }
                            else { break; }
                        }
                        if (consecutiveAtBeginning + consecutiveAtEnd >= 4) { satisfied = true; }
                    }
                    return !satisfied;
                }
            }
            return false;
        }
    }
}
