using System;
using Palmmedia.ReportGenerator.Core;
using Project.Consts;
using Project.Systems.Equipment;
using UnityEditor.Build;
using UnityEngine;
using Zenject;

namespace Project.Systems.Interactable
{
    public abstract class Usable : Interactable
    {
        protected ItemName Name;
        
        private Inventory m_inventory;
        private Outline m_outline;
        private bool m_isSelected;
        
        [Inject]
        private void Injection(Inventory inventory)
        {
            m_inventory = inventory;
        }

        protected override void Start()
        {
            base.Start();
            SetName();
            m_outline = gameObject.AddComponent<Outline>();
            m_outline.OutlineMode = Outline.Mode.OutlineAll;
            m_outline.OutlineColor = new Color(0, 127, 127);
            m_outline.OutlineWidth = 7.0f;
            m_outline.enabled = false;
        }

        protected override void OnMouseOver()
        {
            base.OnMouseOver();
            m_outline.enabled = true;
        }

        protected override void OnMouseExit()
        {
            base.OnMouseExit();
            m_outline.enabled = false;
        }

        protected override void Interaction()
        {
            m_inventory.AddItem(new Item(Name));
            Destroy(gameObject);
        }
        
        /// <summary>
        /// Add item's name via this method using the following command:
        /// m_name = ItemName.name;
        /// where name is object's in-game name (e.g. wrench, hammer)
        /// ItemName is an enum class (you can find int at _Scripts/Consts)
        /// </summary>
        protected abstract void SetName();
    }
}