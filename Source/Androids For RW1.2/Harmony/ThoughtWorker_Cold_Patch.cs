using Verse;
using HarmonyLib;
using RimWorld;

namespace MOARANDROIDS
{
    internal class ThoughtWorker_Cold_Patch

    {
        /*
         * PostFix évitant d'attribuer de need comfort et outdoor aux T1 et T2 et l'hygiene a l'ensemble des robots
         */
        [HarmonyPatch(typeof(ThoughtWorker_Cold), "CurrentStateInternal")]
        public class CurrentStateInternal_Patch
        {
            [HarmonyPostfix]
            public static void Listener(Pawn p, ref ThoughtState __result)
            {
                if (Utils.IsBasicAndroidTier(p) || Utils.pawnCurrentlyControlRemoteSurrogate(p))
                {
                    __result = ThoughtState.Inactive;
                }
            }
        }
    }
}