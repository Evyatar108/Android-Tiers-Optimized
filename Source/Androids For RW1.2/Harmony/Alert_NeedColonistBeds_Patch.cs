using Verse;
using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System;

namespace MOARANDROIDS
{
    internal class Alert_NeedColonistBeds_Patch
    {
        [HarmonyPatch(typeof(Alert_NeedColonistBeds), "NeedColonistBeds")]
        public class NeedColonistBeds_Patch
        {
            [HarmonyPrefix]
            public static bool Listener(Map map, ref bool __result)
            {
                try
                {
                    if (!map.IsPlayerHome)
                    {
                        return false;
                    }
                    int num = 0;
                    int num2 = 0;
                    List<Building> allBuildingsColonist = map.listerBuildings.allBuildingsColonist;
                    for (int i = 0; i < allBuildingsColonist.Count; i++)
                    {
                        Building_Bed building_Bed = allBuildingsColonist[i] as Building_Bed;
                        if (building_Bed != null && !building_Bed.ForPrisoners && !building_Bed.Medical && building_Bed.def.building.bed_humanlike)
                        {
                            if (building_Bed.SleepingSlotsCount == 1)
                            {
                                num++;
                            }
                            else
                            {
                                num2++;
                            }
                        }
                    }
                    int num3 = 0;
                    int num4 = 0;
                    for (int i = 0; i < map.mapPawns.FreeColonistsSpawned.Count; i++)
                    {
                        Pawn current = map.mapPawns.FreeColonistsSpawned[i];
                        //On ignore les androis dans la comptabilisation
                        if (Utils.ExceptionAndroidList.Contains(current.def.defName))
                            continue;

                        Pawn pawn = LovePartnerRelationUtility.ExistingMostLikedLovePartner(current, false);
                        if (pawn == null || !pawn.Spawned || pawn.Map != current.Map || pawn.Faction != Faction.OfPlayer || pawn.HostFaction != null)
                        {
                            num3++;
                        }
                        else
                        {
                            num4++;
                        }
                    }

                    num2 -= num4 / 2;
                    if (num2 < 0)
                    {
                        num += num2 * 2;
                        num2 = 0;
                    }

                    num2 -= num3;
                    if (num2 < 0)
                    {
                        num += num2;
                        num2 = 0;
                    }

                    __result = num < 0 || num2 < 0;

                    return false;
                }
                catch (Exception e)
                {
                    Log.Message("[ATPP] ALert_NeedColonistBeds.NeedColonistBeds :" + e.Message + " - " + e.StackTrace);
                    return true;
                }
            }
        }
    }
}