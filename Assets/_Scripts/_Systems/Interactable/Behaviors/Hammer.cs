using Project.Consts;
using UnityEngine;

namespace Project.Systems.Interactable
{
    public class Hammer : Usable
    {
        protected override void SetName()
        {
            Name = ItemName.hammer;
        }
    }
}