using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

namespace Project.Systems.GameEvents
{
    [System.Serializable]
    public class UnityEventInt : UnityEvent<int>
    {
    }
    [System.Serializable]
    public class UnityEventStr : UnityEvent<string>
    {
    }
    public class GameEventListener : MonoBehaviour
    {
        [SerializeField] private GameEvent gameEvent;
        [SerializeField] [CanBeNull] private UnityEvent voidResponse;
        [SerializeField] [CanBeNull] private UnityEventInt intResponse;
        [SerializeField] [CanBeNull] private UnityEventStr strResponse;

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
            voidResponse?.Invoke();
        }

        public void OnEventRaisedWithParam(int val)
        {
            intResponse?.Invoke(val);
        }
        
        public void OnEventRaisedWithParam(string str)
        {
            strResponse?.Invoke(str);
        }
    }
}
