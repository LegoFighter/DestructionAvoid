using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationScript : MonoBehaviour
{
    public GameObject Earth;
    public GameObject Asteroid;

    public float speedEarthRotation = 10f;
    public float speedAsteroidRotation = 10f;

    void Update()
    {
        Earth.transform.Rotate(Vector3.up, speedEarthRotation * Time.deltaTime);
        Asteroid.transform.Rotate(new Vector3(0.25f, .5f, 1), -speedAsteroidRotation * Time.deltaTime);
    }

}
