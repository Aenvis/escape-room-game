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
        
        //we can dynamically pass porameters to UnityEvent and use this Invoke method instead of the "pure" one -- useful in e.g. debug console
        public void InvokeWithIntParam(int val)
        {
            foreach (var listener in m_listeners)
            {
                UnityEngine.Debug.Log("Event Raised with params");
                listener.OnEventRaisedWithParam(val);
            }
        }
        
        public void InvokeWithIntParam(string str)
        {
            foreach (var listener in m_listeners)
            {
                UnityEngine.Debug.Log("Event Raised with params");
                listener.OnEventRaisedWithParam(str);
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
}
