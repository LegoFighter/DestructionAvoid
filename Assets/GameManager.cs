using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameProperties GameProperties;
    public TileHandler TileHandler;

    [Header("Game Events")]
    public GameEvent CityPropertiesUpdated;
    public GameEvent CountdownUpdateUI;
    public GameEvent CountdownOver;


    public void Start()
    {

        GameProperties.Cash = 0;
        GameProperties.AverageEducationLevel = 0;
        GameProperties.Population = 0;
        GameProperties.StageOfRocketCompletion = 0;
        GameProperties.DistanceToFromEarth = 0;
        GameProperties.AsteroidSpeed = 0;
        GameProperties.AmountOfTiles = 0;
        GameProperties.CountdownHoursLeft = 24;
        GameProperties.CountdownMinutesLeft = 0;
       
        CountdownUpdateUI.Raise();
        CityPropertiesUpdated.Raise();
    }

    public void CountdownMinute()
    {
        if (GameProperties.CountdownMinutesLeft == 0)
        {
            GameProperties.CountdownHoursLeft--;

            if (GameProperties.CountdownHoursLeft == 0 && GameProperties.CountdownMinutesLeft == 0)
            {
                CountdownUpdateUI.Raise();
                CountdownOver.Raise();
                return;
            }
            GameProperties.CountdownMinutesLeft = 59;
        }
        else
        {
            GameProperties.CountdownMinutesLeft--;
        }
        CountdownUpdateUI.Raise();
    }



}
