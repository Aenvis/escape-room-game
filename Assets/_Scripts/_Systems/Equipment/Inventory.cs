// ReSharper disable once CheckNamespace

using System.Collections.Generic;

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

        public void RemoveItem(Item item)
        {
            m_inventory.Remove(item); 
        }

        public Item GetItemAt(int id) => m_inventory[id];
        
        public int GetCount() => m_inventory.Count;
    }
}