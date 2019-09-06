using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScienceCenter : MonoBehaviour
{


    private Tile baseTile;
    private bool bonusApplied;

    void Start()
    {
        baseTile = GetComponent<Tile>();
        bonusApplied = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("NuclearTestArea") && !bonusApplied)
        {
            NuclearTestArea NTA = other.gameObject.GetComponent<NuclearTestArea>();
            baseTile.ProductionOutput[3] += baseTile.ProductionOutput[3] * NTA.bonusFactor;
            baseTile.ProductionOutput[4] += baseTile.ProductionOutput[4] * NTA.bonusFactor;
            baseTile.BonusApplied.Raise();
            baseTile.TileInfoUpdate.Raise();
            bonusApplied = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("NuclearTestArea") && bonusApplied)
        {
            NuclearTestArea NTA = other.gameObject.GetComponent<NuclearTestArea>();
            baseTile.ProductionOutput[3] -= baseTile.ProductionOutput[3] * NTA.bonusFactor;
            baseTile.ProductionOutput[4] -= baseTile.ProductionOutput[4] * NTA.bonusFactor;
            baseTile.BonusApplied.Raise();
            baseTile.TileInfoUpdate.Raise();
            bonusApplied = false;
        }
    }

}
