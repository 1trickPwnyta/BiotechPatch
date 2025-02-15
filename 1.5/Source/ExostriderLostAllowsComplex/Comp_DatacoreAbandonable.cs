using Verse;

namespace BiotechPatch.ExostriderLostAllowsComplex
{
    public class Comp_DatacoreAbandonable : ThingComp
    {
        public override void Notify_AbandonedAtTile(int tile)
        {
            base.Notify_AbandonedAtTile(tile);
            if (BiotechPatchSettings.ExostriderLostAllowsComplex)
            {
                Find.History.Notify_MechanoidDatacoreReadOrLost();
            }
        }
    }
}
