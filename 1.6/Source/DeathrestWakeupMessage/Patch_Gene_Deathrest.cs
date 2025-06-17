using HarmonyLib;
using RimWorld;
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
                if (!finished && instruction.opcode == OpCodes.Call && (MethodInfo)instruction.operand == BiotechPatchRefs.m_Gene_Deathrest_Wake)
                {
                    yield return instruction;
                    yield return new CodeInstruction(OpCodes.Ldarg_0);
                    yield return new CodeInstruction(OpCodes.Ldfld, BiotechPatchRefs.f_Gene_pawn);
                    yield return new CodeInstruction(OpCodes.Call, BiotechPatchRefs.m_DeathrestUtility_ShowWakeupMessage);
                    finished = true;
                    continue;
                }

                yield return instruction;
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
