using UnityEngine;

namespace Project.Systems.Interactable
{
    public class FuseBox : Interactable
    {
        [SerializeField] private GameObject fuse;
        
        protected override void Interaction()
        {
            if (fuse.activeSelf) return;
            fuse.SetActive(true);
        }
    }
}