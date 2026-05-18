using RimWorld;
using SpecialSauce.ModSettings;
using System.Collections.Generic;
using Verse;

namespace BiotechPatch.MechEnergyDepletedAlert
{
    public class Alert_MechEnergyDepleted : Alert
    {
        private readonly List<Pawn> targets = new List<Pawn>();
        private int recalculateTick = 0;

        public Alert_MechEnergyDepleted()
        {
            defaultLabel = "BiotechPatch_AlertMechEnergyDepleted".Translate();
        }

        private void CalculateTargets()
        {
            if (Find.TickManager.TicksGame >= recalculateTick)
            {
                targets.Clear();
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
                recalculateTick = Find.TickManager.TicksGame + 300;
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
            if (Settings.MechEnergyDepletedAlert.Enabled())
            {
                CalculateTargets();
                return AlertReport.CulpritsAre(targets);
            }
            else
            {
                return false;
            }
        }
    }
}
