using System;
using UnityEngine;
using Zenject;

namespace Project.Systems.Equipment
{
    public class InventoryDisplay : MonoBehaviour
    {
        [SerializeField] private ItemIconData iconData;

        private Inventory m_inventory;
        
        [Inject]
        private void Injection(Inventory inventory)
        {
            m_inventory = inventory;
        }

        private void Start()
        {
            iconData.Init();
        }

        private void OnGUI()
        {
            int itemCount = m_inventory.GetCount();
            if (m_inventory.GetCount() <= 0) return;

            float x = Screen.width - 70f;
            float y = (Screen.height / 2f) - (itemCount * 64f)/2;

            for (int i = 0; i < m_inventory.GetCount(); i++)
            {
                Item currItem = m_inventory.GetItemAt(i);
                GUI.DrawTexture(new Rect(x, y+(i*64f), 64f, 64f), iconData.GetTexture(currItem.Name));
            }
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