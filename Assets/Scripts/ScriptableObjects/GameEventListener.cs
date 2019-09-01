using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    
    public GameEvent GameEvent;
    public UnityEvent Response;

    private void OnEnable() // 4
    {
        GameEvent.RegisterListener(this);
    }

    private void OnDisable() // 5
    {
        GameEvent.UnregisterListener(this);
    }

    public void OnEventRaised() // 6
    {
        Response.Invoke();
    }
}
