using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using UnityEngine;
using Verse;
using static HarmonyLib.Code;

namespace BiotechPatch.HemogenFarmAnyone
{
    [HarmonyPatch(typeof(HealthCardUtility))]
    [HarmonyPatch("DrawOverviewTab")]
    public static class Patch_HealthCardUtility
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> instructionsList = instructions.ToList();
            int index = instructionsList.FindIndex(i => i.LoadsConstant("MessageSelfTendUnsatisfied"));
            instructionsList.InsertRange(index + 20, new[]
            {
                new CodeInstruction(OpCodes.Ldarg_0),
                new CodeInstruction(OpCodes.Ldarga_S, 2),
                new CodeInstruction(OpCodes.Ldarg_1),
                new CodeInstruction(OpCodes.Call, typeof(Patch_HealthCardUtility).Method(nameof(DoHemogenFarmCheckbox)))
            });
            return instructionsList;
        }

        private static void DoHemogenFarmCheckbox(Rect rect, ref float curY, Pawn pawn)
        {
            if (BiotechPatchSettings.HemogenFarmAnyone)
            {
                curY -= 6f;
                Rect hemogenFarmRect = new Rect(0f, curY, rect.width, 23f);
                Comp_HemogenFarm comp = pawn.TryGetComp<Comp_HemogenFarm>();
                if (comp != null && pawn.genes?.HasActiveGene(GeneDefOf.Hemogenic) != true)
                {
                    Widgets.DrawHighlightIfMouseover(hemogenFarmRect);
                    TooltipHandler.TipRegion(hemogenFarmRect, "BiotechPatch_HemogenFarmTip".Translate());
                    Rect leftRect = hemogenFarmRect.LeftPartPixels(hemogenFarmRect.width / 2 - 4f);
                    Rect rightRect = hemogenFarmRect.RightPartPixels(hemogenFarmRect.width / 2 - 4f).LeftPartPixels(hemogenFarmRect.height);
                    using (new TextBlock(TextAnchor.MiddleLeft)) Widgets.Label(leftRect, "BiotechPatch_HemogenFarm".Translate());
                    bool hemogenFarmEnabled = comp.HemogenFarmEnabled;
                    Widgets.Checkbox(rightRect.position, ref hemogenFarmEnabled, rightRect.height);
                    if (hemogenFarmEnabled != comp.HemogenFarmEnabled)
                    {
                        comp.HemogenFarmEnabled = hemogenFarmEnabled;
                    }
                }
                curY += hemogenFarmRect.height + 10f;
            }
        }
    }
}
