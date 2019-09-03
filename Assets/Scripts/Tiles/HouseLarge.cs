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
    }

    private void populationIncrease()
    {
        int bornCitizen = (int)(baseTile.AmountOfLocalCitizen / birthRate);
        baseTile.AmountOfLocalCitizen += bornCitizen;
        GameProperties.Population += bornCitizen;

        RessourceGroups.AmountOfLocalCitizen[baseTile.RessourceGroupId] += bornCitizen;

        CityPropertiesUpdated.Raise();
        UpdateGroups.Raise();

    }
}
