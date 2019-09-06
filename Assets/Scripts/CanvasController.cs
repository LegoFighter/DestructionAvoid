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
    public TextMeshProUGUI[] FieldsGroup;


    [Header("City Properties")]

    public GameObject CityPropertiesUI;
    public TextMeshProUGUI Cash;
    // public TextMeshProUGUI Population;
    // public TextMeshProUGUI AmountOfStructures;

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

    [Header("Tile Details")]
    public GameObject TileUI;
    public TextMeshProUGUI[] GeneralInfo;
    public TextMeshProUGUI[] Field0;
    public TextMeshProUGUI[] Field1;
    public TextMeshProUGUI[] Field2;
    public TextMeshProUGUI[] Field3;
    public TextMeshProUGUI[] Field4;
    public TextMeshProUGUI[] Field5;
    public TextMeshProUGUI[] Field6;

    [Header("Help")]
    public GameObject HelpUI;

    public void HideHelpUI()
    {
        HelpUI.SetActive(false);
    }

    public void ShowHelpUI()
    {
        HelpUI.SetActive(true);
    }

    public void HideTileUI()
    {
        TileUI.SetActive(false);
    }

    public void ShowTileUI()
    {
        TileUI.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OnTileDetailUpdate()
    {
        Tile tile = GameProperties.SelectedTile;

        if (tile != null)
        {
            GeneralInfo[0].text = tile.TileName;
            GeneralInfo[1].text = tile.RessourceGroupId.ToString();
            GeneralInfo[2].text = tile.Cost + " $";
            GeneralInfo[3].text = tile.MaximalProductionCount.ToString();
            GeneralInfo[4].text = (int)(tile.TileEfficiency * 100) + " %";

            for (int i = 0; i < tile.LocalRessources.Length; i++)
            {
                Field0[i].text = tile.LocalRessources[i].ToString();
            }
            for (int i = 0; i < tile.AmountRessourcesMax.Length; i++)
            {
                Field1[i].text = tile.AmountRessourcesMax[i].ToString();
            }
            for (int i = 0; i < tile.RunningCosts.Length; i++)
            {
                Field2[i].text = tile.RunningCosts[i] + " $";
            }
            for (int i = 0; i < tile.WearPerHour.Length; i++)
            {
                Field3[i].text = tile.WearPerHour[i].ToString();
            }
            for (int i = 0; i < tile.MaximalHarvestRates.Length; i++)
            {
                Field4[i].text = tile.MaximalHarvestRates[i].ToString();
            }
            for (int i = 0; i < tile.MaterialCost.Length; i++)
            {
                Field5[i].text = tile.MaterialCost[i].ToString();
            }
            for (int i = 0; i < tile.ProductionOutput.Length; i++)
            {
                Field6[i].text = tile.ProductionOutput[i].ToString();
            }
        }
    }

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

    public void HideUI()
    {
        CityPropertiesUI.SetActive(false);
        AsteroidUI.SetActive(false);
        GroupUI.SetActive(false);
        LaunchUI.SetActive(false);
        HideTileUI();
        HideHelpUI();
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
        HideTileUI();
        HideHelpUI();
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
        //Population.text = countPopulation().ToString();
        // AmountOfStructures.text = GameProperties.AmountOfTiles + " tiles";
    }

    // private int countPopulation()
    // {
    //     int populationTemp = 0;

    //     for (int i = 0; i < RessourceGroups.UnemployedCitizen.Count; i++)
    //     {

    //     }
    //     return populationTemp;
    // }

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

        FieldsGroup[0].text = RessourceGroups.RawMaterials[GameProperties.ActiveRessourceGroup].ToString();
        FieldsGroup[1].text = RessourceGroups.ProcessedMaterials[GameProperties.ActiveRessourceGroup].ToString();
        FieldsGroup[2].text = RessourceGroups.UnemployedCitizen[GameProperties.ActiveRessourceGroup].ToString();
        FieldsGroup[3].text = RessourceGroups.Students[GameProperties.ActiveRessourceGroup].ToString();
        FieldsGroup[4].text = RessourceGroups.Professors[GameProperties.ActiveRessourceGroup].ToString();
        FieldsGroup[5].text = RessourceGroups.Workers[GameProperties.ActiveRessourceGroup].ToString();
        FieldsGroup[6].text = "Ressource Group " + GameProperties.ActiveRessourceGroup;
    }

    public void HideGroupUI()
    {
        GroupUI.SetActive(false);
    }

}
