using Project.Consts;
using Project.Systems.Equipment;
using UnityEngine;
using Zenject;

namespace Project.Systems.Interactable
{
    public class UsableItem : Interactable
    {
        [SerializeField]private new ItemName name;
        
        private Inventory m_inventory;
        private Outline m_outline;
        private bool m_isSelected;
        
        [Inject]
        private void Injection(Inventory inventory)
        {
            m_inventory = inventory;
        }

        protected override void Start()
        {
            base.Start();
            OutlineSetup();
            const string sfxName = "collect item";
            soundEffect = Resources.Load($"{sfxName}") as AudioClip;
            #if UNITY_EDITOR
            if (!soundEffect) 
                Debug.LogError($"COULDNT LOAD RESOURCE: {sfxName} in {gameObject.name}");
            #endif
        }

        protected override void OnMouseOver()
        {
            base.OnMouseOver();
            if (Vector3.Distance(transform.position, m_playerTransform.position) > playerDistance) return;
            m_outline.enabled = true;
        }

        protected override void OnMouseExit()
        {
            base.OnMouseExit();
            m_outline.enabled = false;
        }

        private void OutlineSetup()
        {
            m_outline = gameObject.AddComponent<Outline>();
            m_outline.OutlineMode = Outline.Mode.OutlineAll;
            m_outline.OutlineColor = new Color(0, 127, 127);
            m_outline.OutlineWidth = 7.0f;
            m_outline.enabled = false;  
        }
        
        protected override void Interaction()
        {
            m_inventory.AddItem(new Item(name));
            GameObject o;
            (o = gameObject).SetActive(false);
            Destroy(o, 2);
        }
    }
}