using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CanvasController : MonoBehaviour
{
    [Header("Shared Data")]
    public GameProperties GameProperties;
    public RessourceGroups RessourceGroups;

    [Header("Groups")]
    public GameObject GroupWindow;
    public TextMeshProUGUI groupName;
    public TextMeshProUGUI localCitizen;
    public TextMeshProUGUI students;
    public TextMeshProUGUI professors;
    public TextMeshProUGUI workers;
    public TextMeshProUGUI unemployed;


    [Header("City Properties")]
    [SerializeField]
    public TextMeshProUGUI Cash;
    public TextMeshProUGUI Population;
    public TextMeshProUGUI AsteroidSpeed;
    public TextMeshProUGUI StageOfRocketCompletion;
    public TextMeshProUGUI AmountOfStructures;


    [Header("Countdown")]
    [SerializeField]
    public TextMeshProUGUI Countdown;

    void Start()
    {
        GroupWindow.SetActive(false);
    }

    public void UpdateCountdown()
    {
        if (GameProperties.CountdownMinutesLeft == 0)
        {
            Countdown.text = GameProperties.CountdownHoursLeft + ":00";
        }
        else if (GameProperties.CountdownMinutesLeft < 10)
        {
            Countdown.text = GameProperties.CountdownHoursLeft + ":0" + GameProperties.CountdownMinutesLeft;
        }
        else
        {
            Countdown.text = GameProperties.CountdownHoursLeft + ":" + GameProperties.CountdownMinutesLeft;
        }

    }

    public void UpdateCityPropertiesUI()
    {
        Cash.text = GameProperties.Cash + "$";
        Population.text = GameProperties.Population.ToString();
        AsteroidSpeed.text = GameProperties.AsteroidSpeed + " km/h";
        StageOfRocketCompletion.text = GameProperties.StageOfRocketCompletion + " %";
        AmountOfStructures.text = GameProperties.AmountOfTiles + " tiles";
    }


    public void ShowGroupUI()
    {
        if (!GroupWindow.activeSelf)
        {
            GroupWindow.SetActive(true);
        }
    }

    public void UpdateGroupUI()
    {
        groupName.text = "Group " + GameProperties.ActiveRessourceGroup;
        localCitizen.text = RessourceGroups.AmountOfLocalCitizen[GameProperties.ActiveRessourceGroup].ToString();
        students.text = RessourceGroups.AmountOfStudents[GameProperties.ActiveRessourceGroup].ToString();
        professors.text = RessourceGroups.AmountOfProfessors[GameProperties.ActiveRessourceGroup].ToString();
        workers.text = RessourceGroups.AmountOfWorkers[GameProperties.ActiveRessourceGroup].ToString();
        unemployed.text = RessourceGroups.AmountOfUnemployedCitizens[GameProperties.ActiveRessourceGroup].ToString();
    }

    public void HideGroupUI()
    {
        GroupWindow.SetActive(false);
    }

}
