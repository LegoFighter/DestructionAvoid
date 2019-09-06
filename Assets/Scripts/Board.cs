﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{

    public GameObject[] Tiles;
    public GameObject emptyTile;
    public GameProperties GameProperties;

    public bool isBuildMode;


    void Start()
    {
        VisibiltyEmptyTile(false);
    }

    public void VisibiltyEmptyTile(bool boolean)
    {
        Tile tempTile;
        for (int i = 0; i < Tiles.Length; i++)
        {
            tempTile = Tiles[i].GetComponent<Tile>();

            if (tempTile.Type == 0)
            {
                Tiles[i].SetActive(boolean);
            }
        }
    }

    public void AddTile(GameObject tileToAdd, GameObject tileToReplace)
    {
        int indexToReplace = 0;

        for (int i = 0; i < Tiles.Length; i++)
        {
            if (Tiles[i] == tileToReplace)
            {
                indexToReplace = i;
                break;
            }
        }

        GameObject tileInstance = Instantiate(tileToAdd, tileToReplace.transform.position, Quaternion.identity, gameObject.transform);
        Tiles[indexToReplace] = tileInstance;
        Tile tileRef = tileInstance.GetComponent<Tile>();

        if (!isBuildMode && tileRef.Type == 0)
        {
            tileInstance.SetActive(false);
        }

        if (GameProperties.TilesToDelete.Contains(tileToReplace))
        {
            GameProperties.TilesToDelete.Remove(tileToReplace);
        }

        Destroy(tileToReplace);
    }

    public void RemoveTile(GameObject tileAtPosition)
    {
        AddTile(emptyTile, tileAtPosition);
    }

    public bool CheckIfTileEmpty(GameObject tileAtPosition)
    {
        return tileAtPosition.GetComponent<Tile>().Type == 0;
    }

    public void DeleteTilesToDelete()
    {
        for (int i = GameProperties.TilesToDelete.Count - 1; i >= 0; i--)
        {
            RemoveTile(GameProperties.TilesToDelete[i]);
        }
    }

}
