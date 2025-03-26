using HarmonyLib;
using System;
using System.Collections.Generic;
using Verse;

namespace BiotechPatch.ResurrectedMechsRememberGroup
{
    [HarmonyPatch(typeof(Pawn))]
    [HarmonyPatch(nameof(Pawn.ExposeData))]
    public static class Patch_Pawn
    {
        private static Dictionary<Pawn, Tuple<List<Pawn>, List<int>>> workingLists = new Dictionary<Pawn, Tuple<List<Pawn>, List<int>>>();

        public static void Postfix(Pawn __instance)
        {
            Dictionary<Pawn, int> lastControlGroups = __instance.GetLastControlGroups();
            List<Pawn> pawnWorkingList = new List<Pawn>();
            List<int> intWorkingList = new List<int>();
            Tuple<List<Pawn>, List<int>> workingListTuple = workingLists.TryGetValue(__instance);
            if (workingListTuple != null)
            {
                pawnWorkingList = workingListTuple.Item1;
                intWorkingList = workingListTuple.Item2;
            }
            Scribe_Collections.Look(ref lastControlGroups, "lastControlGroups", LookMode.Reference, LookMode.Value, ref pawnWorkingList, ref intWorkingList);
            workingLists[__instance] = new Tuple<List<Pawn>, List<int>>(pawnWorkingList, intWorkingList);
            __instance.SetLastControlGroups(lastControlGroups);
        }
    }
}
