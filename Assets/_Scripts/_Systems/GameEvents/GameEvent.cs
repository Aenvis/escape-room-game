using System.Collections.Generic;
using UnityEngine;

namespace Project.Systems.GameEvents
{
    [CreateAssetMenu(fileName = "New Game Event", menuName = "Game Event", order  = 1)]
    public class GameEvent : ScriptableObject
    {
        private HashSet<GameEventListener> m_listeners = new HashSet<GameEventListener>();

        public void Invoke()
        {
            foreach (var listener in m_listeners)
            {
                UnityEngine.Debug.Log("Event Raised");
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
    
    public class GameEvent<T> : ScriptableObject
    {
        private HashSet<GameEventListener<T>> m_listeners = new HashSet<GameEventListener<T>>();

        public void Invoke(T param)
        {
            foreach (var listener in m_listeners)
            {
                UnityEngine.Debug.Log("Event Raised");
                listener.OnEventRaised(param);
            }
        }
    
        public void RegisterListener(GameEventListener<T> listener)
        {
            m_listeners.Add(listener);
        }
    
        public void UnregisterListener(GameEventListener<T> listener)
        {
            m_listeners.Remove(listener);
        }
    }
}
