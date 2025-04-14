using HarmonyLib;
using Verse;

namespace BiotechPatch.ChildrenGoByFirstName
{
    [HarmonyPatch(typeof(Pawn))]
    [HarmonyPatch("set_Name")]
    public static class Patch_Pawn
    {
        public static void Postfix(Pawn __instance)
        {
            if (__instance.Name is NameTriple name)
            {
                name.SetPawn(__instance);
            }
        }
    }
}
