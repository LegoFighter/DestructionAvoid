using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CanvasController : MonoBehaviour
{

    public GameProperties GameProperties;

    [Header("Ressource Window")]
    public RessourceHandler RessourceHandler;
    [SerializeField]
    private GameObject GroupWindow;
    public TextMeshProUGUI groupName;
    public TextMeshProUGUI localCitizen;
    public TextMeshProUGUI students;
    public TextMeshProUGUI professors;
    public TextMeshProUGUI workers;
    public TextMeshProUGUI unemployed;

    void Start()
    {
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
        localCitizen.text = RessourceHandler.AmountOfLocalCitizen[GameProperties.ActiveRessourceGroup].ToString();
        students.text = RessourceHandler.AmountOfStudents[GameProperties.ActiveRessourceGroup].ToString();
        professors.text = RessourceHandler.AmountOfProfessors[GameProperties.ActiveRessourceGroup].ToString();
        workers.text = RessourceHandler.AmountOfWorkers[GameProperties.ActiveRessourceGroup].ToString();
        unemployed.text = RessourceHandler.AmountOfUnemployedCitizens[GameProperties.ActiveRessourceGroup].ToString();
    }

    public void HideGroupUI()
    {
        GroupWindow.SetActive(false);
    }

}
