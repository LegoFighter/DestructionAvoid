using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RessourceHandler : MonoBehaviour
{
    public Dictionary<int, List<Tile>> TileGroups;
    public Dictionary<int, int> AmountOfLocalCitizen;
    public Dictionary<int, int> AmountOfStudents;
    public Dictionary<int, int> AmountOfProfessors;
    public Dictionary<int, int> AmountOfWorkers;
    public Dictionary<int, int> AmountOfUnemployedCitizens;

    private int ressourceGroupsAmount = 10;

    public GameEvent GroupsUpdated;

    public void Start()
    {
        TileGroups = new Dictionary<int, List<Tile>>();
        AmountOfLocalCitizen = new Dictionary<int, int>();
        AmountOfStudents = new Dictionary<int, int>();
        AmountOfProfessors = new Dictionary<int, int>();
        AmountOfWorkers = new Dictionary<int, int>();
        AmountOfUnemployedCitizens = new Dictionary<int, int>();

        ressourceGroupsAmount = 9;

        for (int i = 0; i < ressourceGroupsAmount; i++)
        {
            TileGroups.Add(i, new List<Tile>());
            InitilizeGroup(i);
        }

    }

    public void Add(int destinationGroup, Tile tileToAdd)
    {
        TileGroups[destinationGroup].Add(tileToAdd);
        tileToAdd.RessourceGroupId = destinationGroup;
        AddRessources(destinationGroup, tileToAdd);
        GroupsUpdated.Raise();
    }

    private void InitilizeGroup(int i)
    {
        AmountOfLocalCitizen.Add(i, 0);
        AmountOfStudents.Add(i, 0);
        AmountOfProfessors.Add(i, 0);
        AmountOfWorkers.Add(i, 0);
        AmountOfUnemployedCitizens.Add(i, 0);
    }

    public void Remove(Tile tileToRemove)
    {
        TileGroups[tileToRemove.RessourceGroupId].Remove(tileToRemove);
        RemoveRessources(tileToRemove.RessourceGroupId, tileToRemove);
        GroupsUpdated.Raise();
    }

    public void Transfer(int destinationGroup, Tile tileToAdd)
    {
        Remove(tileToAdd);
        Add(destinationGroup, tileToAdd);
        GroupsUpdated.Raise();
    }

    private void RemoveRessources(int targetGroup, Tile tile)
    {
        AmountOfLocalCitizen[targetGroup] -= tile.AmountOfLocalCitizen;
        AmountOfStudents[targetGroup] -= tile.AmountOfStudents;
        AmountOfProfessors[targetGroup] -= tile.AmountOfProfessors;
        AmountOfWorkers[targetGroup] -= tile.AmountOfWorkers;
        AmountOfUnemployedCitizens[targetGroup] -= tile.AmountOfUnemployedCitizens;
    }

    private void AddRessources(int targetGroup, Tile tile)
    {
        AmountOfLocalCitizen[targetGroup] += tile.AmountOfLocalCitizen;
        AmountOfStudents[targetGroup] += tile.AmountOfStudents;
        AmountOfProfessors[targetGroup] += tile.AmountOfProfessors;
        AmountOfWorkers[targetGroup] += tile.AmountOfWorkers;
        AmountOfUnemployedCitizens[targetGroup] += tile.AmountOfUnemployedCitizens;
    }

    public void MergeGroup(int fromA, int toB)
    {
        foreach (var groupMember in TileGroups[fromA])
        {
            Transfer(toB, groupMember);
        }
    }

}
