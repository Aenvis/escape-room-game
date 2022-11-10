using Project.Consts;
using Project.Systems.Equipment;
using Zenject;

namespace Project.Systems.Interactable
{
    public abstract class Usable : Interactable
    {
        protected ItemName Name;
        
        private Inventory m_inventory;

        [Inject]
        private void Injection(Inventory inventory)
        {
            m_inventory = inventory;
        }

        protected override void Start()
        {
            base.Start();
            SetName();
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