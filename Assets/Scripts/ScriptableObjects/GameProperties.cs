using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameProperties : ScriptableObject
{
    public int NextSharedRessourcesId;
    public int Money;
    public float AverageEducationLevel;
    public int Population;
    public float StageOfRocketCompletion;
    public float DistanceToFromEarth;
    public int AsteroidSpeed;
}
