using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public int Id;
    public int sharedRessourcesId;

}

public enum Tyletype {

    CITY_SMALL,
    CITY_MIDDLE,
    CITY_LARGE,
    EXPLOSIVE_TEST_CENTER,
    SCIENCE_CAMPUS,
    STREET,
    ROCKET_CENTER,
    FACTORY,

}