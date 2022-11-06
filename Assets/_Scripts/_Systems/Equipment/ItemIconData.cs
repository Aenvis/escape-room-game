using System;
using System.Collections.Generic;
using Project.Consts;
using UnityEngine;

namespace Project.Systems.Equipment
{
    [CreateAssetMenu(fileName = "Item Textures Data", menuName = "ItemTexturesData", order = 0)]
    public class ItemIconData : ScriptableObject
    {
        [Serializable]
        private struct ItemIcon
        {
            public ItemName name;
            public Texture texture;
        }

        [SerializeField] private List<ItemIcon> data;
        
        private Dictionary<ItemName, Texture> m_texture = new Dictionary<ItemName, Texture>();

        public void Init()
        {
            foreach (var el in data)
            {
                m_texture[el.name] = el.texture;
            }
        }

        public Texture GetTexture(ItemName itemName) => m_texture[itemName];
    }
}