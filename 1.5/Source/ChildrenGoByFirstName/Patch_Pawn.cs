using HarmonyLib;
using Verse;

namespace BiotechPatch.ChildrenGoByFirstName
{
    [HarmonyPatch(typeof(Pawn))]
    [HarmonyPatch("get_Name")]
    public static class Patch_Pawn
    {
        public static void Postfix(Pawn __instance, ref Name __result)
        {
            if (__result is NameTriple)
            {
                __result = new PawnNameTriple(__instance, (NameTriple)__result);
            }
        }
    }
}
