using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileHandler : MonoBehaviour
{

    public GameProperties GameProperties;
    public GameObject[] Tiles;
    private GameObject selectedTile;
    private int lastSelectedType;

    [SerializeField]
    private Board board;
    public GameEvent UpdateUI;
    public Camera MainCamera;


    void Update()
    {
        if (Input.GetMouseButton(0) && Input.GetKey(KeyCode.LeftShift) && selectedTile != null)
        {
            InteractWithBoard(0);
        }
        else if (Input.GetMouseButtonDown(0) && selectedTile != null)
        {
            InteractWithBoard(0);
        }

        if (Input.GetMouseButton(1) && Input.GetKey(KeyCode.LeftShift))
        {
            InteractWithBoard(1);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            InteractWithBoard(1);
        }

        if (Input.GetMouseButton(2))
        {
            selectedTile = null;
        }
    }

    void InteractWithBoard(int action)
    {
        Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            GameObject clickedTile = hit.transform.gameObject;

            if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject() && clickedTile.GetComponent<Tile>() != null)
            {
                if (action == 0 && board.CheckIfTileEmpty(clickedTile))
                {
                    // if (GameProperties.Cash >= selectedTile.Cost)
                    // {
                    //     GameProperties.Cash -= selectedTile.Cost;

                    GameProperties.AmountOfStructures[lastSelectedType]++;
                    board.AddTile(selectedTile, clickedTile);
                }
                else if (action == 1 && !board.CheckIfTileEmpty(clickedTile))
                {
                    //GameProperties.Cash += clickedTile.Cost / 2;
                    GameProperties.AmountOfStructures[lastSelectedType]--;
                    board.RemoveTile(clickedTile);
                }
            }

        }
    }



    public void EnableBuilder(int index)
    {
        selectedTile = Tiles[index];
        lastSelectedType = index + 1;
    }
}
