using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileHandler : MonoBehaviour
{

    public GameProperties GameProperties;
    public GameObject[] Tiles;
    private GameObject selectedGameObejct;
    private int lastSelectedType;
    public RessourceHandler RessourceHandler;
    public int selectedRessourceGroup;

    [SerializeField]
    public Board board;
    public Camera MainCamera;

    [Header("Game Events")]
    public GameEvent ShowGroupUI;
    public GameEvent HideGroupUI;
    public GameEvent UpdateGroupUI;
    public GameEvent CityPropertiesUpdated;
    public GameEvent ShowLaunchUI;
    public GameEvent HideLaunchUI;

    void Update()
    {
        if (Input.GetMouseButton(0) && Input.GetKey(KeyCode.LeftShift) && selectedGameObejct != null)
        {
            ManageTiles(0);
        }
        else if (Input.GetMouseButtonDown(0) && selectedGameObejct != null)
        {
            ManageTiles(0);
        }

        if (Input.GetMouseButton(0) && selectedGameObejct == null)
        {
            ShowTileInfo(0);
        }

        if (Input.GetMouseButton(1) && Input.GetKey(KeyCode.LeftShift))
        {
            ManageTiles(1);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            ManageTiles(1);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            selectedGameObejct = null;
            board.VisibiltyEmptyTile(false);
            board.isBuildMode = false;
            HideUIExtensions();
        }


        if (Input.GetKey(KeyCode.Alpha0))
        {
            selectedRessourceGroup = 0;
        }
        else if (Input.GetKey(KeyCode.Alpha1))
        {
            selectedRessourceGroup = 1;
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            selectedRessourceGroup = 2;
        }
        else if (Input.GetKey(KeyCode.Alpha3))
        {
            selectedRessourceGroup = 3;
        }
        else if (Input.GetKey(KeyCode.Alpha4))
        {
            selectedRessourceGroup = 4;
        }
        else if (Input.GetKey(KeyCode.Alpha5))
        {
            selectedRessourceGroup = 5;
        }
        else if (Input.GetKey(KeyCode.Alpha6))
        {
            selectedRessourceGroup = 6;
        }
        else if (Input.GetKey(KeyCode.Alpha7))
        {
            selectedRessourceGroup = 7;
        }
        else if (Input.GetKey(KeyCode.Alpha8))
        {
            selectedRessourceGroup = 8;
        }
        else if (Input.GetKey(KeyCode.Alpha9))
        {
            selectedRessourceGroup = 9;
        }
    }

    void ManageTiles(int action)
    {
        Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            GameObject clickedGameObject = hit.transform.gameObject;
            Tile clickedTile = clickedGameObject.GetComponent<Tile>();

            Tile selectedTile = null;

            if (selectedGameObejct != null)
                selectedTile = selectedGameObejct.GetComponent<Tile>();


            if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject() && clickedTile != null)
            {
                if (action == 0 && board.CheckIfTileEmpty(clickedGameObject))
                {
                    // if (GameProperties.Cash >= selectedTile.Cost)
                    // {
                    //     GameProperties.Cash -= selectedTile.Cost;

                    GameProperties.AmountOfTiles++;
                    GameProperties.Population += selectedTile.AmountOfLocalCitizen;

                    RessourceHandler.Add(selectedRessourceGroup, board.AddTile(selectedGameObejct, clickedGameObject));

                    HideGroupUI.Raise();
                    CityPropertiesUpdated.Raise();

                    SwitchLaunchUI(selectedTile);
                }
                else if (action == 0 && !board.CheckIfTileEmpty(clickedGameObject))
                {
                    SwitchLaunchUI(clickedTile);
                    GameProperties.ActiveRessourceGroup = clickedTile.RessourceGroupId;
                    UpdateGroupUI.Raise();
                    ShowGroupUI.Raise();
                    CityPropertiesUpdated.Raise();
                }
                else if (action == 1 && !board.CheckIfTileEmpty(clickedGameObject))
                {
                    //GameProperties.Cash += clickedTile.Cost / 2;
                    GameProperties.Population -= clickedTile.AmountOfLocalCitizen;
                    GameProperties.AmountOfTiles--;

                    RessourceHandler.Remove(board.RemoveTile(clickedGameObject));
                    HideLaunchUI.Raise();
                    UpdateGroupUI.Raise();
                    CityPropertiesUpdated.Raise();
                }
            }
        }
    }

    void ShowTileInfo(int action)
    {
        Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            GameObject clickedGameObject = hit.transform.gameObject;
            Tile clickedTile = clickedGameObject.GetComponent<Tile>();

            if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject() && clickedTile != null)
            {
                if (action == 0 && board.CheckIfTileEmpty(clickedGameObject))
                {
                    HideUIExtensions();
                }
                else if (action == 0 && !board.CheckIfTileEmpty(clickedGameObject))
                {
                    SwitchLaunchUI(clickedTile);

                    GameProperties.ActiveRessourceGroup = clickedTile.RessourceGroupId;
                    UpdateGroupUI.Raise();
                    ShowGroupUI.Raise();
                }
            }
        }
    }

    
    void SwitchLaunchUI(Tile tile)
    {
        if (tile.Type == 7)
        {
            ShowLaunchUI.Raise();
        }
        else
        {
            HideLaunchUI.Raise();
        }
    }

    void HideUIExtensions()
    {
        HideGroupUI.Raise();
        HideLaunchUI.Raise();
    }

    public void EnableBuilder(int index)
    {
        selectedGameObejct = Tiles[index];
        lastSelectedType = index + 1;
        board.VisibiltyEmptyTile(true);
        board.isBuildMode = true;
    }
}
