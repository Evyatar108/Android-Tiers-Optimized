using Verse;
using HarmonyLib;
using RimWorld;

namespace MOARANDROIDS
{
    internal class ThoughtWorker_NeedJoy_Patch

    {
        /*
         * PostFix servant a desactivé les moods liés a la joie pour les T1 et T2
         */
        [HarmonyPatch(typeof(ThoughtWorker_NeedJoy), "CurrentStateInternal")]
        public class CurrentStateInternal_Patch
        {
            [HarmonyPostfix]
            public static void Listener(Pawn p, ref ThoughtState __result)
            {
                if (Utils.ExceptionAndroidListBasic.Contains(p.def.defName))
                {
                    __result = ThoughtState.Inactive;
                }
            }
        }
    }
}