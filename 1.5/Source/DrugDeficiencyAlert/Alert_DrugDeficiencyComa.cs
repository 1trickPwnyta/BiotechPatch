using RimWorld;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace BiotechPatch.DrugDeficiencyAlert
{
    public class Alert_DrugDeficiencyComa : Alert_Critical
    {
        private static List<Hediff_ChemicalDependency> GetRelevantHediffs(Pawn pawn) => pawn.health.hediffSet.hediffs.Where(h => h is Hediff_ChemicalDependency && h.CurStageIndex == 3).Select(h => h as Hediff_ChemicalDependency).ToList();

        private List<Pawn> DeficientPawns => PawnsFinder.AllMapsCaravansAndTravelingTransportPods_Alive_FreeColonistsAndPrisoners_NoCryptosleep.Where(p =>
        {
            return GetRelevantHediffs(p).Any();
        }).ToList();

        public override AlertReport GetReport()
        {
            if (BiotechPatchSettings.DrugDeficiencyAlert)
            {
                return AlertReport.CulpritsAre(DeficientPawns);
            }
            else
            {
                return false;
            }
        }

        public override string GetLabel()
        {
            return "BiotechPatch_DrugDeficiencyComaAlertLabel".Translate() + ": " + DeficientPawns.Select(p => GetRelevantHediffs(p).MaxBy(h => h.Severity)).MaxBy(h => h.Severity).SeverityLabel;
        }

        public override TaggedString GetExplanation()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (Pawn pawn in DeficientPawns)
            {
                stringBuilder.AppendLine("  - " + pawn.NameShortColored.Resolve() + " (" + string.Join(", ", GetRelevantHediffs(pawn).Select(h => h.chemical.LabelCap)) + ")");
            }
            return "BiotechPatch_DrugDeficiencyComaAlertDesc".Translate(stringBuilder.ToString().TrimEndNewlines());
        }
    }
}
