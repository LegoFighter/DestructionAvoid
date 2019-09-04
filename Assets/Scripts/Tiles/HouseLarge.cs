using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseLarge : MonoBehaviour
{


    public GameEvent CityPropertiesUpdated;
    public GameEvent UpdateGroups;

    [Header("Shared Data")]
    public GameProperties GameProperties;
    public RessourceGroups RessourceGroups;

    public float AmountOfCitizen = 2000;


    private Tile baseTile;

    [SerializeField]
    private float birthRate = .02f;

    void Start()
    {
        baseTile = GetComponent<Tile>();
    }
    public void OnMinuteTick()
    {
        populationIncrease();
        CityPropertiesUpdated.Raise();
    }

    public void OnQuarterTick()
    {
        int citizenDuty = (int)(AmountOfCitizen * 0.2f);
        int rawMaterialsDuty = 0;
        int processedMaterialsDuty = 0;


        int citizenAdmission = 0;
        int rawMaterialsAdmission = 0;
        int processedMaterialsAdmission = 0;

        RemoveRessources(citizenDuty, rawMaterialsDuty, processedMaterialsDuty);
        AddRessources(citizenAdmission, rawMaterialsAdmission, processedMaterialsAdmission);

        UpdateGroups.Raise();
    }

    private void populationIncrease()
    {
        int bornCitizen = (int)(AmountOfCitizen / birthRate);
        GameProperties.Population += bornCitizen;
        CityPropertiesUpdated.Raise();
    }

    private void RemoveRessources(int citizen, int rawMaterials, int processedMaterials)
    {
        RessourceGroups.UnemployedCitizen[baseTile.RessourceGroupId] -= citizen;
        RessourceGroups.RawMaterials[baseTile.RessourceGroupId] -= rawMaterials;
        RessourceGroups.ProcessedMaterials[baseTile.RessourceGroupId] -= processedMaterials;
    }

    private void AddRessources(int citizen, int rawMaterials, int processedMaterials)
    {
        RessourceGroups.UnemployedCitizen[baseTile.RessourceGroupId] += citizen;
        RessourceGroups.RawMaterials[baseTile.RessourceGroupId] += rawMaterials;
        RessourceGroups.ProcessedMaterials[baseTile.RessourceGroupId] += processedMaterials;
    }
}
