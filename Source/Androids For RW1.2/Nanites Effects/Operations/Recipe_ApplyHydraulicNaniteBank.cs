﻿using System.Collections.Generic;
using RimWorld;
using Verse;

namespace MOARANDROIDS
{
    public class Recipe_ApplyHydraulicNaniteBank : Recipe_SurgeryAndroids
    {

        public override void ApplyOnPawn(Pawn pawn, BodyPartRecord part, Pawn billDoer, List<Thing> ingredients, Bill bill)
        {
            CompUseEffect_HealHydraulicSystem.Apply(pawn);
        }

    }
}