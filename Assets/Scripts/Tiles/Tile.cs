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
    public string TileName;

    public bool InDangerZone;

    [Header("Shared Data")]
    public GameProperties GameProperties;
    public RessourceGroups RessourceGroups;

    [Header("Game Events")]
    public GameEvent CityPropertiesUpdated;
    public GameEvent UpdateGroups;
    public GameEvent TileInfoUpdate;
    public GameEvent BonusApplied;

    [Header("Ressource Order")]
    [SerializeField]
    private string[] ressourceOrder = new string[] { "Raw Materials", "Processed Materials", "Unemployed Citizien", "Students", "Professors", "Workers" };

    [Header("Ressource Options")]
    public int[] LocalRessources;

    public int[] AmountRessourcesMax;

    public int[] RunningCosts;

    public int[] WearPerHour;

    public int[] MaximalHarvestRates;

    public int[] MaterialCost;

    public int[] ProductionOutput;

    public int MaximalProductionCount;

    public float TileEfficiency;

    void Start()
    {
        InDangerZone = false;
    }

    public void Outputting()
    {
        ProducesOutput();
        TileInfoUpdate.Raise();
        CityPropertiesUpdated.Raise();
        UpdateGroups.Raise();
    }

    public void Harvesting()
    {
        HarvestRessourcesPerHour();
        CalculateCostsPerHour();
        ApplyWearPerHour();
        TileInfoUpdate.Raise();
        CityPropertiesUpdated.Raise();
        UpdateGroups.Raise();
    }

    public void OnQuarterTick()
    {
        TileInfoUpdate.Raise();
    }
    private void ProducesOutput()
    {
        int productionCycleCounter = 0;
        while (CanProduce() && productionCycleCounter < MaximalProductionCount)
        {
            for (int i = 0; i < LocalRessources.Length; i++)
            {
                LocalRessources[i] -= MaterialCost[i];
            }
            RessourceGroups.RawMaterials[RessourceGroupId] += ProductionOutput[0];
            RessourceGroups.ProcessedMaterials[RessourceGroupId] += ProductionOutput[1];
            RessourceGroups.UnemployedCitizen[RessourceGroupId] += ProductionOutput[2];
            RessourceGroups.Students[RessourceGroupId] += ProductionOutput[3];
            RessourceGroups.Professors[RessourceGroupId] += ProductionOutput[4];
            RessourceGroups.Workers[RessourceGroupId] += ProductionOutput[5];
            productionCycleCounter++;
        }
        TileEfficiency = (float)productionCycleCounter /(float) MaximalProductionCount;
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
            if (LocalRessources[i] > 0)
            {
                LocalRessources[i] -= WearPerHour[i];

                if (LocalRessources[i] < 0)
                    LocalRessources[i] = 0;

            }
        }
    }


    private void HarvestRessourcesPerHour()
    {

        HarvestRessource(RessourceGroups.RawMaterials, 0);
        HarvestRessource(RessourceGroups.ProcessedMaterials, 1);
        HarvestRessource(RessourceGroups.UnemployedCitizen, 2);
        HarvestRessource(RessourceGroups.Students, 3);
        HarvestRessource(RessourceGroups.Professors, 4);
        HarvestRessource(RessourceGroups.Workers, 5);
    }

    private void HarvestRessource(Dictionary<int, int> ressourceDictionary, int ressourceIndex)
    {
        int cappedMaximum = AmountRessourcesMax[ressourceIndex] - LocalRessources[ressourceIndex];
        int regularMaximum = MaximalHarvestRates[ressourceIndex];

        if(cappedMaximum < regularMaximum) {
            regularMaximum = cappedMaximum;
        }

        if (ressourceDictionary[RessourceGroupId] > 0 && regularMaximum > 0 && cappedMaximum > 0)
        {
            int ressourceCheck = ressourceDictionary[RessourceGroupId] - regularMaximum;

            if (ressourceCheck <= 0)
            {
                int diff = Mathf.Abs(ressourceCheck);
                ressourceDictionary[RessourceGroupId] -= diff;
                LocalRessources[ressourceIndex] += diff;
            }
            else
            {
                ressourceDictionary[RessourceGroupId] -= regularMaximum;
                LocalRessources[ressourceIndex] += regularMaximum;
            }

            if (ressourceDictionary[RessourceGroupId] < 0)
                ressourceDictionary[RessourceGroupId] = 0;
        }



    }
}
