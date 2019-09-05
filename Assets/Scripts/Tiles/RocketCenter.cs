using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketCenter : MonoBehaviour
{
    public Rocket RocketPrefab;
    public Transform RocketSpawnPoint;
    public GameProperties GameProperties;

    public AsteroidData AsteroidData;
    public bool RocketIsLaunchable = true;

    public GameEvent RocketLaunched;

    public int RocketDamage;

    public int RocketSpeed;

    public int SelfDestructDelay;

    public float SuccessPropabiltiy;

    private Tile baseTile;

    void Start()
    {
        baseTile = GetComponent<Tile>();
    }


    public void LaunchRockets()
    {
        CalculateSuccessPropability();
        if (RocketIsLaunchable && AsteroidData.Asteroid != null)
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

    private void CalculateSuccessPropability()
    {
        SuccessPropabiltiy = (baseTile.LocalRessources[4]/baseTile.AmountRessourcesMax[4]) - ((baseTile.LocalRessources[4]/baseTile.AmountRessourcesMax[4])/5); 
    }

}
