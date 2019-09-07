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

    public GameEvent SuccessRate;

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
        float prob_0 = (float)baseTile.LocalRessources[0] / (float)baseTile.AmountRessourcesMax[0];
        float prob_1 = (float)baseTile.LocalRessources[1] / (float)baseTile.AmountRessourcesMax[1];
        float prob_2 = (float)baseTile.LocalRessources[3] / (float)baseTile.AmountRessourcesMax[3];
        float prob_3 = (float)baseTile.LocalRessources[4] / (float)baseTile.AmountRessourcesMax[4];
        float prob_4 = (float)baseTile.LocalRessources[5] / (float)baseTile.AmountRessourcesMax[5];

        float all_prob = prob_0 + prob_1 + prob_2 + prob_3 + prob_4;
        SuccessPropabiltiy = all_prob / 5;
        GameProperties.RocketSuccessRate = (int)(SuccessPropabiltiy*100);
        SuccessRate.Raise();
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
