using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameProperties GameProperties;
    public AsteroidData AsteroidData;
    public GameObject Asteroid;

    [Header("Game Events")]
    public GameEvent CityPropertiesUpdated;
    public GameEvent CountdownUpdateUI;
    public GameEvent CountdownOver;
    public GameEvent GameOver;


    [SerializeField]
    private bool runningState;

    public void Start()
    {
        runningState = true;

        GameProperties.Cash = 0;
        GameProperties.AverageEducationLevel = 0;
        GameProperties.Population = 0;
        GameProperties.StageOfRocketCompletion = 0;
        GameProperties.AmountOfTiles = 0;
        GameProperties.CountdownHoursLeft = 24;
        GameProperties.CountdownMinutesLeft = 0;
        GameProperties.ActiveRessourceGroup = 0;

        AsteroidData.Asteroid = Asteroid;
        AsteroidData.AsteroidSpeed = 50000;
        AsteroidData.DistanceToFromEarth = 1000000;

        CountdownUpdateUI.Raise();
        CityPropertiesUpdated.Raise();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            CountdownOver.Raise();
            StartCoroutine(GameOverTimer());
        }
    }

    public void CountdownMinute()
    {
        if (runningState)
        {
            if (GameProperties.CountdownMinutesLeft == 0)
            {
                GameProperties.CountdownHoursLeft--;

                if (GameProperties.CountdownHoursLeft == 0 && GameProperties.CountdownMinutesLeft == 0)
                {
                    runningState = false;
                    CountdownOver.Raise();
                    StartCoroutine(GameOverTimer());
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


    IEnumerator GameOverTimer()
    {
        yield return new WaitForSecondsRealtime(5);
        GameOver.Raise();
    }

}
