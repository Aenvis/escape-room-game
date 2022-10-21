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
        
        public void InvokeWithIntParam(int val)
        {
            foreach (var gameEventListener in m_listeners)
            {
                var listener = gameEventListener as GameEventListenerInt;
                UnityEngine.Debug.Log("Event Raised with params");
                listener.OnEventRaised(val);
            }
        }
        
        public void InvokeWithIntParam(string str)
        {
            foreach (var gameEventListener in m_listeners)
            {
                var listener = gameEventListener as GameEventListenerStr;
                UnityEngine.Debug.Log("Event Raised with params");
                listener.OnEventRaised(str);
            }
        }
    
        public void RegisterListener(GameEventListener listener)=> m_listeners.Add(listener);
        
        public void UnregisterListener(GameEventListener listener)=> m_listeners.Remove(listener);
        
    }
}
