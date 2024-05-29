using BiotechPatch.MechsInColonistBar;
using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Reflection;
using Verse;

namespace BiotechPatch
{
    public static class BiotechPatchRefs
    {
        public static readonly FieldInfo f_ColonistBar_tmpPawns = AccessTools.Field(typeof(ColonistBar), "tmpPawns");
        public static readonly FieldInfo f_ColonistBar_tmpMaps = AccessTools.Field(typeof(ColonistBar), "tmpMaps");
        public static readonly FieldInfo f_ColonistBar_tmpCaravans = AccessTools.Field(typeof(ColonistBar), "tmpCaravans");

        public static readonly MethodInfo m_List_Pawn_Clear = AccessTools.Method(typeof(List<Pawn>), nameof(List<Pawn>.Clear));
        public static readonly MethodInfo m_List_Pawn_get_Item = AccessTools.Method(typeof(List<Pawn>), "get_Item");
        public static readonly MethodInfo m_List_Map_get_Item = AccessTools.Method(typeof(List<Map>), "get_Item");
        public static readonly MethodInfo m_ColonistBarUtility_AddColonyMechs = AccessTools.Method(typeof(ColonistBarUtility), nameof(ColonistBarUtility.AddColonyMechs));
        public static readonly MethodInfo m_Pawn_get_IsColonist = AccessTools.Method(typeof(Pawn), "get_IsColonist");
        public static readonly MethodInfo m_Pawn_get_IsColonyMech = AccessTools.Method(typeof(Pawn), "get_IsColonyMech");
        public static readonly MethodInfo m_ModsConfig_get_BiotechActive = AccessTools.Method(typeof(ModsConfig), "get_BiotechActive");
    }
}
