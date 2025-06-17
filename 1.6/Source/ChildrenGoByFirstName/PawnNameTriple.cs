using Verse;

namespace BiotechPatch.ChildrenGoByFirstName
{
    // Unused, kept only for backward compatibility
    public class PawnNameTriple : NameTriple
    {
        public Pawn pawn;

        public PawnNameTriple() { }

        public PawnNameTriple(Pawn pawn, NameTriple name) : base(name.First, name.NickSet ? name.Nick : null, name.Last)
        {
            this.pawn = pawn;
        }
    }
}
