using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public GameEvent OnHour;
    public GameEvent OnMinute;
    public GameEvent OnQuarter;
    [SerializeField]
    private float OnMinuteDuration = 5;
    [SerializeField]
    private int currentMinute;
    [SerializeField]
    private int currentHour;

    public bool clockActive = true;

    void Start()
    {
        StartCoroutine(MinuteTick());
    }

    IEnumerator MinuteTick()
    {
        while (clockActive)
        {
            OnMinute.Raise();
            if (currentMinute == 59)
            {
                currentMinute = 0;
                currentHour++;
                OnHour.Raise();
            }
            else
            {
                currentMinute++;
            }

            if (currentMinute == 0 || currentMinute == 15 || currentMinute == 30 || currentMinute == 45)
            {
                OnQuarter.Raise();
            }


            yield return new WaitForSecondsRealtime(OnMinuteDuration);
        }
    }

}
