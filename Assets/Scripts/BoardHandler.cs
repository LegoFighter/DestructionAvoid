using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardHandler : MonoBehaviour
{

    public GameProperties GameProperties;
    public GameObject[] Tiles;
    private GameObject selectedGameObejct;
    private int lastSelectedType;
    public RessourceHandler RessourceHandler;

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

    public GameEvent TileBuild;
    public GameEvent TileSold;

    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha0))
        {
            GameProperties.ActiveRessourceGroup = 0;
            UpdateGroupUI.Raise();
            ShowGroupUI.Raise();
        }
        else if (Input.GetKey(KeyCode.Alpha1))
        {
            GameProperties.ActiveRessourceGroup = 1;
            UpdateGroupUI.Raise();
            ShowGroupUI.Raise();
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {

            GameProperties.ActiveRessourceGroup = 2;
            UpdateGroupUI.Raise();
            ShowGroupUI.Raise();
        }
        else if (Input.GetKey(KeyCode.Alpha3))
        {
            GameProperties.ActiveRessourceGroup = 3;
            UpdateGroupUI.Raise();
            ShowGroupUI.Raise();
        }
        else if (Input.GetKey(KeyCode.Alpha4))
        {
            GameProperties.ActiveRessourceGroup = 4;
            UpdateGroupUI.Raise();
            ShowGroupUI.Raise();
        }
        else if (Input.GetKey(KeyCode.Alpha5))
        {
            GameProperties.ActiveRessourceGroup = 5;
            UpdateGroupUI.Raise();
            ShowGroupUI.Raise();
        }
        else if (Input.GetKey(KeyCode.Alpha6))
        {
            GameProperties.ActiveRessourceGroup = 6;
            UpdateGroupUI.Raise();
            ShowGroupUI.Raise();
        }
        else if (Input.GetKey(KeyCode.Alpha7))
        {
            GameProperties.ActiveRessourceGroup = 7;
            UpdateGroupUI.Raise();
            ShowGroupUI.Raise();
        }
        else if (Input.GetKey(KeyCode.Alpha8))
        {
            GameProperties.ActiveRessourceGroup = 8;
            UpdateGroupUI.Raise();
            ShowGroupUI.Raise();
        }
        else if (Input.GetKey(KeyCode.Alpha9))
        {
            GameProperties.ActiveRessourceGroup = 9;
            UpdateGroupUI.Raise();
            ShowGroupUI.Raise();
        }


        if (board.isBuildMode)
        {
            if (Input.GetMouseButton(0) && Input.GetKey(KeyCode.LeftShift) && selectedGameObejct != null)
            {
                InteractionBuildMode(0);
            }
            else if (Input.GetMouseButtonDown(0) && selectedGameObejct != null)
            {
                InteractionBuildMode(0);
            }
            else if (Input.GetMouseButton(0) && selectedGameObejct == null)
            {
                InteractionInpectMode(0);
            }


            if (Input.GetMouseButton(1) && Input.GetKey(KeyCode.LeftShift))
            {
                //Build Mode: Shift+Right Mouse Button -> Transfer tile to currently selected group;
                InteractionBuildMode(2);
            }
            else if (Input.GetMouseButtonDown(1))
            {
                //Right Mouse Button -> Sell Tile;
                InteractionBuildMode(1);
            }
        }

        if (!board.isBuildMode)
        {
            if (Input.GetMouseButton(0))
            {
                InteractionInpectMode(0);
            }

            if (Input.GetMouseButtonDown(1))
            {
                InteractionInpectMode(1);
            }
            else if (Input.GetMouseButton(1) && Input.GetKey(KeyCode.LeftShift))
            {
                InteractionInpectMode(1);
            }
        }

        if (Input.GetKey(KeyCode.Space))
        {
            selectedGameObejct = null;
            board.VisibiltyEmptyTile(false);
            board.isBuildMode = false;
            HideUIExtensions();
        }

    }

    void InteractionBuildMode(int action)
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
                    if (GameProperties.Cash >= selectedTile.Cost)
                    {
                        GameProperties.Cash -= selectedTile.Cost;
                        GameProperties.AmountOfTiles++;
                        board.AddTile(selectedGameObejct, clickedGameObject);
                        CityPropertiesUpdated.Raise();
                        UpdateGroupUI.Raise();
                        TileBuild.Raise();
                        SwitchLaunchUI(selectedTile);
                    }
                }
                else if (action == 0 && !board.CheckIfTileEmpty(clickedGameObject))
                {
                    SwitchLaunchUI(clickedTile);
                    GameProperties.ActiveRessourceGroup = clickedTile.RessourceGroupId;
                    UpdateGroupUI.Raise();
                    ShowGroupUI.Raise();
                    CityPropertiesUpdated.Raise();
                }
                else if (action == 1 && board.CheckIfTileEmpty(clickedGameObject))
                {
                    selectedGameObejct = null;
                    HideUIExtensions();
                }
                else if (action == 1 && !board.CheckIfTileEmpty(clickedGameObject))
                {
                    GameProperties.Cash += clickedTile.Cost / 2;
                    GameProperties.AmountOfTiles--;
                    board.RemoveTile(clickedGameObject);
                    HideLaunchUI.Raise();
                    UpdateGroupUI.Raise();
                    CityPropertiesUpdated.Raise();
                    TileSold.Raise();
                }
                else if (action == 2 && !board.CheckIfTileEmpty(clickedGameObject))
                {
                    clickedTile.RessourceGroupId = GameProperties.ActiveRessourceGroup;
                    HideLaunchUI.Raise();
                    UpdateGroupUI.Raise();
                    CityPropertiesUpdated.Raise();
                }
            }
        }
    }

    void InteractionInpectMode(int action)
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
                else if (action == 1 && board.CheckIfTileEmpty(clickedGameObject))
                {
                    HideUIExtensions();
                }
                else if (action == 1 && !board.CheckIfTileEmpty(clickedGameObject))
                {
                    SwitchLaunchUI(clickedTile);

                    clickedTile.RessourceGroupId = GameProperties.ActiveRessourceGroup;
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
