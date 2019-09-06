﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameProperties : ScriptableObject
{
    public int Cash;
    public float AverageEducationLevel;
    public int Population;
    // public int AmountOfTiles;
    public int ActiveRessourceGroup;
    public int CountdownMinutesLeft;
    public int CountdownHoursLeft;
    public Tile SelectedTile;
    public List<GameObject> TilesToDelete;
    
}
