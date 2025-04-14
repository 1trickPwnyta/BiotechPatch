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

        public override void ExposeData()
        {
            Scribe_Collections.Look(ref pawns, "pawns", LookMode.Deep, LookMode.Reference, ref keysWorkingList, ref valuesWorkingList);
        }
    }
}
