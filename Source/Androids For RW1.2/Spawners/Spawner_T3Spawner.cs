using System.Collections.Generic;
using Verse;
using RimWorld;

namespace MOARANDROIDS
{
    public class CompAndroidSpawner3T : ThingComp
    {

        public override void CompTick()
        {
            this.CheckShouldSpawn();
        }

        private void CheckShouldSpawn()
        {
            if (true)
            {
                this.SpawnDude();
                this.parent.Destroy();
            }
        }

        public void SpawnDude()
        {
            PawnKindDef pawnKindDef = new List<PawnKindDef>
            {
                PawnKindDefOf.AndroidT3Colonist
            }.RandomElement<PawnKindDef>();
            PawnGenerationRequest request = new PawnGenerationRequest(pawnKindDef, Faction.OfPlayer, PawnGenerationContext.NonPlayer);
            Pawn pawn = PawnGenerator.GeneratePawn(request);

            //TODO: Implement, make work, test.
            //Pawn originalCloned = parent.TryGetComp<ThingyHolderThatsHoldingAClonedPawn>();
            //pawn.story = originalCloned.story;

            GenSpawn.Spawn(pawn, parent.Position, parent.Map);
        }
    }
}