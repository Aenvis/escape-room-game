using System.Net.Mime;
using Project._Scripts.Consts;
using UnityEngine;

namespace Project.Systems.Equipment
{
    public class Item
    {
        public string Name { get; private set; }
        public Item(string name=null)
        {
            Name = name;
        }
    }
}