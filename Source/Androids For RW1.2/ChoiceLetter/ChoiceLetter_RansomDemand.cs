using System;
using System.Collections.Generic;
using Verse;
using RimWorld;

namespace MOARANDROIDS
{
    public class ChoiceLetter_RansomDemand : ChoiceLetter
    {
        public Faction faction;
        public int fee;
        public HashSet<string> cryptolockedThings;
        public bool deviceType = false;

        public override IEnumerable<DiaOption> Choices
        {
            get
            {
                if (base.ArchivedOnly)
                {
                    yield return base.Option_Close;
                }
                else
                {
                    DiaOption accept = new DiaOption("RansomDemand_Accept".Translate());
                    accept.action = delegate
                    {
                        Utils.anyPlayerColonnyPaySilver(fee);

                        //Check si la faction tient parole
                        if (Rand.Chance(1.0f - Settings.riskCryptolockerScam))
                        {
                            //Suppression cryptolocker des surrogates
                            for (int i = 0; i < Find.Maps.Count; i++)
                            {
                                Map map = Find.Maps[i];
                                for (int i1 = 0; i1 < map.listerThings.AllThings.Count; i1++)
                                {
                                    Thing t = map.listerThings.AllThings[i1];
                                    if (cryptolockedThings.Contains(t.GetUniqueLoadID()))
                                    {
                                        try
                                        {
                                            if (t is Pawn)
                                            {
                                                Pawn p = (Pawn)t;
                                                if (p.IsSurrogateAndroid())
                                                {
                                                    p.health.AddHediff(Utils.hediffNoHost);
                                                }
                                            }
                                            else
                                            {
                                                t.SetFaction(Faction.OfPlayer);
                                                CompFlickable cf = t.TryGetComp<CompFlickable>();
                                                if (cf != null)
                                                {
                                                    cf.SwitchIsOn = true;
                                                }
                                            }
                                            CompSkyMind csm = t.TryGetComp<CompSkyMind>();
                                            if (csm != null)
                                                csm.Infected = -1;
                                        }
                                        catch (Exception)
                                        {

                                        }
                                    }
                                }
                            }

                            if (deviceType)
                                Messages.Message("ATPP_CryptolockerDeviceClearedByFaction".Translate(faction.Name), MessageTypeDefOf.PositiveEvent);
                            else
                                Messages.Message("ATPP_CryptolockerClearedByFaction".Translate(faction.Name), MessageTypeDefOf.PositiveEvent);
                        }
                        else
                        {
                            //ATPP_LetterFactionScamCryptolocker
                            Find.LetterStack.ReceiveLetter("ATPP_LetterFactionScam".Translate(), "ATPP_LetterFactionScamCryptolockerDesc".Translate(faction.Name), LetterDefOf.ThreatBig);
                        }

                        Find.LetterStack.RemoveLetter(this);
                    };
                    accept.resolveTree = true;
                    if (!Utils.anyPlayerColonnyHasEnoughtSilver(fee))
                    {
                        accept.Disable("NeedSilverLaunchable".Translate(this.fee.ToString()));
                    }
                    yield return accept;
                    yield return base.Option_Reject;
                    yield return base.Option_Postpone;
                }
            }
        }

        public override bool CanShowInLetterStack
        {
            get
            {
                return base.CanShowInLetterStack;
            }
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Collections.Look(ref cryptolockedThings, "ATPP_cryptolockedThings", LookMode.Value);
            Scribe_References.Look<Faction>(ref this.faction, "ATPP_faction", false);
            Scribe_Values.Look<int>(ref this.fee, "ATPP_fee", 0, false);
            Scribe_Values.Look<bool>(ref this.deviceType, "ATPP_deviceType", false, false);
        }
    }
}