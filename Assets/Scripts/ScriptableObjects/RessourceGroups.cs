﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class RessourceGroups : ScriptableObject
{
    public Dictionary<int, int> AmountOfLocalCitizen;
    public Dictionary<int, int> AmountOfStudents;
    public Dictionary<int, int> AmountOfProfessors;
    public Dictionary<int, int> AmountOfWorkers;
    public Dictionary<int, int> AmountOfUnemployedCitizens;

}