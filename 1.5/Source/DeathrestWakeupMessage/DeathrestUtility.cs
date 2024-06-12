using RimWorld;
using Verse;

namespace BiotechPatch.DeathrestWakeupMessage
{
    public static class DeathrestUtility
    {
        public static void ShowWakeupMessage(Pawn pawn)
        {
            if (BiotechPatchSettings.DeathrestWakeupMessage && PawnUtility.ShouldSendNotificationAbout(pawn))
            {
                Messages.Message("BiotechPatch_WokeFromDeathrest".Translate(pawn.Named("PAWN")), pawn, MessageTypeDefOf.PositiveEvent);
            }
        }
    }
}
