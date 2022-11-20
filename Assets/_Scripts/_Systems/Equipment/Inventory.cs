// ReSharper disable once CheckNamespace
using System.Collections.Generic;
using System.Linq;
using Project.Consts;

namespace Project.Systems.Equipment
{
    public class Inventory
    {
        private List<Item> m_inventory;

        public Inventory()
        {
            m_inventory = new List<Item>();
        }

        public void AddItem(Item item)
        {
            UnityEngine.Debug.Log("Added");
            m_inventory.Add(item);
        }

        public void RemoveItem(ItemName itemName)
        {
            foreach (var item in m_inventory)
            {
                if (item is null || item.Name != itemName) continue;
                m_inventory.Remove(item);
                break;
            }
        }
        
        public Item GetItemAt(int id) => m_inventory[id];

        public bool Contains(ItemName itemName) => m_inventory.Any(item => item.Name == itemName);
        
        
        public int GetCount() => m_inventory.Count;
    }
}