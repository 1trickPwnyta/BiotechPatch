using HarmonyLib;
using Verse;

namespace BiotechPatch.BirthNotCancelledWhenNotDowned
{
    [HarmonyPatch(typeof(Pawn_HealthTracker))]
    [HarmonyPatch("MakeUndowned")]
    public static class Patch_Pawn_HealthTracker
    {
        public static void Postfix(Pawn ___pawn)
        {
            if (BiotechPatchSettings.BirthNotCancelledWhenNotDowned)
            {
                LocalTargetInfo target = ___pawn.GetBirthRitualTargetMother();
                if (target != null)
                {
                    ___pawn.Map.reservationManager.Reserve(___pawn, ___pawn.CurJob, target);
                }
            }
        }
    }
}
