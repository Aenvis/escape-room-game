using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Game Event", menuName = "Game Event", order  = 1)]
public class GameEvent : ScriptableObject
{
    private HashSet<GameEventListener> m_listeners = new HashSet<GameEventListener>();

    public void Invoke()
    {
        foreach (var listener in m_listeners)
        {
            Debug.Log("Event Raised");
            listener.OnEventRaised();
        }
    }
    
    public void RegisterListener(GameEventListener listener)
    {
        m_listeners.Add(listener);
    }
    
    public void UnregisterListener(GameEventListener listener)
    {
        m_listeners.Remove(listener);
    }
}
