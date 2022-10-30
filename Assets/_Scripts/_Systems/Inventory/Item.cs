using Project._Scripts.Consts;

namespace Project.Systems.Inventory
{
    public class Item : IUsable
    {
        private ItemType type;
        void IUsable.Use()
        {
            throw new System.NotImplementedException();
        }
    }
}