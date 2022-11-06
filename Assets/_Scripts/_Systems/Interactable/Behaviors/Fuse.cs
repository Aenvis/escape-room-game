using Project.Consts;
using UnityEngine;

namespace Project.Systems.Interactable
{
    public class Fuse : Usable
    {
        protected override void SetName()
        {
            Name = ItemName.fuse;
        }
    }
}