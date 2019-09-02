using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{

    public GameObject[] Tiles;
    public GameObject emptyTile;


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
        Destroy(Tiles[indexToReplace].gameObject);
        Tiles[indexToReplace] = Instantiate(tileToAdd, tileToReplace.transform.position, Quaternion.identity, gameObject.transform);

    }

    public void RemoveTile(GameObject tileAtPosition)
    {
        AddTile(emptyTile, tileAtPosition);
    }

    public bool CheckIfTileEmpty(GameObject tileAtPosition)
    {
        return tileAtPosition.GetComponent<Tile>().Type == 0;
    }

    // public Vector3 CalculateGridPosition(Vector3 position)
    // {
    //     return new Vector3(Mathf.Round(position.x), .5f, Mathf.Round(position.z));
    // }
}
