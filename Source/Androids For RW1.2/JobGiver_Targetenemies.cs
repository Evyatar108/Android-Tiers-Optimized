using RimWorld;
using Verse;
using Verse.AI;

namespace MOARANDROIDS
{
    // Token: 0x0200002A RID: 42
    public class JobGiver_TargetEnemiesSwarm : ThinkNode_JobGiver
    {
        // Token: 0x06000082 RID: 130 RVA: 0x00004484 File Offset: 0x00002684
        protected override Job TryGiveJob(Pawn pawn)
        {
            Job result;
            if (pawn.TryGetAttackVerb(null, false) == null)
            {
                result = null;
            }
            else
            {
                Pawn pawn2 = this.FindPawnTarget(pawn);
                if (pawn2 != null)
                {
                    result = this.MeleeAttackJob(pawn, pawn2);
                }
                else
                {
                    using (PawnPath pawnPath = pawn.Map.pathFinder.FindPath(pawn.Position, pawn2.Position, TraverseParms.For(pawn, Danger.None, TraverseMode.PassDoors, false), PathEndMode.OnCell))
                    {
                        if (!pawnPath.Found)
                        {
                            return null;
                        }

                        if (!pawnPath.TryFindLastCellBeforeBlockingDoor(pawn, out _))
                        {
                            Log.Error(pawn + " did TryFindLastCellBeforeDoor but found none when it should have been one. Target: " + pawn2.LabelCap);
                            return null;
                        }
                    }
                    result = null;
                }
            }
            return result;
        }

        private Job MeleeAttackJob(Pawn pawn, Thing target)
        {
            return new Job(JobDefOf.AttackMelee, target)
            {
                maxNumMeleeAttacks = 999,
                expiryInterval = 999999,
                attackDoorIfTargetLost = true
            };
        }

        private Pawn FindPawnTarget(Pawn pawn)
        {
            return (Pawn)AttackTargetFinder.BestAttackTarget(pawn, TargetScanFlags.NeedThreat, (Thing x) => x is Pawn, 0f, 100f, default(IntVec3), float.MaxValue, true);
        }
    }
}
