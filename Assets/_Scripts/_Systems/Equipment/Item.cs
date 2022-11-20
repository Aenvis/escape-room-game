using Project.Consts;

namespace Project.Systems.Equipment
{
    public class Item
    {
        public ItemName Name { get; private set; }
        public Item(ItemName? name=null)
        {
            if (name is not null) Name = (ItemName)name;
        }
    }
}