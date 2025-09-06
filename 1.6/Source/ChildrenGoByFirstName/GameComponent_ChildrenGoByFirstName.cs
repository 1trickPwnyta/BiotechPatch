using RimWorld;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace BiotechPatch.ChildrenGoByFirstName
{
    public class GameComponent_ChildrenGoByFirstName : GameComponent
    {
        private Queue<NameTriple> namesToSearch = new Queue<NameTriple>();

        public GameComponent_ChildrenGoByFirstName() { }

        public GameComponent_ChildrenGoByFirstName(Game _) { }

        public void SchedulePawnSearch(NameTriple nameTriple)
        {
            namesToSearch.Enqueue(nameTriple);
        }

        public override void GameComponentTick()
        {
            while (namesToSearch.TryDequeue(out NameTriple nameToSearch))
            {
                Pawn pawn = PawnsFinder.All_AliveOrDead.FirstOrDefault(p => p.Name is NameTriple name && name.First == nameToSearch.First && name.Last == nameToSearch.Last);
                if (pawn != null || Scribe.mode == LoadSaveMode.Inactive)
                {
                    NameUtility.names[nameToSearch] = pawn;
                }
            }
        }
    }
}
