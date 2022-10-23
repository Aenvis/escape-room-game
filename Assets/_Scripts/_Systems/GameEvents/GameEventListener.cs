using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

namespace Project.Systems.GameEvents
{
    public class GameEventListener : MonoBehaviour
    {
        [SerializeField] protected GameEvent gameEvent;
        [SerializeField] [CanBeNull] private UnityEvent voidResponse;
        [SerializeField] private bool isUnique;
        
        private void OnEnable()=>gameEvent.RegisterListener(this);
        
        private void OnDisable()=>gameEvent.UnregisterListener(this);
        
        public  void OnEventRaised() => voidResponse?.Invoke();
    }
}
