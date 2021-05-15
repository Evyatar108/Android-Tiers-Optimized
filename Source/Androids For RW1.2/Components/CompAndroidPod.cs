using Verse;
using RimWorld;

namespace MOARANDROIDS
{
    public class CompAndroidPod : ThingComp
    {
        public override void CompTick()
        {
            if (!this.parent.Spawned)
            {
                return;
            }

            int CGT = Find.TickManager.TicksGame;
            if (CGT % 60 == 0)
            {
                //Rafraichissement qt de courant consommé
                refreshPowerConsumed();
            }
        }

        public float getPowerConsumed()
        {
            CompPowerTrader cpt = this.parent.TryGetComp<CompPowerTrader>();

            if (cpt == null)
                return 0;
            else
                return getCurrentAndroidPowerConsumed() + cpt.Props.basePowerConsumption;
        }


        public void refreshPowerConsumed()
        {
            this.parent.TryGetComp<CompPowerTrader>().powerOutputInt = -getPowerConsumed();
        }

        public int getCurrentAndroidPowerConsumed()
        {
            int ret = 0;

            Building_Bed bed = (Building_Bed)parent;

            foreach (var cp in bed.CurOccupants)
            {
                //Il sagit d'un android 
                if (cp != null && Utils.ExceptionAndroidList.Contains(cp.def.defName))
                {
                    ret += Utils.getConsumedPowerByAndroid(cp.def.defName);
                }
            }
            return ret;
        }
    }
}