using Verse;
using HarmonyLib;
using RimWorld;

namespace MOARANDROIDS
{
    internal class WorkGiver_TakeToBedToOperate_Patch

    {
        /*
         * Allow crafters to do doctor jobs (for androids)
         */
        [HarmonyPatch(typeof(WorkGiver_TakeToBedToOperate), "HasJobOnThing")]
        public class HasJobOnThing_Patch
        {
            [HarmonyPostfix]
            public static void Listener(Pawn pawn, Thing t, bool forced, ref bool __result, WorkGiver_TakeToBedToOperate __instance)
            {
                Utils.genericPostFixExtraCrafterDoctorJobs(pawn, t, forced, ref __result, __instance);
            }
        }
    }
}