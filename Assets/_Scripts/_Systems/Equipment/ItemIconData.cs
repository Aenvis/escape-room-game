using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Systems.Equipment
{
    [CreateAssetMenu(fileName = "Item Textures Data", menuName = "ItemTexturesData", order = 0)]
    public class ItemIconData : ScriptableObject
    {
        [Serializable]
        struct itemIcon
        {
            public string name;
            public Texture texture;
        }

        [SerializeField] private List<itemIcon> data;
        
        private Dictionary<string, Texture> m_texture = new Dictionary<string, Texture>();

        public void Init()
        {
            foreach (var el in data)
            {
                m_texture[el.name] = el.texture;
            }
        }

        public Texture GetTexture(string name) => m_texture[name];
    }
}