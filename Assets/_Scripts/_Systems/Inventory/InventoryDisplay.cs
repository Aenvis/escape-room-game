using System;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace Project.Systems.Inventory
{
    public class InventoryDisplay : MonoBehaviour
    {
        private Inventory m_inventory;
        [Inject]
        private void Injection(Inventory inventory)
        {
            m_inventory = inventory;
        }

        private void OnGUI()
        {
            int itemCount = m_inventory.GetCount();
            if (m_inventory.GetCount() <= 0) return;

            float x = Screen.width - 60f;
            float y = (Screen.height / 2f) - (itemCount * 50f)/2;
            
            GUI.Box(new Rect(x, y, 50f, itemCount * 50f), "");
            GUI.backgroundColor = new Color(0, 0, 0, 0);
        }
        
        #region debug
        #if UNITY_EDITOR
        public void DEBUG_AddItem(object count)
        {
            for(int i = 0; i < (int)count; i++) m_inventory.AddItem(new Item());
        }
        #endif
			
        #endregion
    }
}