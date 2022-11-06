using System.Threading;
using JetBrains.Annotations;
using Project.Systems.Equipment;
using Project.Systems.Quest;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Project.Systems.Interactable
{
    public abstract class Interactable : MonoBehaviour
    {
        [SerializeField] [CanBeNull] private QuestData quest;
        [SerializeField] private float playerDistance;
        
        private Transform m_playerTransform;
        private PlayerActionMaps m_playerInput;
        private Inventory m_inventory;
        
        private bool m_canInteract = true;
        

        [Inject]
        private void Inject(PlayerActionMaps playerActionMaps, Inventory inventory)
        {
            m_playerInput = playerActionMaps;
            m_inventory = inventory;
        }

        protected virtual void OnEnable()
        {
            m_playerInput.Interactions.Enable(); //FIXME: its being enabled many times in different places 
            m_playerInput.Interactions.Interact.performed += OnInteractKey;
        }
        
        protected virtual void Start()
        {
            m_playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            if(quest != null) quest.Completed = false;
        }
        
        protected virtual void OnDisable()
        {   //we dont want to disable action map after one object is deleted - it cuts all the interactions
            m_playerInput.Interactions.Interact.performed -= OnInteractKey;
        }

        private void OnMouseOver()
        {
            m_canInteract = true;
        }

        private void OnMouseExit()
        {
            m_canInteract = false;
        }
        
        protected virtual void OnInteractKey(InputAction.CallbackContext context)
        {
            if(!m_canInteract || Vector3.Distance(transform.position, m_playerTransform.position) > playerDistance) return;

            CheckQuest();
            Interaction();
        }

        private void CheckQuest()
        {
            if (quest is not null)
            {
                if (!quest.Completed)
                {
                    if (m_inventory.Contains(quest.GetRequiredItem()))
                    {
                        m_inventory.RemoveItem(quest.GetRequiredItem());
                        quest.Completed = true;
                    }
                    else
                    {
                        Debug.Log(quest.GetText());
                        return;
                    }
                }
            }
        }
        
        protected abstract void Interaction();
    }
}