using System.Security.Cryptography;
using Project.Consts;
using UnityEngine;
using Project.Systems.Equipment;
using Zenject;

namespace Project.Systems.Interactable
{
    public class Wrench : Usable
    {
        protected override void SetName()
        {
            Name = ItemName.wrench;
        }
    }
}