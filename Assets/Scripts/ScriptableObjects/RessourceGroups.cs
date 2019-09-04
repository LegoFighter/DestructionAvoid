using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class RessourceGroups : ScriptableObject
{
    public Dictionary<int, int> UnemployedCitizen;
    public Dictionary<int, int> RawMaterials;
    public Dictionary<int, int> ProcessedMaterials;
    public Dictionary<int, int> Students;
    public Dictionary<int, int> Professors;
    public Dictionary<int, int> Workers;
}
