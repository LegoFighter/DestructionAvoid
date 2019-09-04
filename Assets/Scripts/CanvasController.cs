using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class CanvasController : MonoBehaviour
{
    [Header("Shared Data")]
    public GameProperties GameProperties;
    public RessourceGroups RessourceGroups;
    public AsteroidData AsteroidData;

    [Header("Groups")]
    public GameObject GroupUI;
    public TextMeshProUGUI GroupName;
    public TextMeshProUGUI UnemployedCitizen;
    public TextMeshProUGUI RawMaterial;
    public TextMeshProUGUI ProcessedMaterial;


    [Header("City Properties")]
    
    public GameObject CityPropertiesUI;
    public TextMeshProUGUI Cash;
    public TextMeshProUGUI Population;
    public TextMeshProUGUI StageOfRocketCompletion;
    public TextMeshProUGUI AmountOfStructures;

    [Header("Launch")]
    public GameObject LaunchUI;


    [Header("Countdown")]
    public TextMeshProUGUI Countdown;


    [Header("Won")]
    public GameObject WonUI;
    public TextMeshProUGUI WinStatus;
    public TextMeshProUGUI PlayTime;

    [Header("Information")]
    public GameObject InformationUI;
    public TextMeshProUGUI[] infoTextField;

    [Header("Asteroid")]
    public GameObject AsteroidUI;

    public float HideInformationDelay = 3;
    private Coroutine hideInformationCoroutin;


    public void ShowNewInfoMessage(string infoText)
    {
        if (hideInformationCoroutin != null)
            StopCoroutine(hideInformationCoroutin);


        InformationUI.SetActive(true);

        for (int i = infoTextField.Length - 1; i > 0; i--)
        {
            infoTextField[i].text = infoTextField[i - 1].text;
        }
        infoTextField[0].text = infoText;

        hideInformationCoroutin = StartCoroutine(HideInfoBox());
    }

    public void HideUI() {
        CityPropertiesUI.SetActive(false);
        AsteroidUI.SetActive(false);
        GroupUI.SetActive(false);
        LaunchUI.SetActive(false);
    }

    IEnumerator HideInfoBox()
    {
        yield return new WaitForSecondsRealtime(HideInformationDelay);
        InformationUI.SetActive(false);
        ClearInformationBox();
    }

    void ClearInformationBox()
    {
        for (int i = 0; i < infoTextField.Length; i++)
        {
            infoTextField[i].text = "";
        }
    }

    void Start()
    {
        GroupUI.SetActive(false);
        LaunchUI.SetActive(false);
        WonUI.SetActive(false);
        InformationUI.SetActive(false);
        ClearInformationBox();
    }

    public void ShowWinUI()
    {
        WinStatus.text = "You Won!";
        int secondsSinceStart = (int)Time.timeSinceLevelLoad;
        int hours = secondsSinceStart / 3600;
        int minutes = secondsSinceStart / 60;
        int seconds = secondsSinceStart % 60;

        PlayTime.text = "You beat the game in " + timeFormatter(hours) + ":" + timeFormatter(minutes) + ":" + timeFormatter(seconds) + ".";
        WonUI.SetActive(true);
    }

    private string timeFormatter(int unit)
    {
        var result = "";
        if (unit < 10)
        {
            result += "0" + unit;
        }
        else if (unit == 0)
        {
            result += "00";
        }
        else
        {
            result += unit;
        }
        return result;
    }

    public void ShowLooseUI()
    {
        WinStatus.text = "You Lost.";
        PlayTime.text = "Better luck next time!";
        WonUI.SetActive(true);
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

    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void UpdateCityPropertiesUI()
    {
        Cash.text = GameProperties.Cash + "$";
        Population.text = GameProperties.Population.ToString();
        StageOfRocketCompletion.text = GameProperties.StageOfRocketCompletion + " %";
        AmountOfStructures.text = GameProperties.AmountOfTiles + " tiles";
    }

    public void UpdateAsteroidProperties()
    {
        // AsteroidSpeed.text = AsteroidData.AsteroidSpeed + " km/h";
        // AsteroidDistanceFromEarth.text = AsteroidData.AsteroidSpeed + " km";
    }

    public void ShowLaunchUI()
    {
        LaunchUI.SetActive(true);
    }

    public void HideLaunchUI()
    {
        LaunchUI.SetActive(false);
    }

    public void ShowGroupUI()
    {
        if (!GroupUI.activeSelf)
        {
            GroupUI.SetActive(true);
        }
    }

    public void UpdateGroupUI()
    {
        GroupName.text = "Group " + GameProperties.ActiveRessourceGroup;
        UnemployedCitizen.text = RessourceGroups.UnemployedCitizen[GameProperties.ActiveRessourceGroup].ToString();
        RawMaterial.text = RessourceGroups.RawMaterials[GameProperties.ActiveRessourceGroup].ToString();
        ProcessedMaterial.text = RessourceGroups.ProcessedMaterials[GameProperties.ActiveRessourceGroup].ToString();

    }

    public void HideGroupUI()
    {
        GroupUI.SetActive(false);
    }

}
