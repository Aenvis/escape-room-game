using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

namespace Project.Systems.GameEvents
{
    [System.Serializable]
    public class UnityEventInt : UnityEvent<int>
    {
    }
    public class GameEventListenerInt : GameEventListener
    {
        [SerializeField] [CanBeNull] private UnityEventInt response;
        
        public void OnEventRaised(int val)=>response?.Invoke(val);
    }
}