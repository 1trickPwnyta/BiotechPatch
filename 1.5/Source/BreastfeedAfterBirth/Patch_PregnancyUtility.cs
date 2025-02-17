using RimWorld;
using Verse;
using Verse.AI;

namespace BiotechPatch.BreastfeedAfterBirth
{
    // Patched manually in initializer
    public static class Patch_PregnancyUtility
    {
        public static void Postfix(Thing birtherThing, Pawn doctor, LordJob_Ritual lordJobRitual, Thing __result)
        {
            if (BiotechPatchSettings.BreastfeedAfterBirth)
            {
                if (doctor != null && birtherThing is Pawn lactator && __result is Pawn baby)
                {
                    Job job = ChildcareUtility.MakeBreastfeedCarryToMomJob(baby, lactator);
                    if (job != null) 
                    {
                        doctor.jobs.StartJob(job);
                    }
                }
            }
        }
    }
}
