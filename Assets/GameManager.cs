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


    [SerializeField]
    private bool runningState;

    public void Start()
    {
        runningState = true;

        GameProperties.Cash = 1000000;
        GameProperties.AverageEducationLevel = 0;
        GameProperties.Population = 0;
        GameProperties.AmountOfTiles = 0;
        GameProperties.CountdownHoursLeft = 24;
        GameProperties.CountdownMinutesLeft = 0;
        GameProperties.ActiveRessourceGroup = 0;

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

    public void OnGameOverEvent() {
        runningState = false;
    }


    IEnumerator GameOverTimer()
    {
        yield return new WaitForSecondsRealtime(5);
        GameOver.Raise();
    }

}
