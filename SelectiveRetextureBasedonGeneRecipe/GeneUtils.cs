using System;
using System.Collections.Generic;
using System.Linq;
using SBS;
using Verse;

namespace SelectiveRetextureBasedonGeneRecipe
{
    public class GeneUtils
    {
        public static void ClearGene(Pawn pawn)
        {
            List<string> geneDefsToRemove = new List<string>();
            List<Gene> waitForRemove = new List<Gene>();
            foreach (var geneDef in Enum.GetValues(typeof(ModBodyGeneEnums)))
            {
                geneDefsToRemove.Add(geneDef.ToString());
            }

            foreach (var geneDef in Enum.GetValues(typeof(DefaultBodyGeneEnums)))
            {
                geneDefsToRemove.Add(geneDef.ToString());
            }

            foreach (var gene in pawn.genes.GenesListForReading)
            {
                if (geneDefsToRemove.Contains(gene.def.defName))
                {
                    Log.Message("will remove gene:" + gene.Label + " from pawn: " + pawn.Name.ToStringShort);
                    waitForRemove.Add(gene);
                }
            }

            foreach (var gene in waitForRemove)
            {
                pawn.genes.RemoveGene(gene);
            }
        }

        public static bool HasGeneDef(Pawn pawn, params ModBodyGeneEnums[] genes)
        {
            if (pawn == null || pawn.genes == null)
            {
                Log.Message("GeneUtils.HasGeneDef called with null pawn or genes. Returning false");
                return false;
            }
            foreach (var gene in genes)
            {
                if (DefDatabase<GeneDef>.GetNamedSilentFail(gene.ToString()) == null)
                {
                    Log.Message($"Cannot find gene definition for {gene}. Returning false.");
                    return false;
                }
            }
            return true;
        }
    }
}