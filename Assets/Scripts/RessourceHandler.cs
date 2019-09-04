using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RessourceHandler : MonoBehaviour
{
    public Dictionary<int, List<Tile>> TileGroups;

    public RessourceGroups RessourceGroups;

    public GameProperties GameProperties;
    private int ressourceGroupsAmount = 10;

    public GameEvent GroupsUpdate;

    public void Start()
    {
        TileGroups = new Dictionary<int, List<Tile>>();
        RessourceGroups.AmountOfLocalCitizen = new Dictionary<int, int>();
        RessourceGroups.AmountOfStudents = new Dictionary<int, int>();
        RessourceGroups.AmountOfProfessors = new Dictionary<int, int>();
        RessourceGroups.AmountOfWorkers = new Dictionary<int, int>();
        RessourceGroups.AmountOfUnemployedCitizens = new Dictionary<int, int>();

        ressourceGroupsAmount = 10;

        for (int i = 0; i < ressourceGroupsAmount; i++)
        {
            TileGroups.Add(i, new List<Tile>());
            InitilizeGroup(i);
        }

    }

    public void Add(Tile tileToAdd)
    {
        tileToAdd.RessourceGroupId = GameProperties.ActiveRessourceGroup;
        
        TileGroups[tileToAdd.RessourceGroupId].Add(tileToAdd);
        AddRessources(tileToAdd.RessourceGroupId, tileToAdd);
        GroupsUpdate.Raise();
    }

    private void InitilizeGroup(int i)
    {
        RessourceGroups.AmountOfLocalCitizen.Add(i, 0);
        RessourceGroups.AmountOfStudents.Add(i, 0);
        RessourceGroups.AmountOfProfessors.Add(i, 0);
        RessourceGroups.AmountOfWorkers.Add(i, 0);
        RessourceGroups.AmountOfUnemployedCitizens.Add(i, 0);
    }

    public void Remove(Tile tileToRemove)
    {
        TileGroups[tileToRemove.RessourceGroupId].Remove(tileToRemove);
        RemoveRessources(tileToRemove.RessourceGroupId, tileToRemove);
        GroupsUpdate.Raise();
    }

    public void Transfer(Tile tileToAdd)
    {
        Remove(tileToAdd);
        Add(tileToAdd);
        GroupsUpdate.Raise();
    }

    private void RemoveRessources(int targetGroup, Tile tile)
    {
        RessourceGroups.AmountOfLocalCitizen[targetGroup] -= tile.AmountOfLocalCitizen;
        RessourceGroups.AmountOfStudents[targetGroup] -= tile.AmountOfStudents;
        RessourceGroups.AmountOfProfessors[targetGroup] -= tile.AmountOfProfessors;
        RessourceGroups.AmountOfWorkers[targetGroup] -= tile.AmountOfWorkers;
        RessourceGroups.AmountOfUnemployedCitizens[targetGroup] -= tile.AmountOfUnemployedCitizens;
    }

    private void AddRessources(int targetGroup, Tile tile)
    {
        RessourceGroups.AmountOfLocalCitizen[targetGroup] += tile.AmountOfLocalCitizen;
        RessourceGroups.AmountOfStudents[targetGroup] += tile.AmountOfStudents;
        RessourceGroups.AmountOfProfessors[targetGroup] += tile.AmountOfProfessors;
        RessourceGroups.AmountOfWorkers[targetGroup] += tile.AmountOfWorkers;
        RessourceGroups.AmountOfUnemployedCitizens[targetGroup] += tile.AmountOfUnemployedCitizens;
    }

    public void MergeGroup(int fromA)
    {
        foreach (var groupMember in TileGroups[fromA])
        {
            Transfer(groupMember);
        }
    }

    public void OnGroupsUpdate()
    {


    }

}
