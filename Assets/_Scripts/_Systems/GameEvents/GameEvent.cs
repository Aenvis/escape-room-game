using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Project.Systems.GameEvents
{
    [CreateAssetMenu(fileName = "New Game Event", menuName = "Game Event", order  = 1)]
    public class GameEvent : ScriptableObject
    {
        private HashSet<GameEventListener> m_listeners = new HashSet<GameEventListener>();
        
        public void Invoke([CanBeNull] object param=null)
        {
            foreach (var listener in m_listeners)
            {
                UnityEngine.Debug.Log("Event Raised with params");
                switch (param)
                {
                    case int:
                        listener.OnEventRaised((int)param!);
                        break;
                    case string:
                        listener.OnEventRaised((string)param!);
                        break;
                    default:
                        listener.OnEventRaised();
                        break;
                }
            }
        }

        public void RegisterListener(GameEventListener listener)=> m_listeners.Add(listener);
        
        public void UnregisterListener(GameEventListener listener)=> m_listeners.Remove(listener);
        
    }
}
