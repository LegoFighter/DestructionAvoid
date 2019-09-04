using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class RessourceGroups : ScriptableObject
{
    public Dictionary<int, int> UnemployedCitizen;
    public Dictionary<int, int> RawMaterials;
    public Dictionary<int, int> ProcessedMaterials;

}
