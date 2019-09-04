using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RessourceHandler : MonoBehaviour
{
    public RessourceGroups RessourceGroups;
    public GameProperties GameProperties;
    private int ressourceGroupsAmount = 10;

    public void Start()
    {
        RessourceGroups.UnemployedCitizen = new Dictionary<int, int>();
        RessourceGroups.RawMaterials = new Dictionary<int, int>();
        RessourceGroups.ProcessedMaterials = new Dictionary<int, int>();

        ressourceGroupsAmount = 10;

        for (int i = 0; i < ressourceGroupsAmount; i++)
        {
            RessourceGroups.UnemployedCitizen.Add(i, 0);
            RessourceGroups.RawMaterials.Add(i, 0);
            RessourceGroups.ProcessedMaterials.Add(i, 0);
        }

    }

}
