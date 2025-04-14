using Verse;

namespace BiotechPatch.ChildrenGoByFirstName
{
    public static class NameUtility
    {
        public static Pawn GetPawn(this NameTriple nameTriple)
        {
            GameComponent_ChildrenGoByFirstName comp = Current.Game.GetComponent<GameComponent_ChildrenGoByFirstName>();
            if (comp != null && comp.pawns.ContainsKey(nameTriple))
            {
                return comp.pawns[nameTriple];
            }
            else
            {
                return null;
            }
        }

        public static void SetPawn(this NameTriple nameTriple, Pawn pawn)
        {
            GameComponent_ChildrenGoByFirstName comp = Current.Game.GetComponent<GameComponent_ChildrenGoByFirstName>();
            if (comp != null)
            {
                comp.pawns[nameTriple] = pawn;
            }
        }
    }
}
