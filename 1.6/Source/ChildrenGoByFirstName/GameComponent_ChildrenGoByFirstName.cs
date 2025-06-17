using RimWorld;
using System.Collections.Generic;
using Verse;

namespace BiotechPatch.ChildrenGoByFirstName
{
    public class GameComponent_ChildrenGoByFirstName : GameComponent
    {
        public Dictionary<NameTriple, Pawn> pawns = new Dictionary<NameTriple, Pawn>();
        private List<NameTriple> keysWorkingList;
        private List<Pawn> valuesWorkingList;

        public GameComponent_ChildrenGoByFirstName() { }

        public GameComponent_ChildrenGoByFirstName(Game _) { }

        private void Purge()
        {
            List<NameTriple> keysToRemove = new List<NameTriple>();
            foreach (KeyValuePair<NameTriple, Pawn> pair in pawns)
            {
                if (pair.Value == null || !PawnsFinder.All_AliveOrDead.Contains(pair.Value))
                {
                    keysToRemove.Add(pair.Key);
                }
            }
            foreach (NameTriple name in keysToRemove)
            {
                pawns.Remove(name);
            }
        }

        public override void ExposeData()
        {
            if (Scribe.mode == LoadSaveMode.Saving)
            {
                Purge();
            }
            Scribe_Collections.Look(ref pawns, "pawns", LookMode.Deep, LookMode.Reference, ref keysWorkingList, ref valuesWorkingList);
        }
    }
}
