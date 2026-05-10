using HarmonyLib;
using RimWorld;
using SpecialSauce.ModSettings;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Verse;

namespace BiotechPatch.DeathrestWakeupMessage
{
    [HarmonyPatch(typeof(Gene_Deathrest))]
    [HarmonyPatch(nameof(Gene_Deathrest.TickDeathresting))]
    public static class Patch_Gene_Deathrest_TickDeathresting
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            bool finished = false;

            foreach (CodeInstruction instruction in instructions)
            {
                if (!finished && instruction.Calls(typeof(Gene_Deathrest).Method(nameof(Gene_Deathrest.Wake))))
                {
                    yield return instruction;
                    yield return new CodeInstruction(OpCodes.Ldarg_0);
                    yield return new CodeInstruction(OpCodes.Ldfld, typeof(Gene).Field(nameof(Gene.pawn)));
                    yield return new CodeInstruction(OpCodes.Call, typeof(Patch_Gene_Deathrest_TickDeathresting).Method(nameof(ShowWakeupMessage)));
                    finished = true;
                    continue;
                }

                yield return instruction;
            }
        }

        private static void ShowWakeupMessage(Pawn pawn)
        {
            if (Settings.DeathrestWakeupMessage.Enabled() && PawnUtility.ShouldSendNotificationAbout(pawn))
            {
                Messages.Message("BiotechPatch_WokeFromDeathrest".Translate(pawn.Named("PAWN")), pawn, MessageTypeDefOf.PositiveEvent);
            }
        }
    }

    [HarmonyPatch(typeof(Gene_Deathrest))]
    [HarmonyPatch(nameof(Gene_Deathrest.GetGizmos))]
    public static class Patch_Gene_Deathrest_GetGizmos
    {
        public static void Postfix(Gene_Deathrest __instance, ref IEnumerable<Gizmo> __result)
        {
            if (__instance.pawn.Deathresting && DebugSettings.ShowDevGizmos)
            {
                __result = __result.AddItem(new Command_Action
                { 
                    defaultLabel = "DEV: Finish deathresting",
                    action = delegate()
                    {
                        __instance.deathrestTicks = __instance.MinDeathrestTicks + 1;
                        __instance.adjustedDeathrestTicks = 240001f;
                        foreach (CompDeathrestBindable compDeathrestBindable in __instance.BoundComps)
                        {
                            compDeathrestBindable.presenceTicks = __instance.deathrestTicks;
                        }
                    }
                });
            }
        }
    }
}
