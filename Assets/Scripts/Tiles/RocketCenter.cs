using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketCenter : MonoBehaviour
{
    public Rocket Rocket;
    public Transform RocketSpawnPoint;
    public GameProperties GameProperties;

    public AsteroidData AsteroidData;
    public bool RocketIsLaunchable = true;

    public void LaunchRockets()
    {
        if (RocketIsLaunchable && AsteroidData.Asteroid.gameObject.activeSelf)
        {
            Rocket rocket = Instantiate(Rocket, transform.position, Quaternion.identity, gameObject.transform);

            //BOTH PROPERTIES ARE SET BY A POSSIBILITY
            Rocket.Target = AsteroidData.Asteroid.transform;
            rocket.Damage = 50;
        }
    }

}
