using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public ITileType tileType;
    public int id;
    public SharedRessources sharedRessources;

    public void SetSharedRessource(SharedRessources sharedRessources)
    {
        this.sharedRessources = sharedRessources;
    }
}
