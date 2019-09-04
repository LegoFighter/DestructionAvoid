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


    public void LaunchRockets()
    {
        if (RocketIsLaunchable && AsteroidData.Asteroid != null)
        {
            Rocket Rocket = Instantiate(RocketPrefab, RocketSpawnPoint.transform.position, RocketSpawnPoint.transform.rotation, gameObject.transform);

            //BOTH PROPERTIES ARE SET BY A POSSIBILITY
            Rocket.Target = AsteroidData.Asteroid;
            Rocket.Damage = 50;
            Rocket.Speed = 200;
            Rocket.SelfDestructDelay = 3;
            RocketLaunched.Raise();
        }
    }

}
