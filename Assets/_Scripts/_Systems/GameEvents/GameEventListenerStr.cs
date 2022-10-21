using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

namespace Project.Systems.GameEvents
{
    [System.Serializable]
    public class UnityEventStr : UnityEvent<string>
    {
    }
    public class GameEventListenerStr : GameEventListener
    {
        [SerializeField] [CanBeNull] private UnityEventStr response;

        public void OnEventRaised(string str)=> response?.Invoke(str);
        
    }
}