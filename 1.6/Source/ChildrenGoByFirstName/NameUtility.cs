using System.Collections.Generic;
using Verse;

namespace BiotechPatch.ChildrenGoByFirstName
{
    public static class NameUtility
    {
        public static Dictionary<NameTriple, Pawn> names = new Dictionary<NameTriple, Pawn>();

        public static Pawn GetPawn(this NameTriple nameTriple)
        {
            if (!names.ContainsKey(nameTriple))
            {
                GameComponent_ChildrenGoByFirstName comp = Current.Game.GetComponent<GameComponent_ChildrenGoByFirstName>();
                if (comp != null)
                {
                    comp.SchedulePawnSearch(nameTriple);
                }
                return null;
            }
            return names[nameTriple];
        }

        public static void ClearCache()
        {
            names.Clear();
        }
    }
}
