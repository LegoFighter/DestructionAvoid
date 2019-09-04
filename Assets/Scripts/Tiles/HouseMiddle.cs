using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseMiddle : MonoBehaviour
{

    public GameEvent CityPropertiesUpdated;
    public GameEvent UpdateGroups;

    [Header("Shared Data")]
    public GameProperties GameProperties;
    public RessourceGroups RessourceGroups;

    private Tile baseTile;

    [SerializeField]
    private float birthRate = .02f;
    void Start()
    {
        baseTile = GetComponent<Tile>();
    }




}
