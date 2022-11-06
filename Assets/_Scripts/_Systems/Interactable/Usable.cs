using System;
using Palmmedia.ReportGenerator.Core;
using Project.Systems.Equipment;
using UnityEngine;
using Zenject;

namespace Project.Systems.Interactable
{
    public abstract class Usable : Interactable
    {
        protected string Name;
        
        private Inventory m_inventory;

        [Inject]
        private void Injection(Inventory inventory)
        {
            m_inventory = inventory;
        }

        protected override void Start()
        {
            base.Start();
            SetName();
        }

        protected override void Interaction()
        {
            m_inventory.AddItem(new Item(Name));
            Destroy(gameObject);
        }
        
        /// <summary>
        /// Add item's name via this method using the following command:
        /// m_name = name;
        /// where name is object's in-game name (e.g. "wrench", "hammer")
        /// !!!Use lowercase only!!!
        /// </summary>
        protected abstract void SetName();
    }
}