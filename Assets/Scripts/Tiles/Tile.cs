using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [Header("General Variable")]
    public int Id;
    public int RessourceGroupId;
    public int Type;
    public int Cost;

    [Header("Shared Data")]
    public GameProperties GameProperties;
    public RessourceGroups RessourceGroups;

    [Header("Game Events")]
    public GameEvent CityPropertiesUpdated;
    public GameEvent UpdateGroups;

    [Header("Local Amount of Ressources")]
    public int[] LocalRessources;

    [Header("Max Amount of Ressources")]
    public int[] AmountRessourcesMax;

    [Header("Running Costs Per Hour")]
    public int[] RunningCosts;

    [Header("Wear Per Hour")]
    public float[] WearPerHour;

    [Header("Maximal Harvest Rates")]
    public int[] MaximalHarvestRates;

    [Header("Material Cost Per Unit")]
    public int[] MaterialCost;

    [Header("Production Output")]
    public int[] ProductionOutput;

    [Header("Maximal Production Count")]
    public int MaximalProductionCount;

    [Header("Efficiency")]
    public float TileEfficiency;


    private void ProducesOutput()
    {
        int productionCycleCounter = 0;
        while (CanProduce() && productionCycleCounter < MaximalProductionCount)
        {
            for (int i = 0; i < LocalRessources.Length; i++)
            {
                LocalRessources[i] -= MaterialCost[i];
            }
            RessourceGroups.UnemployedCitizen[RessourceGroupId] += ProductionOutput[0];
            RessourceGroups.RawMaterials[RessourceGroupId] += ProductionOutput[1];
            RessourceGroups.ProcessedMaterials[RessourceGroupId] += ProductionOutput[2];
            RessourceGroups.Students[RessourceGroupId] += ProductionOutput[3];
            RessourceGroups.Professors[RessourceGroupId] += ProductionOutput[4];
            RessourceGroups.Workers[RessourceGroupId] += ProductionOutput[5];
            productionCycleCounter++;
        }
        TileEfficiency = productionCycleCounter / MaximalProductionCount;
    }

    private bool CanProduce()
    {
        for (int i = 0; i < MaterialCost.Length; i++)
        {
            if (LocalRessources[i] < MaterialCost[i]) { return false; }
        }
        return true;
    }


    private void CalculateCostsPerHour()
    {
        for (int i = 0; i < RunningCosts.Length; i++)
        {
            GameProperties.Cash -= RunningCosts[i] * LocalRessources[i];

            if (GameProperties.Cash <= 0 && LocalRessources[i] > 0)
            {
                LocalRessources[i] -= 1;
            }
        }
    }

    private void ApplyWearPerHour()
    {
        for (int i = 0; i < LocalRessources.Length; i++)
        {
            if (LocalRessources[i] != 0)
            {
                LocalRessources[i] -= (int)((float)LocalRessources[i] * WearPerHour[i]);
            }
        }
    }

    private void HarvestRessourcesPerHour()
    {
        HarvestRessource(RessourceGroups.UnemployedCitizen, 0);
        HarvestRessource(RessourceGroups.RawMaterials, 1);
        HarvestRessource(RessourceGroups.ProcessedMaterials, 2);
        HarvestRessource(RessourceGroups.Students, 3);
        HarvestRessource(RessourceGroups.Professors, 4);
        HarvestRessource(RessourceGroups.Workers, 5);
    }

    private void HarvestRessource(Dictionary<int, int> ressourceDictionary, int ressourceIndex)
    {
        int checkUnemp = ressourceDictionary[RessourceGroupId] - MaximalHarvestRates[ressourceIndex];
        int diff = 0;
        if (checkUnemp < 0)
        {
            diff = (MaximalHarvestRates[0] - checkUnemp);
            RessourceGroups.UnemployedCitizen[RessourceGroupId] -= diff;
            LocalRessources[ressourceIndex] += diff;
        }
        else
        {
            RessourceGroups.UnemployedCitizen[RessourceGroupId] -= MaximalHarvestRates[ressourceIndex];
            LocalRessources[ressourceIndex] += MaximalHarvestRates[ressourceIndex];
        }
    }
}
