using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameProperties GameProperties;
    public AsteroidData AsteroidData;
    public RessourceGroups RessourceGroups;
    private int ressourceGroupsAmount = 10;
    public GameObject Asteroid;

    [Header("Game Events")]
    public GameEvent CityPropertiesUpdated;
    public GameEvent CountdownUpdateUI;
    public GameEvent CountdownOver;
    public GameEvent GameOver;
    public GameEvent StartGame;


    [SerializeField]
    private bool runningState;


    [Header("Game")]
    public int StartMoney;
    public int CountdownStartHour;
    public int CountdownStartMinute;


    public void Start()
    {
        runningState = true;

        GameProperties.Cash = StartMoney;
        // GameProperties.AverageEducationLevel = 0;
        // GameProperties.Population = 0;
        // GameProperties.AmountOfTiles = 0;
        GameProperties.CountdownHoursLeft = CountdownStartHour;
        GameProperties.CountdownMinutesLeft = CountdownStartMinute;
        GameProperties.ActiveRessourceGroup = 0;
        GameProperties.TilesToDelete = new List<GameObject>();
        GameProperties.RocketSuccessRate = 0;

        // GameProperties.TilesToDelete.Clear();

        AsteroidData.Asteroid = Asteroid;
        AsteroidData.AsteroidSpeed = 50000;
        AsteroidData.DistanceToFromEarth = 1000000;

        RessourceGroups.UnemployedCitizen = new Dictionary<int, int>();
        RessourceGroups.RawMaterials = new Dictionary<int, int>();
        RessourceGroups.ProcessedMaterials = new Dictionary<int, int>();
        RessourceGroups.Students = new Dictionary<int, int>();
        RessourceGroups.Workers = new Dictionary<int, int>();
        RessourceGroups.Professors = new Dictionary<int, int>();

        ressourceGroupsAmount = 10;

        for (int i = 0; i < ressourceGroupsAmount; i++)
        {
            RessourceGroups.UnemployedCitizen.Add(i, 0);
            RessourceGroups.RawMaterials.Add(i, 0);
            RessourceGroups.ProcessedMaterials.Add(i, 0);
            RessourceGroups.Students.Add(i, 0);
            RessourceGroups.Workers.Add(i, 0);
            RessourceGroups.Professors.Add(i, 0);
        }

        CountdownUpdateUI.Raise();
        CityPropertiesUpdated.Raise();

        // Screen.SetResolution(1600, 900, true);
    }

    // void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.R))
    //     {
    //         CountdownOver.Raise();
    //         StartCoroutine(GameOverTimer());
    //     }
    // }

    public void CountdownMinute()
    {
        if (runningState)
        {
            if (GameProperties.CountdownHoursLeft == 23 && GameProperties.CountdownMinutesLeft == 59)
            {
                StartGame.Raise();
            }

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

    public void OnGameOverEvent()
    {
        runningState = false;
    }


    IEnumerator GameOverTimer()
    {
        yield return new WaitForSecondsRealtime(5);
        GameOver.Raise();
    }

}
