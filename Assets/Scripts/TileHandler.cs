using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileHandler : MonoBehaviour {

    public GameProperties GameProperties;
    [SerializeField]
    private Tile[] tiles;
    [SerializeField]
    private Board board;
    private Tile selectedTile;

    public GameEvent UpdateUI;
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(0) && Input.GetKey(KeyCode.LeftShift) && selectedTile != null)
        {
            InteractWithBoard(0);
        }
		else if (Input.GetMouseButtonDown(0) && selectedTile != null)
        {
            InteractWithBoard(0);
        }

        if (Input.GetMouseButtonDown(1))
        {
            InteractWithBoard(1);
        }
    }

    void InteractWithBoard(int action)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 gridPosition = board.CalculateGridPosition(hit.point);
            if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            {
                if (action == 0 && board.CheckForBuildingAtPosition(gridPosition) == null)
                {
                    if (GameProperties.Cash >= selectedTile.Cost)
                    {
                        GameProperties.Cash -= selectedTile.Cost;

                        GameProperties.AmountOfStructures[selectedTile.Id]++;
                        
                        //TODO:
                        //board.AddBuilding(selectedTile, gridPosition);
                    }
                }
                else if (action == 1 && board.CheckForBuildingAtPosition(gridPosition) != null)
                {                 
                    GameProperties.Cash += board.CheckForBuildingAtPosition(gridPosition).Cost/2;

                    //TODO:
                    //board.RemoveBuilding(gridPosition);
                }
            }
        }
    }

    public void EnableBuilder(int index)
    {
        selectedTile = tiles[index];
        Debug.Log("Selected building: " + selectedTile.Type);
    }
}
