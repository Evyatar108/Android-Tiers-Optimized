using System.Collections.Generic;
using System;
using HarmonyLib;
using RimWorld;
using Verse;

namespace MOARANDROIDS
{
    internal static class MapPawns_Patch
    {
        [HarmonyPatch(typeof(MapPawns), "get_AnyPawnBlockingMapRemoval")]
        public class MapPawns_get_AnyPawnBlockingMapRemoval
        {
            [HarmonyPostfix]
            public static void Listener(MapPawns __instance, ref bool __result, List<Pawn> ___pawnsSpawned)
            {
                //Si retour pas true alors check s'il y a de la correction a faire
                if (!__result)
                {
                    for (int i = 0; i < ___pawnsSpawned.Count; i++)
                    {
                        var pawnSpawned = ___pawnsSpawned[i];
                        if (pawnSpawned == null)
                            continue;

                        CompAndroidState cas = pawnSpawned.TryGetComp<CompAndroidState>();

                        //Si pawn non décédé mais est un surrogate inactif
                        if (!pawnSpawned.Dead && pawnSpawned.Faction != null && pawnSpawned.Faction.IsPlayer && cas != null && cas.isSurrogate && cas.externalController == null)
                        {
                            __result = true;
                            return;
                        }
                    }
                }
            }
        }

        /*
         * Prefix permetant de jerter en fonction de la config les surrogates des listings
         */
        [HarmonyPatch(typeof(MapPawns), "get_FreeColonists")]
        public class get_FreeColonists_Patch
        {
            [HarmonyPostfix]
            public static void Listener(ref List<Pawn> __result)
            {
                __result.RemoveAll(pawn => pawn.Faction != Faction.OfPlayer || pawn.HostFaction != null || !pawn.RaceProps.Humanlike || pawn.IsSurrogateAndroid(false, true));
            }

            //[HarmonyPrefix]
            //public static bool Listener(ref List<Pawn> __result, MapPawns __instance, Dictionary<Faction, List<Pawn>> ___freeHumanlikesOfFactionResult)
            //{
            //    try
            //    {
            //        if (!Settings.hideInactiveSurrogates)
            //            return true;

            //        List<Pawn> list;
            //        if (!___freeHumanlikesOfFactionResult.TryGetValue(Faction.OfPlayer, out list))
            //        {
            //            list = new List<Pawn>();
            //            ___freeHumanlikesOfFactionResult.Add(Faction.OfPlayer, list);
            //        }
            //        else
            //        {
            //            list.Clear();
            //        }

            //        List<Pawn> allPawns = __instance.AllPawns;
            //        for (int i = 0; i < allPawns.Count; i++)
            //        {
            //            var pawn = allPawns[i];
            //            if (pawn.Faction == Faction.OfPlayer && pawn.HostFaction == null && pawn.RaceProps.Humanlike && !pawn.IsSurrogateAndroid(false, true))
            //            {
            //                list.Add(pawn);
            //            }
            //        }

            //        __result = list;

            //        return false;
            //    }
            //    catch (Exception e)
            //    {
            //        Log.Message("[ATPP] MapPawns.get_FreeColonists " + e.Message + " " + e.StackTrace);

            //        return true;
            //    }
            //}
        }
    }
}