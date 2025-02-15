using HarmonyLib;
using Verse;

namespace BiotechPatch.ChildrenGoByFirstName
{
    [HarmonyPatch(typeof(NameTriple))]
    [HarmonyPatch("get_Nick")]
    public static class Patch_NameTriple
    {
        public static void Postfix(NameTriple __instance, ref string __result)
        {
            PawnNameTriple name;
            if (BiotechPatchSettings.ChildrenGoByFirstName && (name = __instance as PawnNameTriple) != null && name.pawn != null && !name.pawn.DevelopmentalStage.Adult())
            {
                __result = name.First;
            }
        }
    }
}
