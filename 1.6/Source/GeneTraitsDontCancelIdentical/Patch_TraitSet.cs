using HarmonyLib;
using RimWorld;
using System.Linq;
using Verse;

namespace BiotechPatch.GeneTraitsDontCancelIdentical
{
    [HarmonyPatch(typeof(TraitSet))]
    [HarmonyPatch("RecalculateSuppression")]
    public static class Patch_TraitSet
    {
        public static void Postfix(TraitSet __instance, Pawn ___pawn)
        {
            if (BiotechPatchSettings.GeneTraitsDontCancelIdentical)
            {
                foreach (Gene gene in ___pawn.genes.GenesListForReading.Where(g => g.Active && g.def.forcedTraits != null))
                {
                    foreach (GeneticTraitData data in gene.def.forcedTraits)
                    {
                        Trait trait = ___pawn.story.traits.allTraits.FirstOrDefault(t => t.def == data.def && t.Degree == data.degree);
                        if (trait != null && trait.suppressedByTrait)
                        {
                            ___pawn.story.traits.allTraits.Remove(trait);
                            trait = null;
                        }
                        if (trait == null)
                        {
                            trait = new Trait(data.def, data.degree);
                            trait.sourceGene = gene;
                            ___pawn.story.traits.GainTrait(trait, true);
                        }
                        else if (trait.suppressedByGene != null)
                        {
                            trait.sourceGene = gene;
                            trait.suppressedByGene = null;
                        }
                    }
                }
            }
        }
    }
}
