using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketCenter : MonoBehaviour
{
    public Rocket RocketPrefab;
    public Transform RocketSpawnPoint;
    public GameProperties GameProperties;

    public AsteroidData AsteroidData;

    public GameEvent RocketLaunched;
    public GameEvent NotEnoughMoney;

    public int RocketCosts;

    public int RocketDamage;

    public int RocketSpeed;

    public int SelfDestructDelay;

    public float SuccessPropabiltiy;

    private Tile baseTile;

    private bool bonusApplied;

    void Start()
    {
        baseTile = GetComponent<Tile>();
        bonusApplied = false;
    }

    public void LaunchRockets()
    {
        if (GameProperties.Cash >= RocketCosts)
        {
            GameProperties.Cash -= RocketCosts;

            CalculateSuccessPropability();
            if (AsteroidData.Asteroid != null)
            {
                Rocket Rocket = Instantiate(RocketPrefab, RocketSpawnPoint.transform.position, RocketSpawnPoint.transform.rotation, gameObject.transform);

                int calculatedRocketDamage = (int)(RocketDamage * SuccessPropabiltiy);
                int calculatedRocketSpeed = (int)(RocketDamage * SuccessPropabiltiy);
                int calculatedRocketSelfDestruct = (int)(RocketDamage * SuccessPropabiltiy);
                Rocket.Target = AsteroidData.Asteroid;
                Rocket.Damage = UnityEngine.Random.Range(calculatedRocketDamage, RocketDamage);
                Rocket.Speed = UnityEngine.Random.Range(calculatedRocketSpeed, RocketDamage);
                Rocket.SelfDestructDelay = UnityEngine.Random.Range(calculatedRocketSelfDestruct, SelfDestructDelay);
                RocketLaunched.Raise();
            }
        }
        else
        {
            NotEnoughMoney.Raise();
        }


    }

    private void CalculateSuccessPropability()
    {
        SuccessPropabiltiy = (baseTile.LocalRessources[4] / baseTile.AmountRessourcesMax[4]) - ((baseTile.LocalRessources[4] / baseTile.AmountRessourcesMax[4]) / 5);
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
