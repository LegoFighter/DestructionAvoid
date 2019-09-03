using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameProperties GameProperties;
    public TileHandler TileHandler;

    public void Start()
    {
        GameProperties.Cash = 0;
        GameProperties.AverageEducationLevel = 0;
        GameProperties.Population = 0;
        GameProperties.StageOfRocketCompletion = 0;
        GameProperties.DistanceToFromEarth = 0;
        GameProperties.AsteroidSpeed = 0;
        GameProperties.AmountOfStructures = new Dictionary<int, int>();

        for (int i = 0; i < TileHandler.Tiles.Length; i++)
        {
            GameProperties.AmountOfStructures.Add(i + 1, 0);
        }
    }



}
