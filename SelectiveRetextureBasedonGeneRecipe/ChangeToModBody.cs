using System;
using System.Collections.Generic;
using System.Net;
using RimWorld;
using SBS;
using Verse;

namespace SelectiveRetextureBasedonGeneRecipe
{
    public abstract class AbstractChangeToRecipe : Recipe_InstallImplant
    {
        protected bool addAsXenogene = false;
        public override void ApplyOnPawn(Pawn pawn, BodyPartRecord part, Pawn billDoer, List<Thing> ingredients,
            Bill bill)
        {
            if (billDoer != null)
            {
                TaleRecorder.RecordTale(TaleDefOf.DidSurgery, new object[]
                {
                    billDoer,
                    pawn
                });
                DoChange(pawn);
                pawn.Drawer.renderer.SetAllGraphicsDirty();
            }
        }
        public override bool AvailableOnNow(Thing thing, BodyPartRecord part = null)
        {
            if (thing is Pawn pawn && pawn.IsCreepJoiner)
            {
                return false;
            }
            return base.AvailableOnNow(thing, part);
        }
        protected abstract void DoChange(Pawn pawn);
    }

    public class ChangeToCategoryRecipe : AbstractChangeToRecipe
    {
        protected override void DoChange(Pawn pawn)
        {
            pawn.genes.AddGene(DefDatabase<GeneDef>.GetNamed(ModBodyGeneEnums.SelectiveRetextureGenes.ToString()),
                addAsXenogene);
        }

        public override bool AvailableOnNow(Thing thing, BodyPartRecord part = null)
        {
            if (thing is Pawn pawn && GeneUtils.HasGeneDef(pawn, ModBodyGeneEnums.SelectiveRetextureGenes))
            {
                return base.AvailableOnNow(thing, part);
            }

            return false;
        }
    }

    public class ChangeToHoomanCuteRecipe : AbstractChangeToRecipe
    {
        protected override void DoChange(Pawn pawn)
        {
            GeneUtils.ClearGene(pawn);
            ModBodyGeneEnums target = pawn.gender == Gender.Male
                ? ModBodyGeneEnums.Gene_HCMale
                : ModBodyGeneEnums.Gene_HCFemale;
            pawn.genes.AddGene(DefDatabase<GeneDef>
                .GetNamed(target.ToString()), addAsXenogene);
        }

        public override bool AvailableOnNow(Thing thing, BodyPartRecord part = null)
        {
            if (thing is Pawn pawn && GeneUtils.HasGeneDef(pawn, ModBodyGeneEnums.Gene_HCMale,
                    ModBodyGeneEnums.Gene_HCFemale))
            {
                return base.AvailableOnNow(thing, part);
            }
            return false;
        }
    }

    public class ChangeToRealisticBodyRecipe : AbstractChangeToRecipe
    {
        protected override void DoChange(Pawn pawn)
        {
            GeneUtils.ClearGene(pawn);
            pawn.genes.AddGene(DefDatabase<GeneDef>
                .GetNamed(ModBodyGeneEnums.Gene_RealisticBody.ToString()), addAsXenogene);
        }

        public override bool AvailableOnNow(Thing thing, BodyPartRecord part = null)
        {
            if (thing is Pawn pawn && pawn.gender == Gender.Female
                                   && GeneUtils.HasGeneDef(pawn, ModBodyGeneEnums.Gene_RealisticBody))
            {
                return base.AvailableOnNow(thing, part);
            }

            return false;
        }
    }

    public class ChangeToXevaRecipe : AbstractChangeToRecipe
    {
        protected override void DoChange(Pawn pawn)
        {
            GeneUtils.ClearGene(pawn);
            ModBodyGeneEnums target = pawn.gender == Gender.Male
                ? ModBodyGeneEnums.Gene_XevaMale
                : ModBodyGeneEnums.Gene_XevaFemale;
            pawn.genes.AddGene(DefDatabase<GeneDef>
                .GetNamed(target.ToString()), addAsXenogene);
        }
        public override bool AvailableOnNow(Thing thing, BodyPartRecord part = null)
        {
            if (thing is Pawn pawn && GeneUtils.HasGeneDef(pawn, ModBodyGeneEnums.Gene_XevaMale,
                    ModBodyGeneEnums.Gene_XevaFemale))
            {
                return base.AvailableOnNow(thing, part);
            }
            return false;
        }
    }

    public class ChangeToStandRecipe : AbstractChangeToRecipe
    {
        protected override void DoChange(Pawn pawn)
        {
            GeneUtils.ClearGene(pawn);
            pawn.genes.AddGene(DefDatabase<GeneDef>
                .GetNamed(DefaultBodyGeneEnums.Body_Standard.ToString()), addAsXenogene);
        }
    }
}