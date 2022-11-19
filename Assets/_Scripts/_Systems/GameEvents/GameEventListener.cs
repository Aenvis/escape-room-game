using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Project.Systems.GameEvents
{
    public class GameEventListener : MonoBehaviour
    {
        [SerializeField] protected GameEvent gameEvent;
        [FormerlySerializedAs("voidResponse")] [SerializeField] [CanBeNull] private UnityEvent<object> response;
        [SerializeField] private bool isUnique;
        
        private void OnEnable()=>gameEvent.RegisterListener(this);
        
        private void OnDisable()=>gameEvent.UnregisterListener(this);
        
        public  void OnEventRaised(object param=null) => response?.Invoke(param);
    }
}
