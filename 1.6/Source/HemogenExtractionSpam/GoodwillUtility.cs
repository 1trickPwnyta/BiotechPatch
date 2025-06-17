using RimWorld;

namespace BiotechPatch.HemogenExtractionSpam
{
    public static class GoodwillUtility
    {
        public static bool ShouldSendMessage(Faction factionToInform, HistoryEventDef def)
        {
            return !BiotechPatchSettings.HemogenExtractionSpam || def != HistoryEventDefOf.ExtractedHemogenPack || !factionToInform.HostileTo(Faction.OfPlayer);
        }
    }
}
