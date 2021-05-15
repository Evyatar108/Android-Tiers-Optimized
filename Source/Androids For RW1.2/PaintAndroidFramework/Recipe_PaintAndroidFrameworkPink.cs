﻿using System.Collections.Generic;
using RimWorld;
using Verse;

namespace MOARANDROIDS
{
    public class Recipe_PaintAndroidFrameworkPink : Recipe_SurgeryAndroids
    {

        public override void ApplyOnPawn(Pawn pawn, BodyPartRecord part, Pawn billDoer, List<Thing> ingredients, Bill bill)
        {
            CompAndroidState cas = pawn.TryGetComp<CompAndroidState>();

            if (cas == null)
                return;

            cas.customColor = (int)AndroidPaintColor.Pink;
            this.applyFrameworkColor(pawn);
        }

    }
}