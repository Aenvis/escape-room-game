using Project.Systems.GameEvents;
using UnityEngine;

namespace Project.Systems.Interactable
{
    public class FuseBox : Interactable
    {
        [SerializeField] private GameObject fuse;
        [SerializeField] private GameEvent gameEvent;
        
        
        protected override void Interaction()
        {
            if (fuse.activeSelf) return;
            gameEvent.Invoke();
            fuse.SetActive(true);
        }
    }
}