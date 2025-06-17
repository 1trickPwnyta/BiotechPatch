using RimWorld;
using System.Collections.Generic;
using Verse;

namespace BiotechPatch.MechEnergyDepletedAlert
{
    public class Alert_MechEnergyDepleted : Alert
    {
        private readonly List<Pawn> targets = new List<Pawn>();

        public Alert_MechEnergyDepleted()
        {
            defaultLabel = "BiotechPatch_AlertMechEnergyDepleted".Translate();
        }

        private void CalculateTargets()
        {
            targets.Clear();
            if (BiotechPatchSettings.MechEnergyDepletedAlert)
            {
                foreach (Pawn pawn in PawnsFinder.AllMaps_Spawned)
                {
                    if (pawn.IsColonyMechPlayerControlled)
                    {
                        if (pawn.needs != null && pawn.needs.energy != null && pawn.needs.energy.IsLowEnergySelfShutdown)
                        {
                            targets.Add(pawn);
                        }
                    }
                }
            }
        }

        public override TaggedString GetExplanation()
        {
            TaggedString explanation = "BiotechPatch_AlertMechEnergyDepletedDesc".Translate() + "\n";
            foreach (Pawn pawn in targets)
            {
                explanation += "\n  - " + pawn.LabelShortCap;
            }
            return explanation;
        }

        public override AlertReport GetReport()
        {
            CalculateTargets();
            return AlertReport.CulpritsAre(targets);
        }
    }
}
