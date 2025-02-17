using HarmonyLib;
using RimWorld;
using Verse.AI;
using Verse;

namespace BiotechPatch.BirthNotCancelledWhenNotDowned
{
    [HarmonyPatch(typeof(JobGiver_KeepLyingDown))]
    [HarmonyPatch("TryGiveJob")]
    public static class Patch_JobGiver_KeepLyingDown
    {
        public static void Postfix(Pawn pawn, ref Job __result)
        {
            if (BiotechPatchSettings.BirthNotCancelledWhenNotDowned)
            {
                LocalTargetInfo target = pawn.GetBirthRitualTargetMother();
                if (target != null)
                {
                    __result.SetTarget(TargetIndex.A, target);
                }
            }
        }
    }
}
