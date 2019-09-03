using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public GameEvent OnHour;
    public GameEvent OnMinute;
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
            yield return new WaitForSecondsRealtime(OnMinuteDuration);
        }
    }

}
