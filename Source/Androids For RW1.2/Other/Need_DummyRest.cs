using System;
using Verse;
using RimWorld;

namespace MOARANDROIDS
{
    public class Need_DummyRest : Need_Rest
    {

        public new RestCategory CurCategory
        {
            get
            {
                return RestCategory.Rested;
            }
        }

        public Need_DummyRest(Pawn pawn) : base(pawn)
        {

        }

        public override void NeedInterval()
        {
            throw new NotImplementedException();
        }

        public override void SetInitialLevel()
        {
            this.CurLevel = Rand.Range(0.9f, 1f);
        }
    }
}