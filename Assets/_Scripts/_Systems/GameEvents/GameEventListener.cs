using UnityEngine;
using UnityEngine.Events;

namespace Project.Systems.GameEvents
{
    public class GameEventListener : MonoBehaviour
    {
        [SerializeField] private GameEvent gameEvent;
        [SerializeField] private UnityEvent response;

        private void OnEnable()
        {
            gameEvent.RegisterListener(this);
        }

        private void OnDisable()
        {
            gameEvent.UnregisterListener(this);
        }

        public void OnEventRaised()
        {
            response?.Invoke();
        }
    }
    
    public class GameEventListener<T> : MonoBehaviour
    {
        [SerializeField] private GameEvent<T> gameEvent;
        [SerializeField] private UnityEvent<T> response;

        private void OnEnable()
        {
            gameEvent.RegisterListener(this);
        }

        private void OnDisable()
        {
            gameEvent.UnregisterListener(this);
        }

        public void OnEventRaised(T param)
        {
            response?.Invoke(param);
        }
    }
}
