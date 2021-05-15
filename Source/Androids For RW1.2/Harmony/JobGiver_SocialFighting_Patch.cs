using Verse;
using Verse.AI;
using HarmonyLib;
using RimWorld;

namespace MOARANDROIDS
{
    internal class JobGiver_SocialFighting_Patch

    {
        /*
         * PAS DE SOCIAL FIGHT POUR LES T1/T2
         */
        [HarmonyPatch(typeof(JobGiver_SocialFighting), "TryGiveJob")]
        public class GetPriority_Patch
        {
            [HarmonyPostfix]
            public static void Listener(Pawn pawn, ref Job __result)
            {
                Pawn otherPawn = ((MentalState_SocialFighting)pawn.MentalState).otherPawn;
                if (pawn.IsBasicAndroidTier() || otherPawn.IsBasicAndroidTier())
                {
                    __result = null;
                }
            }
        }
    }
}