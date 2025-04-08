using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using Verse;

namespace BiotechPatch.MechEnergyDepletedAlert
{
    [HarmonyPatch(typeof(PawnUIOverlay))]
    [HarmonyPatch(nameof(PawnUIOverlay.DrawPawnGUIOverlay))]
    public static class Patch_PawnUIOverlay
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> list = instructions.ToList();
            int index = list.FirstIndexOf(i => i.opcode == OpCodes.Callvirt && i.operand is MethodInfo info && info == typeof(OverlayDrawer).Method(nameof(OverlayDrawer.DrawOverlay)));
            list[index].operand = typeof(PatchUtility_PawnUIOverlay).Method(nameof(PatchUtility_PawnUIOverlay.DrawMechChargeOverlay));
            list.Insert(index, new CodeInstruction(OpCodes.Pop));
            return list;
        }
    }

    public static class PatchUtility_PawnUIOverlay
    {
        public static void DrawMechChargeOverlay(OverlayDrawer drawer, Pawn mech)
        {
            if (BiotechPatchSettings.MechEnergyDepletedAlert && mech.needs?.energy?.IsLowEnergySelfShutdown == true)
            {
                drawer.DrawOverlay(mech, OverlayTypes.NeedsPower);
            }
            else
            {
                drawer.DrawOverlay(mech, OverlayTypes.SelfShutdown);
            }
        }
    }
}
